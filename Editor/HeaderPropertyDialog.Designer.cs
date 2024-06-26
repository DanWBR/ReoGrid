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

namespace unvell.ReoGrid.Editor
{
	partial class HeaderPropertyDialog
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
            this.labText = new System.Windows.Forms.Label();
            this.txtHeaderText = new System.Windows.Forms.TextBox();
            this.labTextColor = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labCellBody = new System.Windows.Forms.Label();
            this.cmbCellBody = new System.Windows.Forms.ComboBox();
            this.labRowHeaderWidth = new System.Windows.Forms.Label();
            this.numRowHeaderWidth = new System.Windows.Forms.NumericUpDown();
            this.colorComboBox1 = new unvell.UIControls.ColorComboBox();
            this.chkAutoFit = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numRowHeaderWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // labText
            // 
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(72, 19);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(31, 13);
            this.labText.TabIndex = 0;
            this.labText.Text = "Text:";
            this.labText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtHeaderText
            // 
            this.txtHeaderText.Location = new System.Drawing.Point(117, 16);
            this.txtHeaderText.Name = "txtHeaderText";
            this.txtHeaderText.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderText.TabIndex = 1;
            // 
            // labTextColor
            // 
            this.labTextColor.AutoSize = true;
            this.labTextColor.Location = new System.Drawing.Point(45, 50);
            this.labTextColor.Name = "labTextColor";
            this.labTextColor.Size = new System.Drawing.Size(58, 13);
            this.labTextColor.TabIndex = 2;
            this.labTextColor.Text = "Text Color:";
            this.labTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(89, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(170, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // labCellBody
            // 
            this.labCellBody.AutoSize = true;
            this.labCellBody.Location = new System.Drawing.Point(12, 106);
            this.labCellBody.Name = "labCellBody";
            this.labCellBody.Size = new System.Drawing.Size(91, 13);
            this.labCellBody.TabIndex = 4;
            this.labCellBody.Text = "Default Cell Body:";
            this.labCellBody.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCellBody
            // 
            this.cmbCellBody.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCellBody.FormattingEnabled = true;
            this.cmbCellBody.Location = new System.Drawing.Point(117, 102);
            this.cmbCellBody.Name = "cmbCellBody";
            this.cmbCellBody.Size = new System.Drawing.Size(188, 21);
            this.cmbCellBody.TabIndex = 5;
            // 
            // labRowHeaderWidth
            // 
            this.labRowHeaderWidth.AutoSize = true;
            this.labRowHeaderWidth.Location = new System.Drawing.Point(35, 134);
            this.labRowHeaderWidth.Name = "labRowHeaderWidth";
            this.labRowHeaderWidth.Size = new System.Drawing.Size(68, 13);
            this.labRowHeaderWidth.TabIndex = 6;
            this.labRowHeaderWidth.Text = "Panel Width:";
            this.labRowHeaderWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numRowHeaderWidth
            // 
            this.numRowHeaderWidth.Location = new System.Drawing.Point(117, 129);
            this.numRowHeaderWidth.Name = "numRowHeaderWidth";
            this.numRowHeaderWidth.Size = new System.Drawing.Size(64, 20);
            this.numRowHeaderWidth.TabIndex = 7;
            // 
            // colorComboBox1
            // 
            this.colorComboBox1.BackColor = System.Drawing.Color.White;
            this.colorComboBox1.CloseOnClick = true;
            this.colorComboBox1.dropdown = false;
            this.colorComboBox1.Location = new System.Drawing.Point(117, 45);
            this.colorComboBox1.Name = "colorComboBox1";
            this.colorComboBox1.Size = new System.Drawing.Size(188, 23);
            this.colorComboBox1.SolidColor = System.Drawing.Color.Empty;
            this.colorComboBox1.TabIndex = 3;
            this.colorComboBox1.Text = "colorComboBox1";
            // 
            // chkAutoFit
            // 
            this.chkAutoFit.AutoSize = true;
            this.chkAutoFit.Location = new System.Drawing.Point(117, 77);
            this.chkAutoFit.Name = "chkAutoFit";
            this.chkAutoFit.Size = new System.Drawing.Size(90, 17);
            this.chkAutoFit.TabIndex = 10;
            this.chkAutoFit.Text = "Auto fit to cell";
            this.chkAutoFit.UseVisualStyleBackColor = true;
            // 
            // HeaderPropertyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(334, 213);
            this.Controls.Add(this.chkAutoFit);
            this.Controls.Add(this.numRowHeaderWidth);
            this.Controls.Add(this.labRowHeaderWidth);
            this.Controls.Add(this.cmbCellBody);
            this.Controls.Add(this.labCellBody);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.colorComboBox1);
            this.Controls.Add(this.txtHeaderText);
            this.Controls.Add(this.labTextColor);
            this.Controls.Add(this.labText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HeaderPropertyDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Header Properties";
            ((System.ComponentModel.ISupportInitialize)(this.numRowHeaderWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labText;
		private System.Windows.Forms.TextBox txtHeaderText;
		private UIControls.ColorComboBox colorComboBox1;
		private System.Windows.Forms.Label labTextColor;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label labCellBody;
		private System.Windows.Forms.ComboBox cmbCellBody;
		private System.Windows.Forms.Label labRowHeaderWidth;
		private System.Windows.Forms.NumericUpDown numRowHeaderWidth;
		private System.Windows.Forms.CheckBox chkAutoFit;
	}
}