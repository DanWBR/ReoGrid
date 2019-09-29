using Eto.Forms;
using Eto.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Events;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Actions;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid
{
    public class ReoGridFullControl : DynamicLayout
    {

        public ReoGridControl GridControl;

        private Worksheet worksheet;

        private ComboBox cbNamedRanges;
        private TextBox tbFormula;

        public ReoGridFullControl() : base()
        {

            var px = new PixelLayout();

            var imgpfx = "DWSIM.CrossPlatform.UI.Controls.ReoGrid.Icons.";

            this.BeginVertical();

            this.SizeChanged += (sender, e) =>
            {
                Console.WriteLine("PixelLayout Size Changed");
                px.Size = new Size(this.Width, this.Height);
            };

            cbNamedRanges = new ComboBox() { Width = 100, Height = 22 };
            var btnFunction = new Button() { Height = 22, Width = 22, Image = Bitmap.FromResource(imgpfx + "FunctionHS.png").WithSize(20, 20) };
            tbFormula = new TextBox() { Height = 22 };

            tbFormula.GotFocus += (sender, e) => {
                backValue = tbFormula.Text;
            };

            var functionPanel = new TableLayout() { Spacing = new Size(5, 5), Height = 34, Padding = new Padding(5) };
            functionPanel.Rows.Add(new TableRow { Cells = { cbNamedRanges, btnFunction, tbFormula }, ScaleHeight = true });

            this.Add(functionPanel, true, false);

            this.EndVertical();

            this.BeginVertical();

            GridControl = new ReoGridControl(px);

            GridControl.NewWorksheet();
            GridControl.NewWorksheet();

            worksheet = GridControl.CurrentWorksheet;

            px.Add(GridControl, 0, 0);
            px.Add(GridControl.editTextbox, 0, 0);

            this.Add(px, true, true);

            this.Add(GridControl.bottomPanel);

            this.EndVertical();

            GridControl.Width = 5000;
            GridControl.Height = 2000;

            // cell events

            GridControl.CurrentWorksheetChanged += workbook_CurrentWorksheetChanged;
            
            this.worksheet.SelectionRangeChanging += grid_SelectionRangeChanging;
            this.worksheet.SelectionRangeChanged += grid_SelectionRangeChanged;
            this.worksheet.FocusPosChanged += grid_FocusPosChanged;

            RefreshCurrentAddress();

        }

        void workbook_CurrentWorksheetChanged(object sender, EventArgs e)
        {
            if (GridControl != null)
            {
                worksheet.SelectionRangeChanging -= grid_SelectionRangeChanging;
                worksheet.SelectionRangeChanged -= grid_SelectionRangeChanged;
            }

            if (this.worksheet != null)
            {
                this.worksheet.FocusPosChanged -= grid_FocusPosChanged;
            }

            this.worksheet = GridControl.CurrentWorksheet;

            if (this.worksheet != null)
            {
                RefreshCurrentAddress();
                ReadFormulaFromCell();

                this.worksheet.SelectionRangeChanging += grid_SelectionRangeChanging;
                this.worksheet.SelectionRangeChanged += grid_SelectionRangeChanged;
            }

            if (this.worksheet != null)
            {
                ReadFormulaFromCell();
                this.worksheet.FocusPosChanged += grid_FocusPosChanged;
            }
        }

        void grid_SelectionRangeChanging(object sender, RangeEventArgs e)
        {
            var range = this.worksheet.SelectionRange;

            if (this.worksheet.IsMergedCell(range))
            {
                cbNamedRanges.Text = range.StartPos.ToAddress();
            }
            else
            {
                cbNamedRanges.Text = range.ToStringSpans();
            }
        }

        void grid_SelectionRangeChanged(object sender, RangeEventArgs e)
        {
            RefreshCurrentAddress();
        }

        void grid_FocusPosChanged(object sender, CellPosEventArgs e)
        {
            ReadFormulaFromCell();
        }

        public void RefreshCurrentAddress()
        {
            if (worksheet == null) return;

            var range = this.worksheet.SelectionRange;

            var name = this.worksheet.GetNameByRange(range);

            if (!string.IsNullOrEmpty(name))
            {
                cbNamedRanges.Text = name;
            }
            else
            {
                if (!range.IsEmpty)
                {
                    if (this.worksheet.IsMergedCell(range))
                    {
                        cbNamedRanges.Text = range.StartPos.ToAddress();
                    }
                    else
                    {
                        cbNamedRanges.Text = range.ToAddress();
                    }
                }
                else
                {
                    cbNamedRanges.Text = string.Empty;
                }
            }
        }

        private void ReadFormulaFromCell()
        {
            if (this.worksheet == null)
            {
                tbFormula.Text = string.Empty;
            }
            else
            {
                var cell = this.worksheet.GetCell(this.worksheet.FocusPos);

                if (cell == null)
                {
                    tbFormula.Text = string.Empty;
                }
                else
                {
                    var formula = cell.Formula;

                    if (!string.IsNullOrEmpty(formula))
                    {
                        tbFormula.Text = "=" + formula;
                    }
                    else
                    {
                        tbFormula.Text = Convert.ToString(cell.Data);
                    }
                }
            }
        }

        private string backValue;

        private bool ApplyNewFormula()
        {
            if (this.worksheet != null)
            {
                var value = tbFormula.Text;

                if (value != backValue)
                {
                    if (this.worksheet.IsEditing)
                    {
                        this.worksheet.EndEdit(value);
                    }
                    else
                    {
                        var pos = GridControl.CurrentWorksheet.FocusPos;

                        var currentData = GridControl.CurrentWorksheet.GetCellData(pos);

                        if (currentData != null || !string.IsNullOrEmpty(tbFormula.Text))
                        {
                            GridControl.DoAction(new SetCellDataAction(pos, tbFormula.Text));
                        }

                        return true;
                    }
                }
            }
            return false;
        }


    }
}
