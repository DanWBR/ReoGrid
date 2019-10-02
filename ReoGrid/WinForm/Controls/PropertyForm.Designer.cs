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

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.PropertyPages
{
	partial class PropertyForm
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabFormat = new System.Windows.Forms.TabPage();
			this.numberPage = new ReoGrid.PropertyPages.FormatPage();
			this.tabProtection = new System.Windows.Forms.TabPage();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.protectionPage1 = new ReoGrid.Editor.PropertyPages.ProtectionPage();
			this.tabControl1.SuspendLayout();
			this.tabFormat.SuspendLayout();
			this.tabProtection.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabFormat);
			this.tabControl1.Controls.Add(this.tabProtection);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(6, 7);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(573, 383);
			this.tabControl1.TabIndex = 0;
			// 
			// tabFormat
			// 
			this.tabFormat.Controls.Add(this.numberPage);
			this.tabFormat.Location = new System.Drawing.Point(4, 22);
			this.tabFormat.Name = "tabFormat";
			this.tabFormat.Size = new System.Drawing.Size(565, 357);
			this.tabFormat.TabIndex = 1;
			this.tabFormat.Text = "Format";
			this.tabFormat.UseVisualStyleBackColor = true;
			// 
			// numberPage
			// 
			this.numberPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numberPage.Grid = null;
			this.numberPage.Location = new System.Drawing.Point(0, 0);
			this.numberPage.Name = "numberPage";
			this.numberPage.Size = new System.Drawing.Size(565, 357);
			this.numberPage.TabIndex = 0;
			// 
			// tabProtection
			// 
			this.tabProtection.Controls.Add(this.protectionPage1);
			this.tabProtection.Location = new System.Drawing.Point(4, 22);
			this.tabProtection.Name = "tabProtection";
			this.tabProtection.Padding = new System.Windows.Forms.Padding(3);
			this.tabProtection.Size = new System.Drawing.Size(565, 357);
			this.tabProtection.TabIndex = 4;
			this.tabProtection.Text = "Protection";
			this.tabProtection.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnCancel.Location = new System.Drawing.Point(491, 7);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnOK.Location = new System.Drawing.Point(403, 7);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(6, 390);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(10, 7, 2, 0);
			this.panel1.Size = new System.Drawing.Size(573, 30);
			this.panel1.TabIndex = 2;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Enabled = false;
			this.splitter1.Location = new System.Drawing.Point(483, 7);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(8, 23);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// protectionPage1
			// 
			this.protectionPage1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.protectionPage1.Grid = null;
			this.protectionPage1.Location = new System.Drawing.Point(3, 3);
			this.protectionPage1.Name = "protectionPage1";
			this.protectionPage1.Size = new System.Drawing.Size(559, 351);
			this.protectionPage1.TabIndex = 0;
			// 
			// PropertyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(585, 427);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PropertyForm";
			this.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.ShowInTaskbar = false;
			this.Text = "Format Cells";
			this.tabControl1.ResumeLayout(false);
			this.tabFormat.ResumeLayout(false);
			this.tabProtection.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TabPage tabFormat;
		private FormatPage numberPage;
		private System.Windows.Forms.TabPage tabProtection;
		private Editor.PropertyPages.ProtectionPage protectionPage1;
	}
}