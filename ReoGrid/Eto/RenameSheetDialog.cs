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
 * Author: Jing <lujing at unvell.com>
 *
 * Copyright (c) 2012-2016 Jing <lujing at unvell.com>
 * Copyright (c) 2012-2016 unvell.com, all rights reserved.
 * 
 ****************************************************************************/

#if ETO

using System;
using Eto.Forms;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoControls
{
	/// <summary>
	/// Represents the sheet rename dialog
	/// </summary>
	public partial class RenameSheetDialog : Dialog<bool>
	{
		/// <summary>
		/// Name of sheet
		/// </summary>
		public string SheetName { get; set; }

		/// <summary>
		/// Create dialog
		/// </summary>
		public RenameSheetDialog()
		{

			InitializeComponent();

			this.Title = LanguageResource.Sheet_RenameDialog_Title;
			label1.Text = LanguageResource.Sheet_RenameDialog_NameLabel;
			btnOK.Text = LanguageResource.Button_OK;
			btnCancel.Text = LanguageResource.Button_Cancel;

			this.txtName.KeyDown += (s, e) =>
			{
				if (e.Key == Keys.Enter)
				{
					btnOK.PerformClick();
				}
			};
		}

		/// <summary>
		/// Event when dialog was loaded
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.txtName.Text = this.SheetName;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.SheetName = this.txtName.Text;
            Result = sender.Equals(btnOK);
			Close();
		}

	}
}

#endif // WINFORM