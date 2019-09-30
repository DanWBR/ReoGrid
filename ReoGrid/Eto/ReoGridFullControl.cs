﻿using Eto.Forms;
using Eto.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Events;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Actions;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoRenderer;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.IO;
using System.IO;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid
{
    public class ReoGridFullControl : PixelLayout
    {

        public ReoGridControl GridControl;

        private Worksheet worksheet;

        private string CurrentFilePath;

        private ComboBox cbNamedRanges;
        private TextBox tbFormula;
        private ColorPicker colorPickerFore, colorPickerBack;
        private FontPicker fontPicker;
        private Button btnLeftAlign, btnRightAlign, btnCenterAlign, btnTopAlign, btnBottomAlign, btnMiddleAlign;

        private Button btnNew, btnOpen, btnSave, btnUndo, btnRedo, btnCut, btnCopy, btnPaste, btnMerge, btnUnMerge;
        private ColorPicker colorPickerBorder;

        private DropDown cbBorderStyle;

        public ReoGridFullControl() : base()
        {

            var container = new DynamicLayout();

            var imgpfx = "DWSIM.CrossPlatform.UI.Controls.ReoGrid.Icons.";

            container.BeginVertical();

            this.SizeChanged += (sender, e) =>
            {
                Console.WriteLine("PixelLayout Size Changed");
                container.Size = new Size(this.Width, this.Height);
            };

            cbNamedRanges = new ComboBox() { Width = 100, Height = 22 };
            var btnFunction = new Button() { Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "FunctionHS.png").WithSize(16, 16) };
            tbFormula = new TextBox() { Height = 22 };

            tbFormula.GotFocus += (sender, e) =>
            {
                backValue = tbFormula.Text;
            };

            tbFormula.KeyDown += (sender, e) => {
                if (e.Key == Keys.Enter)
                {
                    worksheet.Cells[worksheet.selectionRange.StartPos].Formula = tbFormula.Text.TrimStart('=');
                    GridControl.Focus();
                }
            };

            fontPicker = new FontPicker { Width = 100, Value = SystemFonts.Default() };

            colorPickerBack = new ColorPicker() { Value = SystemColors.ControlBackground };
            colorPickerFore = new ColorPicker() { Value = SystemColors.ControlText };
            colorPickerBorder = new ColorPicker() { Value = SystemColors.ControlText };

            var lbFont = new Label { Text = "Font", VerticalAlignment = VerticalAlignment.Center };
            var lbFore = new Label { Text = "Text Color", VerticalAlignment = VerticalAlignment.Center };
            var lbBack = new Label { Text = "Background Color", VerticalAlignment = VerticalAlignment.Center };

            btnLeftAlign = new Button() { ToolTip = "Align Left", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "AlignTableCellMiddleLeftJustHS.PNG").WithSize(16, 16) };
            btnCenterAlign = new Button() { ToolTip = "Align Center", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "AlignTableCellMiddleCenterHS.png").WithSize(16, 16) };
            btnRightAlign = new Button() { ToolTip = "Align Right", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "AlignTableCellMiddleRightHS.png").WithSize(16, 16) };
            btnTopAlign = new Button() { ToolTip = "Align Top", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "AlignLayoutTop.png").WithSize(16, 16) };
            btnMiddleAlign = new Button() { ToolTip = "Align Middle", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "AlignLayoutMiddle.png").WithSize(16, 16) };
            btnBottomAlign = new Button() { ToolTip = "Align Bottom", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "AlignLayoutBottom.png").WithSize(16, 16) };

            btnNew = new Button() { ToolTip = "New Workbook", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "NewDocumentHS.png").WithSize(16, 16) };
            btnOpen = new Button() { ToolTip = "Open Workbook", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "openHS.png").WithSize(16, 16) };
            btnSave = new Button() { ToolTip = "Save Workbook", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "saveHS.png").WithSize(16, 16) };

            btnCut = new Button() { ToolTip = "Cut", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "CutHS.png").WithSize(16, 16) };
            btnCopy = new Button() { ToolTip = "Copy", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "CopyHS.png").WithSize(16, 16) };
            btnPaste = new Button() { ToolTip = "Paste", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "PasteHS.png").WithSize(16, 16) };

            btnUndo = new Button() { ToolTip = "Undo", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "Edit_UndoHS.png").WithSize(16, 16) };
            btnRedo = new Button() { ToolTip = "Redo", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "Edit_RedoHS.png").WithSize(16, 16) };

            btnMerge = new Button() { ToolTip = "Merge", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "cell_merge.png").WithSize(16, 16) };
            btnUnMerge = new Button() { ToolTip = "Unmerge", Height = 24, Width = 24, Image = Bitmap.FromResource(imgpfx + "cell_unmerge.png").WithSize(16, 16) };

            cbBorderStyle = new DropDown() { Items = { "No Borders", "All Borders", "External Only", "Top Only", "Bottom Only", "Left Only", "Right Only" } };

            cbBorderStyle.SelectedIndexChanged += CbBorderStyle_SelectedIndexChanged;

            btnNew.Click += BtnNew_Click;
            btnOpen.Click += BtnOpen_Click;
            btnSave.Click += BtnSave_Click;

            btnCut.Click += BtnCut_Click;
            btnCopy.Click += BtnCopy_Click;
            btnPaste.Click += BtnPaste_Click;

            btnUndo.Click += BtnUndo_Click;
            btnRedo.Click += BtnRedo_Click;

            btnMerge.Click += BtnMerge_Click;
            btnUnMerge.Click += BtnUnMerge_Click;

            btnLeftAlign.Click += textCenterToolStripButton_Click;
            btnCenterAlign.Click += textCenterToolStripButton_Click;
            btnRightAlign.Click += textRightToolStripButton_Click;
            btnTopAlign.Click += textAlignTopToolStripButton_Click;
            btnMiddleAlign.Click += textAlignMiddleToolStripButton_Click;
            btnBottomAlign.Click += textAlignBottomToolStripButton_Click;

            fontPicker.ValueChanged += FontPicker_ValueChanged;
            colorPickerBack.ValueChanged += ColorPickerBack_ValueChanged;
            colorPickerFore.ValueChanged += ColorPickerFore_ValueChanged;
            colorPickerBorder.ValueChanged += ColorPickerBorder_ValueChanged;

            var functionPanel3 = new TableLayout() { Spacing = new Size(4, 4), Height = 34, Padding = new Padding(5) };
            functionPanel3.Rows.Add(new TableRow
            {
                Cells = { btnNew, btnOpen, btnSave, new Label {Text = " " },
                                                            btnCut, btnCopy, btnPaste, new Label {Text = " " },
                                                            btnUndo, btnRedo, new Label {Text = " " },
                                                            new Label {Text = "Border Style", VerticalAlignment = VerticalAlignment.Center }, cbBorderStyle,
                                                            new Label {Text = "Border Color", VerticalAlignment = VerticalAlignment.Center }, colorPickerBorder, new Label {Text = " " },
                                                            btnMerge, btnUnMerge, null},
                ScaleHeight = true
            });

            container.Add(functionPanel3, true, false);

            var functionPanel2 = new TableLayout() { Spacing = new Size(4, 4), Height = 34, Padding = new Padding(5) };
            functionPanel2.Rows.Add(new TableRow
            {
                Cells = { lbFont, fontPicker, lbFore, colorPickerFore, lbBack, colorPickerBack,
                btnLeftAlign, btnCenterAlign, btnRightAlign, btnTopAlign, btnMiddleAlign, btnBottomAlign, null },
                ScaleHeight = true
            });

            container.Add(functionPanel2, true, false);

            var functionPanel = new TableLayout() { Spacing = new Size(4, 4), Height = 34, Padding = new Padding(5) };
            functionPanel.Rows.Add(new TableRow { Cells = { cbNamedRanges, btnFunction, tbFormula }, ScaleHeight = true });

            container.Add(functionPanel, true, false);

            container.EndVertical();

            container.BeginVertical();

            GridControl = new ReoGridControl(this);

            worksheet = GridControl.CurrentWorksheet;

            this.Add(container, 0, 0);
            this.Add(GridControl.editTextbox, 0, 0);

            container.Add(GridControl, true, true);
            container.Add(GridControl.bottomPanel, true, false);

            container.EndVertical();

            GridControl.Width = 5000;
            GridControl.Height = 2000;

            // cell events

            GridControl.CurrentWorksheetChanged += workbook_CurrentWorksheetChanged;

            this.worksheet.SelectionRangeChanging += grid_SelectionRangeChanging;
            this.worksheet.SelectionRangeChanged += grid_SelectionRangeChanged;
            this.worksheet.FocusPosChanged += grid_FocusPosChanged;

            RefreshCurrentAddress();

        }

        private void CbBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GridControl.DoAction(new SetRangeBorderAction(this.worksheet.SelectionRange, BorderPositions.All,
                 new RangeBorderStyle { Color = colorPickerBorder.Value.ToSolidColor(), Style = BorderLineStyle.None }));
            this.GridControl.DoAction(new SetRangeBorderAction(this.worksheet.SelectionRange, GetBorderPositionFromIndex(),
                 new RangeBorderStyle { Color = colorPickerBorder.Value.ToSolidColor(), Style = BorderLineStyle.Solid }));
        }

        private void BtnUnMerge_Click(object sender, EventArgs e)
        {
            this.GridControl.DoAction(new UnmergeRangeAction(this.worksheet.SelectionRange));
        }

        private void BtnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                this.GridControl.DoAction(new MergeRangeAction(this.worksheet.SelectionRange));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxType.Error);
            }
        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {
            this.GridControl.Undo();
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            this.GridControl.Undo();
        }

        private void BtnPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void BtnCut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveAsDocument();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filters.Add(new FileFilter("All Supported Files", new[] { ".xlsx", ".csv", ".rgf" }));

                if (ofd.ShowDialog(this) == DialogResult.Ok)
                {
                    LoadFile(ofd.FileName);
                    this.SetCurrentDocumentFile(ofd.FileName);
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (!CloseDocument())
            {
                return;
            }

            this.GridControl.Reset();

            //showGridLinesToolStripMenuItem.Checked = workbook.HasSettings(ReoGridSettings.View_ShowGridLine);
            this.CurrentFilePath = null;
            //this.currentTempFilePath = null;
        }

        private void ColorPickerBorder_ValueChanged(object sender, EventArgs e)
        {
            if (colorPickerBorder.Value != Colors.Transparent)
            {
                this.GridControl.DoAction(new SetRangeBorderAction(this.worksheet.SelectionRange, GetBorderPositionFromIndex(),
                     new RangeBorderStyle { Color = colorPickerBorder.Value.ToSolidColor(), Style = BorderLineStyle.Solid }));
            }
            else
            {
                colorPickerBorder.Value = SystemColors.ControlText;
            }
        }

        private BorderPositions GetBorderPositionFromIndex()
        {
            //"No Borders", "All Borders", "External Only", "Top Only", "Bottom Only", "Left Only", "Right Only"
            switch (cbBorderStyle.SelectedIndex)
            {
                default: case 0: return BorderPositions.None;
                case 1: return BorderPositions.All;
                case 2: return BorderPositions.Outside;
                case 3: return BorderPositions.Top;
                case 4: return BorderPositions.Bottom;
                case 5: return BorderPositions.Left;
                case 6: return BorderPositions.Right;
            }
        }

        private void ColorPickerFore_ValueChanged(object sender, EventArgs e)
        {
            if (colorPickerFore.Value != Colors.Transparent)
            {
                GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
                {
                    Flag = PlainStyleFlag.TextColor,
                    TextColor = colorPickerFore.Value.ToSolidColor()
                }));
            }
            else
            {
                colorPickerFore.Value = SystemColors.ControlText;
            }
        }

        private void ColorPickerBack_ValueChanged(object sender, EventArgs e)
        {
            if (colorPickerBack.Value != null)
            {
                GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
                {
                    Flag = PlainStyleFlag.BackColor,
                    BackColor = colorPickerBack.Value.ToSolidColor()
                }));
            }
            else
            {
                colorPickerBack.Value = Colors.Transparent;
            }
        }

        private void FontPicker_ValueChanged(object sender, EventArgs e)
        {
            if (fontPicker.Value != null)
            {
                GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
                {
                    Flag = PlainStyleFlag.FontAll,
                    FontName = fontPicker.Value.FamilyName,
                    FontSize = fontPicker.Value.Size,
                    Bold = fontPicker.Value.Bold,
                    Italic = fontPicker.Value.Italic,
                    Underline = fontPicker.Value.Underline,
                    Strikethrough = fontPicker.Value.Strikethrough
                }));
            }
            else
            {
                fontPicker.Value = SystemFonts.Default();
            }
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
            var cell = this.worksheet.GetCell(this.worksheet.FocusPos);
            if (cell != null)
            {
                colorPickerFore.Value = cell.Style.TextColor.ToEto();
                colorPickerBack.Value = cell.Style.BackColor.ToEto();
                fontPicker.Value = cell.RenderFont;
            }
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

        #region Alignment
        private void textLeftToolStripButton_Click(object sender, EventArgs e)
        {
            GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Left,
            }));
        }
        private void textCenterToolStripButton_Click(object sender, EventArgs e)
        {
            GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Center,
            }));
        }
        private void textRightToolStripButton_Click(object sender, EventArgs e)
        {
            GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.Right,
            }));
        }
        private void distributedIndentToolStripButton_Click(object sender, EventArgs e)
        {
            GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.HorizontalAlign,
                HAlign = ReoGridHorAlign.DistributedIndent,
            }));
        }

        private void textAlignTopToolStripButton_Click(object sender, EventArgs e)
        {
            GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.VerticalAlign,
                VAlign = ReoGridVerAlign.Top,
            }));
        }
        private void textAlignMiddleToolStripButton_Click(object sender, EventArgs e)
        {
            GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.VerticalAlign,
                VAlign = ReoGridVerAlign.Middle,
            }));
        }
        private void textAlignBottomToolStripButton_Click(object sender, EventArgs e)
        {
            GridControl.DoAction(new SetRangeStyleAction(this.worksheet.SelectionRange, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.VerticalAlign,
                VAlign = ReoGridVerAlign.Bottom,
            }));
        }
        #endregion

        public bool CloseDocument()
        {
            if (this.GridControl.IsWorkbookEmpty)
            {
                return true;
            }

            var dr = MessageBox.Show("You'll lose all unsaved data. Continue?", "Spreadsheet", MessageBoxButtons.YesNoCancel, MessageBoxType.Question);

            if (dr == DialogResult.No)
                return true;
            else if (dr == DialogResult.Cancel)
                return false;

            FileFormat format = FileFormat._Auto;

            if (!string.IsNullOrEmpty(this.CurrentFilePath))
            {
                format = GetFormatByExtension(this.CurrentFilePath);
            }

            if (format == FileFormat._Auto || string.IsNullOrEmpty(this.CurrentFilePath))
            {
                return SaveAsDocument();
            }
            else
            {
                SaveDocument();
            }

            return true;
        }

        private FileFormat GetFormatByExtension(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return FileFormat._Auto;
            }

            string ext = Path.GetExtension(this.CurrentFilePath);

            if (ext.Equals(".rgf", StringComparison.CurrentCultureIgnoreCase)
                || ext.Equals(".xml", StringComparison.CurrentCultureIgnoreCase))
            {
                return FileFormat.ReoGridFormat;
            }
            else if (ext.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase))
            {
                return FileFormat.Excel2007;
            }
            else if (ext.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
            {
                return FileFormat.CSV;
            }
            else
            {
                return FileFormat._Auto;
            }
        }

        /// <summary>
        /// Save current document
        /// </summary>
        public void SaveDocument()
        {
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                SaveAsDocument();
            }
            else
            {
                SaveFile(this.CurrentFilePath);
            }
        }

        /// <summary>
        /// Save current document by specifying new file path
        /// </summary>
        /// <returns>true if operation is successful, otherwise false</returns>
        public bool SaveAsDocument()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {

                sfd.Filters.Add(new FileFilter("Excel Spreadsheet", new[] { ".xlsx" }));
                sfd.Filters.Add(new FileFilter("ReoGrid Spreadsheet", new[] { ".rgf" }));
                sfd.Filters.Add(new FileFilter("Text (Comma-Separated Values)", new[] { ".csv" }));

                if (!string.IsNullOrEmpty(this.CurrentFilePath))
                {
                    sfd.FileName = Path.GetFileNameWithoutExtension(this.CurrentFilePath);

                    var format = GetFormatByExtension(this.CurrentFilePath);

                    switch (format)
                    {
                        case FileFormat.Excel2007:
                            sfd.CurrentFilterIndex = 1;
                            break;

                        case FileFormat.ReoGridFormat:
                            sfd.CurrentFilterIndex = 2;
                            break;

                        case FileFormat.CSV:
                            sfd.CurrentFilterIndex = 3;
                            break;
                    }
                }

                if (sfd.ShowDialog(this) == DialogResult.Ok)
                {
                    SaveFile(sfd.FileName);
                    return true;
                }
            }

            return false;
        }

        private void SaveFile(string path)
        {

            FileFormat fm = FileFormat._Auto;

            if (path.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
            {
                fm = FileFormat.Excel2007;
            }
            else if (path.EndsWith(".rgf", StringComparison.CurrentCultureIgnoreCase))
            {
                fm = FileFormat.ReoGridFormat;
            }
            else if (path.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
            {
                fm = FileFormat.CSV;
            }

            try
            {
                this.GridControl.Save(path, fm);

                this.SetCurrentDocumentFile(path);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Save error: " + ex.Message, "Save Workbook", MessageBoxButtons.OK, MessageBoxType.Error);
            }
        }

        private void SetCurrentDocumentFile(string filepath)
        {
            this.CurrentFilePath = filepath;
            //this.currentTempFilePath = null;
        }

        public void LoadFile(string path)
        {
            LoadFile(path, Encoding.Default);
        }

        /// <summary>
        /// Load spreadsheet from specified file
        /// </summary>
        /// <param name="path">path to load file</param>
        /// <param name="encoding">encoding to read input stream</param>
        public void LoadFile(string path, Encoding encoding)
        {
            this.CurrentFilePath = null;

            bool success = false;

            GridControl.CurrentWorksheet.Reset();

            try
            {
                GridControl.Load(path, IO.FileFormat._Auto, encoding);
                success = true;
            }
            catch (FileNotFoundException ex)
            {
                success = false;
                MessageBox.Show("File not found: " + ex.FileName, "Spreadsheet Editor", MessageBoxButtons.OK, MessageBoxType.Error);
            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show("Load file failed: " + ex.Message, "Spreadsheet Editor", MessageBoxButtons.OK, MessageBoxType.Error);
            }

            if (success)
            {
                this.CurrentFilePath = path;
                //this.currentTempFilePath = null;
            }
        }

        private void Cut()
        {
            // Cut method will always perform action to do cut
            try
            {
                this.worksheet.Cut();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxType.Error);
            }
        }

        private void Copy()
        {
            try
            {
                this.worksheet.Copy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxType.Error);
            }
        }

        private void Paste()
        {
            try
            {
                this.worksheet.Paste();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxType.Error);
            }
        }

    }
}
