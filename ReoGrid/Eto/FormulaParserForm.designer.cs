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

using Eto.Drawing;
using Eto.Forms;
using unvell.ReoGrid.EtoRenderer;

namespace FormulaParserTest
{
	partial class FormulaParserForm
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
			this.syntaxTree = new TreeGridView();
			this.leftPanel = new PixelLayout();
			this.label1 = new Label();
			this.topPanel = new PixelLayout();
			this.txtErrorMessage = new TextBox();
			this.txtInput = new TextBox();
			this.label2 = new Label();
			this.panel3 = new PixelLayout();
			this.groupConnect = new GroupBox();
			this.txtConnectRight = new TextBox();
			this.label11 = new Label();
			this.txtConnectLeft = new TextBox();
			this.label10 = new Label();
			this.groupFunCall = new GroupBox();
			this.btnRemoveArg = new Button();
			this.btnAddArg = new Button();
			this.lstArgs = new ListBox();
			this.labArguments = new Label();
			this.txtFunName = new TextBox();
			this.label8 = new Label();
			this.groupCell = new GroupBox();
			this.txtCellAddress = new TextBox();
			this.label9 = new Label();
			this.groupName = new GroupBox();
			this.txtName = new TextBox();
			this.label7 = new Label();
			this.groupRange = new GroupBox();
			this.txtRangeAddress = new TextBox();
			this.label5 = new Label();
			this.label6 = new Label();
			this.txtGeneratedFormula = new TextBox();
			this.label3 = new Label();
			this.panel4 = new PixelLayout();
			this.linkComfortable = new LinkButton();
			this.linkCompact = new LinkButton();
			this.chkAfterSubExpr = new CheckBox();
			this.chkBeforeSubExpr = new CheckBox();
			this.linkRestoreToDefault = new LinkButton();
			this.linkClearAll = new LinkButton();
			this.chkSpaceAfterFunctionName = new CheckBox();
			this.chkAfterMinus = new CheckBox();
			this.chkAfterParamList = new CheckBox();
			this.chkBeforePercent = new CheckBox();
			this.chkBeforeParamList = new CheckBox();
			this.chkFunctionNameUppercase = new CheckBox();
			this.chkAfterComma = new CheckBox();
			this.chkBeforeComma = new CheckBox();
			this.chkAfterOperator = new CheckBox();
			this.chkBeforeOperator = new CheckBox();
			this.label4 = new Label();
			this.label12 = new Label();
			this.label13 = new Label();
			this.label16 = new Label();
			this.label15 = new Label();
			this.splitter1 = new Splitter();
			this.splitter2 = new Splitter();
			this.leftPanel.SuspendLayout();
			this.topPanel.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupConnect.SuspendLayout();
			this.groupFunCall.SuspendLayout();
			this.groupCell.SuspendLayout();
			this.groupName.SuspendLayout();
			this.groupRange.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// syntaxTree
			// 
			this.syntaxTree.Tag = new Point(0, 27);
			this.syntaxTree.Size = new Size(387, 458);
			this.syntaxTree.TabIndex = 1;

            syntaxTree.ShowHeader = false;
            syntaxTree.AllowMultipleSelection = false;
            syntaxTree.Columns.Add(new GridColumn { DataCell = new TextBoxCell(0) });

