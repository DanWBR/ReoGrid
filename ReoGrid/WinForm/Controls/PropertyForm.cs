﻿/*****************************************************************************
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
 * ReoGrid and ReoGridEditor is released under MIT license.
 *
 * Copyright (c) 2012-2016 Jing <lujing at unvell.com>
 * Copyright (c) 2012-2016 unvell.com, all rights reserved.
 * 
 ****************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Actions;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.WinForm.Controls;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.PropertyPages
{
	internal interface IPropertyPage
	{
		ReoGridControl Grid { get; set; }
		void SetupUILanguage();
		void LoadPage();
		WorksheetReusableAction CreateUpdateAction();
		event EventHandler Done;
	}

	public partial class PropertyForm : Form
	{
		private ReoGridControl grid;

		public ReoGridControl Grid
		{
			get { return grid; }
			set { SetGrid(value); }
		}

		private static int lastTabPageIndex = 0;

		public PropertyForm() : this(null) { }

		public PropertyForm(ReoGridControl grid)
		{
			this.grid = grid;
			
			InitializeComponent();

			SetupUILanguage();

			numberPage.Done += new EventHandler(IPropertyPage_Done);
			
			tabControl1.SelectedIndex = lastTabPageIndex;
		}

		void SetupUILanguage()
		{
			this.Text = LangResource.FormatPage_Caption;

			this.tabFormat.Text = LangResource.Format;
			this.tabProtection.Text = LangResource.Protection;

			this.btnOK.Text = LangResource.Btn_OK;
			this.btnCancel.Text = LangResource.Btn_Cancel;
		}

		void IPropertyPage_Done(object sender, EventArgs e)
		{
			btnOK.PerformClick();
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			if (grid != null)
			{
				SetGrid(grid);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		public void SetGrid(ReoGridControl grid)
		{
			var sheet = this.grid.CurrentWorksheet;

			var cell = (grid == null) ? null : (sheet.GetCell(sheet.SelectionRange.StartPos));

			this.ProcessAllPages(p =>
			{
				p.Grid = grid;
				p.SetupUILanguage();
				p.LoadPage();
			});
		}

		private void ProcessAllPages(Action<IPropertyPage> handler)
		{
			for (int i = 0; i < tabControl1.TabPages.Count; i++)
			{
				foreach (Control ctrl in tabControl1.TabPages[i].Controls)
				{
					if (ctrl is IPropertyPage)
					{
						handler((IPropertyPage)ctrl);
					}
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (grid != null)
			{
				var actionGroup = new WorksheetReusableActionGroup(grid.CurrentWorksheet.SelectionRange);

				this.ProcessAllPages(p =>
				{
					WorksheetReusableAction action = p.CreateUpdateAction();
					if (action != null) actionGroup.Actions.Add(action);
				});

				grid.DoAction(actionGroup);
			}

			Close();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			lastTabPageIndex = tabControl1.SelectedIndex;
		}
	}
}
