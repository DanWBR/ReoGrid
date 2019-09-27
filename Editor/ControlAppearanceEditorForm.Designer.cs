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
 * ReoGrid and ReoGridEditor is released under MIT license.
 *
 * Copyright (c) 2012-2016 Jing <lujing at unvell.com>
 * Copyright (c) 2012-2016 unvell.com, all rights reserved.
 * 
 ****************************************************************************/

using Eto.Forms;
using Eto.Drawing;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoRenderer;

namespace unvell.ReoGrid.Editor
{
	partial class ControlAppearanceEditorForm
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

            this.Content = layout;

			this.btnExportCSharp = new Button();
			this.lstColors = new ListBox();
			this.btnReset = new Button();
			this.chkUseSystemHighlight = new CheckBox();
			this.labHighligh = new Label();
			this.labMain = new Label();
			this.numSelectionWidth = new NumericUpDown();
			this.labSelectionBorderWidth = new Label();
			this.labPixel = new Label();
			this.btnExportVBNET = new Button();
			this.highlightColorPickerControl = new  Eto.Forms.ColorDialog();
			this.mainColorPickerControl = new Eto.Forms.ColorDialog();
            this.colorPickerControl = new Eto.Forms.ColorPicker();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionWidth)).BeginInit();
			this.SuspendLayout();
			// 
			// btnExportCSharp
			// 
			this.btnExportCSharp.Tag = new Point(280, 166);
			this.btnExportCSharp.Name = "btnExportCSharp";
			this.btnExportCSharp.Size = new Size(128, 28);
			this.btnExportCSharp.TabIndex = 7;
			this.btnExportCSharp.Text = "Export C#";
			this.btnExportCSharp.UseVisualStyleBackColor = true;
			this.btnExportCSharp.Click += new System.EventHandler<System.EventArgs>(this.btnExportCSharp_Click);
			// 
			// lstColors
			// 
			this.lstColors.FormattingEnabled = true;
			this.lstColors.Tag = new Point(13, 54);
			this.lstColors.Name = "lstColors";
			this.lstColors.Size = new Size(231, 147);
			this.lstColors.TabIndex = 0;
			// 
			// btnReset
			// 
			this.btnReset.Tag = new Point(280, 131);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new Size(128, 29);
			this.btnReset.TabIndex = 10;
			this.btnReset.Text = "Reset to Default";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler<System.EventArgs>(this.btnReset_Click);
			// 
			// chkUseSystemHighlight
			// 
			this.chkUseSystemHighlight.AutoSize = true;
			this.chkUseSystemHighlight.Tag = new Point(265, 20);
			this.chkUseSystemHighlight.Name = "chkUseSystemHighlight";
			this.chkUseSystemHighlight.Size = new Size(153, 17);
			this.chkUseSystemHighlight.TabIndex = 9;
			this.chkUseSystemHighlight.Text = "Use System Highlight Color";
			this.chkUseSystemHighlight.UseVisualStyleBackColor = true;
			this.chkUseSystemHighlight.CheckedChanged += new System.EventHandler<System.EventArgs>(this.chkUseSystemHighlight_CheckedChanged);
			// 
			// labHighligh
			// 
			this.labHighligh.AutoSize = true;
			this.labHighligh.Tag = new Point(135, 22);
			this.labHighligh.Name = "labHighligh";
			this.labHighligh.Size = new Size(54, 13);
			this.labHighligh.TabIndex = 7;
			this.labHighligh.Text = "Highlight: ";
			// 
			// labMain
			// 
			this.labMain.AutoSize = true;
			this.labMain.Tag = new Point(10, 22);
			this.labMain.Name = "labMain";
			this.labMain.Size = new Size(36, 13);
			this.labMain.TabIndex = 5;
			this.labMain.Text = "Main: ";
			// 
			// numSelectionWidth
			// 
			this.numSelectionWidth.Tag = new Point(297, 81);
			this.numSelectionWidth.Name = "numSelectionWidth";
			this.numSelectionWidth.Size = new Size(54, 20);
			this.numSelectionWidth.TabIndex = 11;
            this.numSelectionWidth.Value = 1.0;

			// 
			// labSelectionBorderWidth
			// 
			this.labSelectionBorderWidth.AutoSize = true;
			this.labSelectionBorderWidth.Tag = new Point(281, 59);
			this.labSelectionBorderWidth.Name = "labSelectionBorderWidth";
			this.labSelectionBorderWidth.Size = new Size(119, 13);
			this.labSelectionBorderWidth.TabIndex = 12;
			this.labSelectionBorderWidth.Text = "Selection Border Width:";
			// 
			// labPixel
			// 
			this.labPixel.AutoSize = true;
			this.labPixel.Tag = new Point(357, 87);
			this.labPixel.Name = "labPixel";
			this.labPixel.Size = new Size(29, 13);
			this.labPixel.TabIndex = 13;
			this.labPixel.Text = "Pixel";
			// 
			// btnExportVBNET
			// 
			this.btnExportVBNET.Tag = new Point(280, 200);
			this.btnExportVBNET.Name = "btnExportVBNET";
			this.btnExportVBNET.Size = new Size(128, 28);
			this.btnExportVBNET.TabIndex = 14;
			this.btnExportVBNET.Text = "Export VB.NET";
			this.btnExportVBNET.UseVisualStyleBackColor = true;
			this.btnExportVBNET.Click += new System.EventHandler(this.btnExportVBNET_Click);
			// 
			// highlightColorPickerControl
			// 
			this.highlightColorPickerControl.Tag = new Point(204, 17);
			this.highlightColorPickerControl.Name = "highlightColorPickerControl";
			this.highlightColorPickerControl.ShowShadow = false;
			this.highlightColorPickerControl.Size = new Size(40, 23);
			this.highlightColorPickerControl.SolidColor = Color.YellowGreen;
			this.highlightColorPickerControl.TabIndex = 8;
			this.highlightColorPickerControl.Text = "colorPickerControl3";
			this.highlightColorPickerControl.ColorPicked += new System.EventHandler(this.highlightColorPickerControl_ColorPicked);
			// 
			// mainColorPickerControl
			// 
			this.mainColorPickerControl.Tag = new Point(74, 17);
			this.mainColorPickerControl.Name = "mainColorPickerControl";
			this.mainColorPickerControl.ShowShadow = false;
			this.mainColorPickerControl.Size = new Size(40, 23);
			this.mainColorPickerControl.SolidColor = Color.Silver;
			this.mainColorPickerControl.TabIndex = 6;
			this.mainColorPickerControl.Text = "colorPickerControl2";
			this.mainColorPickerControl. += new System.EventHandler(this.mainColorPickerControl_ColorPicked);
			// 
			// colorPickerControl
			// 
			this.colorPickerControl.Tag = new Point(13, 207);
			this.colorPickerControl.Size = new Size(231, 23);
			this.colorPickerControl.TabIndex = 1;
			// 
			// ControlAppearanceEditorForm
			// 
			this.ClientSize = new Size(439, 244);
			layout.Add(this.btnExportVBNET);
			layout.Add(this.labPixel);
			layout.Add(this.labSelectionBorderWidth);
			layout.Add(this.numSelectionWidth);
			layout.Add(this.btnExportCSharp);
			layout.Add(this.highlightColorPickerControl);
			layout.Add(this.chkUseSystemHighlight);
			layout.Add(this.labHighligh);
			layout.Add(this.btnReset);
			layout.Add(this.mainColorPickerControl);
			layout.Add(this.labMain);
			layout.Add(this.colorPickerControl);
			layout.Add(this.lstColors);
			this.Maximizable = false;
			this.Minimizable = false;
			this.Title = "Control Appearance Editor";
			this.ResumeLayout();

		}

		#endregion

		private ListBox lstColors;
		private CheckBox chkUseSystemHighlight;
		private Eto.Forms.ColorPicker highlightColorPickerControl;
		private Label labHighligh;
		private Eto.Forms.ColorPicker mainColorPickerControl;
		private Label labMain;
		private Button btnExportCSharp;
		private Button btnReset;
		private Eto.Forms.ColorPicker colorPickerControl;
		private NumericUpDown numSelectionWidth;
		private Label labSelectionBorderWidth;
		private Label labPixel;
		private Button btnExportVBNET;
	}
}