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

using Eto.Forms;
using Eto;
using Eto.Drawing;

namespace unvell.ReoGrid.EtoControls
{
	partial class RenameSheetDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

            var layout = new PixelLayout();

			this.label1 = new Label();
			this.txtName = new TextBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Tag = new Point(12, 32);
			this.label1.Size = new Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Tag = new Point(66, 29);
			this.txtName.Size = new Size(169, 20);
			this.txtName.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.Tag = new Point(241, 27);
			this.btnOK.Size = new Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler<System.EventArgs>(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Tag = new Point(322, 27);
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler<System.EventArgs>(this.btnOK_Click);
			// 
			// RenameSheetDialog
			// 
			this.ClientSize = new Size(350, 50);

			this.Maximizable = false;
			this.Minimizable = false;			
			this.ShowInTaskbar = false;
			this.Title = "Rename Sheet";

            layout.Add(this.btnCancel, (Point)this.btnCancel.Tag);
            layout.Add(this.btnOK, (Point)this.btnOK.Tag);
            layout.Add(this.txtName, (Point)this.txtName.Tag);
            layout.Add(this.label1, (Point)this.label1.Tag);

            this.Content = layout;

            this.ResumeLayout();

		}

		#endregion

		private Label label1;
		private TextBox txtName;
		private Button btnOK;
		private Button btnCancel;
	}
}

#endif // WINFORM