			// 
			// leftPanel
			// 
			this.leftPanel.Add(this.syntaxTree);
			this.leftPanel.Add(this.label1);
			this.leftPanel.Tag = new Point(5, 104);
			this.leftPanel.Size = new Size(387, 485);
			this.leftPanel.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Tag = new Point(0, 0);
			this.label1.Size = new Size(387, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Syntax Tree:";
			// 
			// topPanel
			// 
			this.topPanel.Add(this.txtErrorMessage);
			this.topPanel.Add(this.txtInput);
			this.topPanel.Add(this.label2);
			this.topPanel.Tag = new Point(5, 6);
			this.topPanel.Size = new Size(872, 98);
			this.topPanel.TabIndex = 0;
			// 
			// txtErrorMessage
			// 
			this.txtErrorMessage.Tag = new Point(93, 49);
			this.txtErrorMessage.ReadOnly = true;
			this.txtErrorMessage.Size = new Size(779, 49);
			this.txtErrorMessage.TabIndex = 2;
			// 
			// txtInput
			// 
			this.txtInput.Tag = new Point(93, 0);
			this.txtInput.Size = new Size(779, 49);
			this.txtInput.TabIndex = 1;
			this.txtInput.Text = "A1+ceiling(B2/B3, B4)%-Sum(F1:F5 F4:F6)*-2";
			// 
			// label2
			// 
			this.label2.Tag = new Point(0, 0);
			this.label2.Size = new Size(93, 98);
			this.label2.TabIndex = 0;
			this.label2.Text = "Formula:";
			// 
			// panel3
			// 
			this.panel3.Add(this.groupConnect);
			this.panel3.Add(this.groupFunCall);
			this.panel3.Add(this.groupCell);
			this.panel3.Add(this.groupName);
			this.panel3.Add(this.groupRange);
			this.panel3.Add(this.label6);
			this.panel3.Tag = new Point(396, 104);
			this.panel3.Size = new Size(481, 247);
			this.panel3.TabIndex = 1;
            // 
            // groupConnect
            // 

            var p1 = new PixelLayout();

            this.groupConnect.Content = p1;

            p1.Add(this.txtConnectRight);
            p1.Add(this.label11);
            p1.Add(this.txtConnectLeft);
            p1.Add(this.label10);
			this.groupConnect.Tag = new Point(16, 114);
			this.groupConnect.Size = new Size(210, 98);
			this.groupConnect.TabIndex = 2;
			this.groupConnect.Text = "Connect";
			this.groupConnect.Visible = false;
			// 
			// txtConnectRight
			// 
			this.txtConnectRight.Tag = new Point(85, 55);
			this.txtConnectRight.Size = new Size(100, 21);
			this.txtConnectRight.TabIndex = 3;
			// 
			// label11
			// 
			this.label11.Tag = new Point(34, 58);
			this.label11.Size = new Size(39, 15);
			this.label11.TabIndex = 2;
			this.label11.Text = "Right:";
			// 
			// txtConnectLeft
			// 
			this.txtConnectLeft.Tag = new Point(85, 28);
			this.txtConnectLeft.Size = new Size(100, 21);
			this.txtConnectLeft.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.Tag = new Point(43, 33);
			this.label10.Size = new Size(30, 15);
			this.label10.TabIndex = 0;
			this.label10.Text = "Left:";
            // 
            // groupFunCall
            // 
            var p2 = new PixelLayout();

            this.groupFunCall.Content = p2;

            p2.Add(this.btnRemoveArg);
			p2.Add(this.btnAddArg);
			p2.Add(this.lstArgs);
			p2.Add(this.labArguments);
			p2.Add(this.txtFunName);
			p2.Add(this.label8);
			this.groupFunCall.Tag = new Point(95, 115);
			this.groupFunCall.Size = new Size(363, 140);
			this.groupFunCall.TabIndex = 4;
			this.groupFunCall.Text = "Function Call";
			this.groupFunCall.Visible = false;
			// 
			// btnRemoveArg
			// 
			this.btnRemoveArg.Tag = new Point(279, 93);
			this.btnRemoveArg.Size = new Size(72, 26);
			this.btnRemoveArg.TabIndex = 5;
			this.btnRemoveArg.Text = "Remove";
			// 
			// btnAddArg
			// 
			this.btnAddArg.Tag = new Point(279, 61);
			this.btnAddArg.Size = new Size(72, 26);
			this.btnAddArg.TabIndex = 4;
			this.btnAddArg.Text = "Add";
			// 
			// lstArgs
			// 
			this.lstArgs.Tag = new Point(122, 64);
			this.lstArgs.Size = new Size(151, 94);
			this.lstArgs.TabIndex = 0;
			// 
			// labArguments
			// 
			this.labArguments.Tag = new Point(41, 64);
			this.labArguments.Size = new Size(69, 15);
			this.labArguments.TabIndex = 2;
			this.labArguments.Text = "Arguments:";
			// 
			// txtFunName
			// 
			this.txtFunName.Tag = new Point(122, 29);
			this.txtFunName.Size = new Size(120, 21);
			this.txtFunName.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.Tag = new Point(15, 33);
			this.label8.Size = new Size(94, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "Function Name:";
			// 
			// groupCell
			// 
			this.groupCell.Tag = new Point(22, 165);
			this.groupCell.Size = new Size(210, 70);
			this.groupCell.TabIndex = 0;
			this.groupCell.Text = "Cell";
			this.groupCell.Visible = false;
			// 
			// txtCellAddress
			// 
			this.txtCellAddress.Tag = new Point(85, 28);
			this.txtCellAddress.Size = new Size(100, 21);
			this.txtCellAddress.TabIndex = 1;
			// 
			// label9
			// 
			this.label9.Tag = new Point(17, 32);
			this.label9.Size = new Size(54, 15);
			this.label9.TabIndex = 0;
			this.label9.Text = "Address:";
            // 
            // groupName
            // 
            var p3 = new PixelLayout();

            this.groupName.Content = p3;

            p3.Add(this.txtName);
			p3.Add(this.label7);
			this.groupName.Tag = new Point(238, 38);
			this.groupName.Size = new Size(210, 70);
			this.groupName.TabIndex = 2;
			this.groupName.Text = "Name";
			this.groupName.Visible = false;
			// 
			// txtName
			// 
			this.txtName.Tag = new Point(83, 28);
			this.txtName.Size = new Size(100, 21);
			this.txtName.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.Tag = new Point(15, 32);
			this.label7.Size = new Size(44, 15);
			this.label7.TabIndex = 0;
			this.label7.Text = "Name:";
            // 
            // groupRange
            // 
            var p4 = new PixelLayout();

            this.groupRange.Content = p4;

            p4.Add(this.txtRangeAddress);
			p4.Add(this.label5);
			this.groupRange.Tag = new Point(22, 38);
			this.groupRange.Size = new Size(210, 70);
			this.groupRange.TabIndex = 1;
			this.groupRange.Text = "Range";
			this.groupRange.Visible = false;
			// 
			// txtRangeAddress
			// 
			this.txtRangeAddress.Tag = new Point(85, 28);
			this.txtRangeAddress.Size = new Size(100, 21);
			this.txtRangeAddress.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Tag = new Point(17, 32);
			this.label5.Size = new Size(54, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Address:";
			// 
			// label6
			// 
			this.label6.Tag = new Point(5, 6);
			this.label6.Size = new Size(471, 21);
			this.label6.TabIndex = 0;
			this.label6.Text = "Node Properties:";
			// 
			// txtGeneratedFormula
			// 
			this.txtGeneratedFormula.BackgroundColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.txtGeneratedFormula.Tag = new Point(396, 531);
			this.txtGeneratedFormula.ReadOnly = true;
			this.txtGeneratedFormula.Size = new Size(481, 58);
			this.txtGeneratedFormula.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Tag = new Point(396, 510);
			this.label3.Size = new Size(481, 21);
			this.label3.TabIndex = 3;
			this.label3.Text = "Regenerated Formula:";
			// 
			// panel4
			// 
			this.panel4.BackgroundColor = SystemColors.WindowBackground;
			this.panel4.Add(this.linkComfortable);
			this.panel4.Add(this.linkCompact);
			this.panel4.Add(this.chkAfterSubExpr);
			this.panel4.Add(this.chkBeforeSubExpr);
			this.panel4.Add(this.linkRestoreToDefault);
			this.panel4.Add(this.linkClearAll);
			this.panel4.Add(this.chkSpaceAfterFunctionName);
			this.panel4.Add(this.chkAfterMinus);
			this.panel4.Add(this.chkAfterParamList);
			this.panel4.Add(this.chkBeforePercent);
			this.panel4.Add(this.chkBeforeParamList);
			this.panel4.Add(this.chkFunctionNameUppercase);
			this.panel4.Add(this.chkAfterComma);
			this.panel4.Add(this.chkBeforeComma);
			this.panel4.Add(this.chkAfterOperator);
			this.panel4.Add(this.chkBeforeOperator);
			this.panel4.Add(this.label4);
			this.panel4.Add(this.label12);
			this.panel4.Add(this.label13);
			this.panel4.Add(this.label16);
			this.panel4.Add(this.label15);
			this.panel4.Tag = new Point(396, 351);
			this.panel4.Size = new Size(481, 156);
			this.panel4.TabIndex = 2;
			// 
			// linkComfortable
			// 
			this.linkComfortable.Tag = new Point(265, 2);
			this.linkComfortable.Size = new Size(63, 13);
			this.linkComfortable.TabIndex = 18;
			this.linkComfortable.Text = "Comfortable";
			// 
			// linkCompact
			// 
			this.linkCompact.Tag = new Point(210, 2);
			this.linkCompact.Size = new Size(49, 13);
			this.linkCompact.TabIndex = 17;
			this.linkCompact.Text = "Compact";
			// 
			// chkAfterSubExpr
			// 
			this.chkAfterSubExpr.Tag = new Point(29, 127);
			this.chkAfterSubExpr.Size = new Size(174, 19);
			this.chkAfterSubExpr.TabIndex = 11;
			this.chkAfterSubExpr.Text = "Space after sub expression";
			// 
			// chkBeforeSubExpr
			// 
			this.chkBeforeSubExpr.Tag = new Point(29, 106);
			this.chkBeforeSubExpr.Size = new Size(185, 19);
			this.chkBeforeSubExpr.TabIndex = 10;
			this.chkBeforeSubExpr.Text = "Space before sub expression";
			// 
			// linkRestoreToDefault
			// 
			this.linkRestoreToDefault.Tag = new Point(160, 2);
			this.linkRestoreToDefault.Size = new Size(41, 13);
			this.linkRestoreToDefault.TabIndex = 9;
			this.linkRestoreToDefault.Text = "Default";
			// 
			// linkClearAll
			// 
			this.linkClearAll.Tag = new Point(121, 2);
			this.linkClearAll.Size = new Size(33, 13);
			this.linkClearAll.TabIndex = 8;
			this.linkClearAll.Text = "None";
			// 
			// chkSpaceAfterFunctionName
			// 
			this.chkSpaceAfterFunctionName.Tag = new Point(251, 106);
			this.chkSpaceAfterFunctionName.Size = new Size(169, 19);
			this.chkSpaceAfterFunctionName.TabIndex = 6;
			this.chkSpaceAfterFunctionName.Text = "Space after function name";
			// 
			// chkAfterMinus
			// 
			this.chkAfterMinus.Tag = new Point(251, 84);
			this.chkAfterMinus.Size = new Size(125, 19);
			this.chkAfterMinus.TabIndex = 3;
			this.chkAfterMinus.Text = "Space after minus";
			// 
			// chkAfterParamList
			// 
			this.chkAfterParamList.Tag = new Point(29, 84);
			this.chkAfterParamList.Size = new Size(166, 19);
			this.chkAfterParamList.TabIndex = 3;
			this.chkAfterParamList.Text = "Space after parameter list";
			// 
			// chkBeforePercent
			// 
			this.chkBeforePercent.Tag = new Point(251, 63);
			this.chkBeforePercent.Size = new Size(143, 19);
			this.chkBeforePercent.TabIndex = 2;
			this.chkBeforePercent.Text = "Space before percent";
			// 
			// chkBeforeParamList
			// 
			this.chkBeforeParamList.Tag = new Point(29, 63);
			this.chkBeforeParamList.Size = new Size(177, 19);
			this.chkBeforeParamList.TabIndex = 2;
			this.chkBeforeParamList.Text = "Space before parameter list";
			// 
			// chkFunctionNameUppercase
			// 
			this.chkFunctionNameUppercase.Tag = new Point(251, 127);
			this.chkFunctionNameUppercase.Size = new Size(192, 19);
			this.chkFunctionNameUppercase.TabIndex = 7;
			this.chkFunctionNameUppercase.Text = "Auto function name uppercase";
			// 
			// chkAfterComma
			// 
			this.chkAfterComma.Tag = new Point(251, 42);
			this.chkAfterComma.Size = new Size(133, 19);
			this.chkAfterComma.TabIndex = 5;
			this.chkAfterComma.Text = "Space after comma";
			// 
			// chkBeforeComma
			// 
			this.chkBeforeComma.Tag = new Point(251, 21);
			this.chkBeforeComma.Size = new Size(144, 19);
			this.chkBeforeComma.TabIndex = 4;
			this.chkBeforeComma.Text = "Space before comma";
			// 
			// chkAfterOperator
			// 
			this.chkAfterOperator.Tag = new Point(29, 42);
			this.chkAfterOperator.Size = new Size(137, 19);
			this.chkAfterOperator.TabIndex = 1;
			this.chkAfterOperator.Text = "Space after operator";
			// 
			// chkBeforeOperator
			// 
			this.chkBeforeOperator.Tag = new Point(29, 21);
			this.chkBeforeOperator.Size = new Size(148, 19);
			this.chkBeforeOperator.TabIndex = 0;
			this.chkBeforeOperator.Text = "Space before operator";
			// 
			// label4
			// 
			this.label4.Tag = new Point(0, 0);
			this.label4.Size = new Size(479, 21);
			this.label4.TabIndex = 7;
			this.label4.Text = "Formula Styles:";
			// 
			// label12
			// 
			this.label12.Tag = new Point(10, 23);
			this.label12.Size = new Size(20, 29);
			this.label12.TabIndex = 12;
			this.label12.Text = "[";
			// 
			// label13
			// 
			this.label13.Tag = new Point(231, 22);
			this.label13.Size = new Size(20, 29);
			this.label13.TabIndex = 13;
			this.label13.Text = "[";
			// 
			// label16
			// 
			this.label16.Tag = new Point(10, 108);
			this.label16.Size = new Size(20, 29);
			this.label16.TabIndex = 16;
			this.label16.Text = "[";
			// 
			// label15
			// 
			this.label15.Tag = new Point(10, 65);
			this.label15.Size = new Size(20, 29);
			this.label15.TabIndex = 15;
			this.label15.Text = "[";
			// 
			// splitter1
			// 
			this.splitter1.Tag = new Point(392, 104);
			this.splitter1.Size = new Size(4, 485);
			this.splitter1.TabIndex = 8;
			// 
			// splitter2
			// 
			this.splitter2.Tag = new Point(396, 507);
			this.splitter2.Size = new Size(481, 3);
			this.splitter2.TabIndex = 22;
            // 
            // FormulaParserForm
            // 

            var p = new PixelLayout();

            this.Content = p;

			this.ClientSize = new Size(882, 595);
            p.Add(this.panel3);
            p.Add(this.panel4);
            p.Add(this.splitter2);
            p.Add(this.label3);
            p.Add(this.txtGeneratedFormula);
            p.Add(this.splitter1);
            p.Add(this.leftPanel);
            p.Add(this.topPanel);
			this.Padding = new Padding(5, 6, 5, 6);
			this.Title = "Formula Syntax Parser";
			this.leftPanel.ResumeLayout();
			this.topPanel.ResumeLayout();
			this.panel3.ResumeLayout();
			this.groupConnect.ResumeLayout();
			this.groupFunCall.ResumeLayout();
			this.groupCell.ResumeLayout();
			this.groupName.ResumeLayout();
			this.groupRange.ResumeLayout();
			this.panel4.ResumeLayout();
			this.ResumeLayout();

		}

#endregion

		private TreeGridView syntaxTree;
		private PixelLayout leftPanel;
		private Label label1;
		private PixelLayout topPanel;
		private TextBox txtInput;
		private Label label2;
		private PixelLayout panel3;
		private Label label6;
		private TextBox txtGeneratedFormula;
		private Label label3;
		private PixelLayout panel4;
		private CheckBox chkAfterComma;
		private CheckBox chkBeforeComma;
		private CheckBox chkAfterOperator;
		private CheckBox chkBeforeOperator;
		private Label label4;
		private Splitter splitter1;
		private CheckBox chkAfterParamList;
		private CheckBox chkBeforeParamList;
		private CheckBox chkFunctionNameUppercase;
		private CheckBox chkSpaceAfterFunctionName;
		private GroupBox groupFunCall;
		private Label labArguments;
		private TextBox txtFunName;
		private Label label8;
		private GroupBox groupCell;
		private TextBox txtCellAddress;
		private Label label9;
		private GroupBox groupName;
		private TextBox txtName;
		private Label label7;
		private GroupBox groupRange;
		private TextBox txtRangeAddress;
		private Label label5;
		private Splitter splitter2;
		private TextBox txtErrorMessage;
		private LinkButton linkRestoreToDefault;
		private LinkButton linkClearAll;
		private Button btnRemoveArg;
		private Button btnAddArg;
		private ListBox lstArgs;
		private CheckBox chkAfterSubExpr;
		private CheckBox chkBeforeSubExpr;
		private GroupBox groupConnect;
		private TextBox txtConnectRight;
		private Label label11;
		private TextBox txtConnectLeft;
		private Label label10;
		private CheckBox chkAfterMinus;
		private CheckBox chkBeforePercent;
		private Label label12;
		private Label label13;
		private Label label16;
		private Label label15;
		private LinkButton linkComfortable;
		private LinkButton linkCompact;
	}
}

#endif // DEBUG && WINFORM && FORMULA
