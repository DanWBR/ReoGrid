/*****************************************************************************
 * 
 * ReoGrid - .NET Spreadsheet Control
 * 
 * http://reogrid.net/
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
 * PURPOSE.
 *
 * ReoGridEditor is released under MIT license.
 *
 * Copyright (c) 2012-2016 Jing <lujing at unvell.com>
 * Copyright (c) 2012-2016 unvell.com, all rights reserved.
 * 
 ****************************************************************************/

using System;
using Eto.Drawing;
using Eto.Forms;

using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Events;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid;
using unvell.Common;

namespace unvell.ReoGrid.Editor
{
	public partial class AddressFieldControl : TableLayout
	{
		private TextBox addressBox;
		private DropDown dropdown;
		private PushdownArrowControl arrowControl;

		public TextBox AddressBox
		{
			get { return addressBox; }
			set { addressBox = value; }
		}

		private ReoGridControl workbook;
		private Worksheet worksheet;

		public ReoGridControl GridControl
		{
			get
			{
				return workbook;
			}
			set
			{
				if (this.workbook != null)
				{
					this.workbook.CurrentWorksheetChanged -= workbook_CurrentWorksheetChanged;

					//workbook.Disposed -= grid_Disposed;
				}

				this.worksheet = null;
				this.workbook = value;

				if (workbook != null)
				{
					this.workbook.CurrentWorksheetChanged += workbook_CurrentWorksheetChanged;
			
					//workbook.Disposed += grid_Disposed;

					RefreshCurrentAddress();
				}
			}
		}

		void workbook_CurrentWorksheetChanged(object sender, EventArgs e)
		{
			if (this.worksheet != null)
			{
				this.worksheet.SelectionRangeChanging -= grid_SelectionRangeChanging;
				this.worksheet.SelectionRangeChanged -= grid_SelectionRangeChanged;
			}

			this.worksheet = this.workbook.CurrentWorksheet;

			if (this.worksheet != null)
			{
				RefreshCurrentAddress();

				this.worksheet.SelectionRangeChanging += grid_SelectionRangeChanging;
				this.worksheet.SelectionRangeChanged += grid_SelectionRangeChanged;
			}
		}

		public AddressFieldControl()
		{

			SuspendLayout();

			BackgroundColor = SystemColors.WindowBackground;

			addressBox = new TextBox()
			{
				TextAlignment = TextAlignment.Center,
			};

			arrowControl = new PushdownArrowControl() { Width = 20 };

			this.Rows.Add(new TableRow { Cells = { addressBox, arrowControl } });
            this.Rows[0].Cells[0].ScaleWidth = true;

			addressBox.GotFocus += addressBox_GotFocus;
			addressBox.LostFocus += addressBox_LostFocus;
			addressBox.KeyDown += txtAddress_KeyDown;
			
			arrowControl.MouseDown += arrowControl_MouseDown;

			ResumeLayout();
		}

		void grid_SelectionRangeChanging(object sender, RangeEventArgs e)
		{
			var range = this.worksheet.SelectionRange;

			if (this.worksheet.IsMergedCell(range))
			{
				addressBox.Text = range.StartPos.ToAddress();
			}
			else
			{
				addressBox.Text = range.ToStringSpans();
			}
		}

		void grid_SelectionRangeChanged(object sender, RangeEventArgs e)
		{
			RefreshCurrentAddress();
		}

		public void RefreshCurrentAddress()
		{
			if (worksheet == null) return;

			var range = this.worksheet.SelectionRange;

			var name = this.worksheet.GetNameByRange(range);

			if (!string.IsNullOrEmpty(name))
			{
				addressBox.Text = name;
			}
			else
			{
				if (!range.IsEmpty)
				{
					if (this.worksheet.IsMergedCell(range))
					{
						addressBox.Text = range.StartPos.ToAddress();
					}
					else
					{
						addressBox.Text = range.ToAddress();
					}
				}
				else
				{
					addressBox.Text = string.Empty;
				}
			}
		}

		private bool focused = false;

		void addressBox_GotFocus(object sender, EventArgs e)
		{
			focused = true;
			addressBox.TextAlignment = TextAlignment.Left;
			addressBox.Selection = new Range<int>(0, addressBox.Text.Length);
			focused = false;
		}

		void addressBox_LostFocus(object sender, EventArgs e)
		{
			if (!focused)
			{
				EndEditAddress();
			}
		}

		void txtAddress_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Keys.Enter)
			{
				string id = addressBox.Text;

				// avoid to directly use trim, it will create new string even nothing to be trimmed
				if (id.StartsWith(" ") || id.EndsWith(" "))
				{
					id = id.Trim();
				}

				if (RangePosition.IsValidAddress(id))
				{
					this.worksheet.SelectionRange = new RangePosition(id);
					workbook.Focus();
				}
				else if (RGUtility.IsValidName(id))
				{
					var refRange = this.worksheet.GetNamedRange(id);

					if (refRange != null)
					{
						this.worksheet.SelectionRange = refRange;
						workbook.Focus();
					}
					else
					{
						try
						{
							this.worksheet.DefineNamedRange(id, this.worksheet.SelectionRange);
							workbook.Focus();
						}
						catch (NamedRangeAlreadyDefinedException)
						{
							// should be not reached
							MessageBox.Show("Another range with same name does already exist.");
						}
					}
				}
			}
			else if (e.Key == Keys.Down)
			{
				PushDown();
			}
			else if (e.Key == Keys.Escape)
			{
				workbook.Focus();
			}
		}

		void grid_Disposed(object sender, EventArgs e)
		{
			this.GridControl = null;
		}

		void arrowControl_MouseDown(object sender, MouseEventArgs e)
		{
			PushDown();
		}

		public void StartEditAddress()
		{
			if (dropdown == null || !dropdown.Visible)
			{
				PushDown();
			}

			addressBox.Focus();
		}

		public void EndEditAddress()
		{
			if (!focused)
			{
				addressBox.TextAlignment = TextAlignment.Center;
			}
		}

		private void PushDown()
		{
			if (dropdown == null)
			{
				dropdown = new DropDown();
				dropdown.SelectedIndexChanged += ListBox_ItemSelected;
			}

			dropdown.Items.Clear();
			foreach (var name in this.worksheet.GetAllNamedRanges())
			{
				dropdown.Items.Add(name);
			}

			dropdown.Width = this.Width;
			dropdown.Height = 200;

			StartEditAddress();
		}

		void ListBox_ItemSelected(object sender, EventArgs e)
		{
			GotoNamedRange(Convert.ToString(dropdown.SelectedValue.ToString()));
		}

		public void GotoNamedRange(string name)
		{
			if (workbook != null)
			{
				var refRange = this.worksheet.GetNamedRange(name);

				if (refRange != null)
				{
					this.worksheet.SelectionRange = refRange;
					EndEditAddress();
					workbook.Focus();
				}
			}
		}

		private void PullUp()
		{
			if (dropdown != null)
			{
				dropdown.Visible = false;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.Key == Keys.Escape)
			{
				EndEditAddress();
			}

			base.OnKeyDown(e);
		}
	}

	internal class PushdownArrowControl : Drawable
	{
		protected override void OnPaint(PaintEventArgs e)
		{
			GraphicsToolkit.FillTriangle(e.Graphics, 7, new Point(Bounds.Right - 10, Bounds.Top + Bounds.Height / 2 - 1));
		}
	}

}
