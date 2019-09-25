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
using Eto.Drawing;
using Eto.Forms;
using System.Text;
using System.Diagnostics;

using unvell.Common;
using unvell.ReoGrid.Views;
using unvell.ReoGrid.Rendering;
using unvell.ReoGrid.Graphics;
using unvell.ReoGrid.Interaction;
using unvell.ReoGrid.Main;

using DrawMode = unvell.ReoGrid.Rendering.DrawMode;
using Rectangle = unvell.ReoGrid.Graphics.Rectangle;

using Cursor = Eto.Forms.Mouse;

using Point = Eto.Drawing.Point;
using Size = Eto.Drawing.Size;
using WFRect = Eto.Drawing.Rectangle;
using unvell.ReoGrid.EtoRenderer;

namespace unvell.ReoGrid
{
    /// <summary>
    /// ReoGrid - .NET Spreadsheet Component for Windows Form
    /// </summary>

    public partial class ReoGridControl : Eto.Forms.Drawable, IVisualWorkbook, IContextMenuControl, IPersistenceWorkbook, IActionControl, IWorkbook, IScrollableWorksheetContainer
    {

        #region Constructor, Init & Dispose

        private EtoControlAdapter adapter;

        public Drawable InnerControl;

        //private Graphics canvasGraphics;
        /// <summary>
        /// Create component instance.
        /// </summary>
        public ReoGridControl()
        {
            SuspendLayout();

            #region Sheet tabs

            this.sheetTab = new SheetTabControl(this)
            {
                Width = 400,
                Height = 26,
                Position = SheetTabControlPosition.Bottom
            };

            this.sheetTab.MouseMove += (s, e) => this.Cursor = Cursors.Default;

            this.sheetTab.SplitterMoving += (s, e) =>
            {
                //var p = this.bottomPanel.PointFromScreen(Eto.Forms.Mouse.Position);
                //int newWidth = (int)p.X - (int)this.sheetTab.Bounds.Left;

                //if (newWidth < 50)
                //{
                //    newWidth = 50;
                //}

                this.sheetTab.Width = 50;
            };

            this.sheetTab.SheetListClick += (s, e) =>
            {
                if (this.sheetListMenu == null)
                {
                    this.sheetListMenu = new ContextMenu();

                    this.sheetListMenu.Items.Clear();

                    foreach (var sheet in this.Worksheets)
                    {
                        var sheetMenuItem = new ButtonMenuItem() { Text = sheet.Name, Tag = sheet };
                        sheetMenuItem.Click += sheetMenuItem_Click;
                        this.sheetListMenu.Items.Add(sheetMenuItem);
                    }
                }

                //var p = Mouse.Position;
                this.sheetListMenu.Show(this);
            };

            this.sheetTab.TabMouseDown += (s, e) =>
            {
                if (e.MouseButtons == unvell.ReoGrid.Interaction.MouseButtons.Right)
                {
                    if (this.sheetContextMenu == null)
                    {
                        if (this.SheetTabContextMenu != null)
                        {
                            this.sheetContextMenu = this.SheetTabContextMenu;
                        }
                        else
                        {
                            this.sheetContextMenu = new ContextMenu();

                            #region Add sheet context menu items
                            var insertSheetMenu = new ButtonMenuItem { Text = LanguageResource.Menu_InsertSheet };
                            insertSheetMenu.Click += (ss, ee) =>
                            {
                                var sheet = this.CreateWorksheet();
                                if (sheet != null)
                                {
                                    this.workbook.InsertWorksheet(this.sheetTab.SelectedIndex, sheet);
                                    this.CurrentWorksheet = sheet;
                                }
                            };

                            var deleteSheetMenu = new ButtonMenuItem { Text = LanguageResource.Menu_DeleteSheet };
                            deleteSheetMenu.Click += (ss, ee) =>
                            {
                                if (this.workbook.WorksheetCount > 1)
                                {
                                    this.workbook.RemoveWorksheet(this.sheetTab.SelectedIndex);
                                }
                            };

                            var renameSheetMenu = new ButtonMenuItem { Text = LanguageResource.Menu_RenameSheet };
                            renameSheetMenu.Click += (ss, ee) =>
                            {
                                //var index = this.sheetTab.SelectedIndex;
                                //if (index >= 0 && index < this.workbook.worksheets.Count)
                                //{
                                //    var sheet = this.workbook.worksheets[this.sheetTab.SelectedIndex];
                                //    if (sheet != null)
                                //    {
                                //        using (var rsd = new unvell.ReoGrid.EtoRenderer.RenameSheetDialog())
                                //        {
                                //            var rect = this.sheetTab.GetItemBounds(this.sheetTab.SelectedIndex);

                                //            var p = this.sheetTab.PointToScreen(rect.Location);
                                //            p.X -= (rsd.Width - rect.Width) / 2;
                                //            p.Y -= rsd.Height + 5;

                                //            rsd.Location = p;
                                //            rsd.SheetName = sheet.Name;

                                //            if (rsd.ShowDialog() == DialogResult.OK)
                                //            {
                                //                sheet.Name = rsd.SheetName;
                                //            }
                                //        }
                                //    }
                                //}
                            };

                            this.sheetContextMenu.Items.Add(insertSheetMenu);
                            this.sheetContextMenu.Items.Add(deleteSheetMenu);
                            this.sheetContextMenu.Items.Add(renameSheetMenu);
                            #endregion // Add sheet context menu items
                        }
                    }

                    this.sheetContextMenu.Show(this.sheetTab);
                }
            };


            #endregion // Sheet tabs

            this.InitControl();

            ResumeLayout();

            this.adapter = new EtoControlAdapter(this);

            this.editTextbox = new InputTextBox(this) { Visible = false, TextWrap = true };
            this.adapter.editTextbox = this.editTextbox;

            this.InitWorkbook(null);

        }

        void canvasElements_VisibleChanged(object sender, EventArgs e)
        {
            if (this.currentWorksheet != null)
            {
                this.currentWorksheet.UpdateViewportControllBounds();
            }
        }

        /// <summary>
        /// Release resources used in this component.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources;
        /// False to release only unmanaged resources.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    var gdiRenderer = (GDIRenderer)renderer;

        //    if (gdiRenderer != null)
        //    {
        //        gdiRenderer.Dispose();
        //    }

        //    base.Dispose(disposing);

        //    this.workbook.Dispose();

        //    if (builtInCellsSelectionCursor != null) builtInCellsSelectionCursor.Dispose();
        //    if (defaultPickRangeCursor != null) defaultPickRangeCursor.Dispose();
        //    if (builtInFullColSelectCursor != null) builtInFullColSelectCursor.Dispose();
        //    if (builtInFullRowSelectCursor != null) builtInFullRowSelectCursor.Dispose();
        //}

        #endregion // Constructor

        #region Adapter

        private class EtoControlAdapter : IControlAdapter//, IPlatformDependencyInterface
        {
            private readonly ReoGridControl control;
            internal InputTextBox editTextbox;

            private Scrollable ScrollableParent;

            public EtoControlAdapter(ReoGridControl control)
            {
                this.control = control;
                this.ScrollableParent = (Scrollable)control.Parent;
            }

            #region Scroll
            public int ScrollBarHorizontalMaximum
            {
                get { return 1000; }
                set { }
            }

            public int ScrollBarHorizontalMinimum
            {
                get { return 0; }
                set { }
            }

            public int ScrollBarHorizontalValue
            {
                get { return ScrollableParent.ScrollPosition.X; }
                set { ScrollableParent.ScrollPosition = new Point(value, ScrollableParent.ScrollPosition.Y); }
            }

            public int ScrollBarHorizontalLargeChange
            {
                get { return 50; }
                set { }
            }

            public int ScrollBarVerticalMaximum
            {
                get { return 1000; }
                set { }
            }

            public int ScrollBarVerticalMinimum
            {
                get { return 0; }
                set { }
            }

            public int ScrollBarVerticalValue
            {
                get { return ScrollableParent.ScrollPosition.Y; }
                set { ScrollableParent.ScrollPosition = new Point(ScrollableParent.ScrollPosition.X, value); }
            }

            public int ScrollBarVerticalLargeChange
            {
                get { return 50; }
                set { }
            }

            #endregion

            #region Cursor & Context Menu
            public void ShowContextMenuStrip(ViewTypes viewType, Graphics.Point containerLocation)
            {
                Point p = Point.Round(new PointF(containerLocation.X, containerLocation.Y));

                switch (viewType)
                {
                    default:
                    case ViewTypes.Cells:
                        if (this.control.ContextMenu != null)
                            this.control.ContextMenu.Show(this.control);
                        break;

                    case ViewTypes.ColumnHeader:
                        if (this.control.columnHeaderContextMenu != null)
                            this.control.columnHeaderContextMenu.Show(this.control);
                        break;

                    case ViewTypes.RowHeader:
                        if (this.control.rowHeaderContextMenu != null)
                            this.control.rowHeaderContextMenu.Show(this.control);
                        break;

                    case ViewTypes.LeadHeader:
                        if (this.control.leadHeaderContextMenu != null)
                            this.control.leadHeaderContextMenu.Show(this.control);
                        break;
                }
            }

            private Eto.Forms.Cursor oldCursor = null;
            public void ChangeCursor(CursorStyle cursor)
            {

                oldCursor = this.control.Cursor;

                switch (cursor)
                {
                    default:
                    case CursorStyle.PlatformDefault: this.control.Cursor = Cursors.Default; break;
                    case CursorStyle.Selection: this.control.Cursor = Cursors.IBeam; break;
                    case CursorStyle.Hand: this.control.Cursor = Cursors.Pointer; break;
                }
            }

            public void RestoreCursor()
            {
                if (this.oldCursor != null)
                {
                    this.control.Cursor = this.oldCursor;
                }
            }

            public void ChangeSelectionCursor(CursorStyle cursor)
            {

            }

            #endregion // Cursor & Context Menu

            #region Edit Control
            public void ShowEditControl(Graphics.Rectangle bounds, Cell cell)
            {
                var rect = new WFRect((int)Math.Round(bounds.Left), (int)Math.Round(bounds.Top),
                    (int)Math.Round(bounds.Width), (int)Math.Round(bounds.Height));

                editTextbox.SuspendLayout();
                editTextbox.TextWrap = cell.IsMergedCell || cell.InnerStyle.TextWrapMode != TextWrapMode.NoWrap;
                editTextbox.InitialSize = rect.Size;
                editTextbox.VAlign = cell.InnerStyle.VAlign;
                editTextbox.Font = cell.RenderFont;
                //editTextbox.ForeColor = cell.InnerStyle.TextColor;
                //editTextbox.BackColor = cell.InnerStyle.HasStyle(PlainStyleFlag.BackColor)
                //    ? cell.InnerStyle.BackColor : this.control.ControlStyle[ControlAppearanceColors.GridBackground];
                editTextbox.ResumeLayout();
                editTextbox.Visible = true;
                editTextbox.Focus();
            }

            public void HideEditControl()
            {
                editTextbox.Visible = false;
            }

            public void SetEditControlText(string text)
            {
                bool sameBeforeChange = editTextbox.Text == text;

                editTextbox.Text = text;

                if (sameBeforeChange)
                {
                    this.control.currentWorksheet.RaiseCellEditTextChanging(text);
                }
            }

            public string GetEditControlText()
            {
                return editTextbox.Text;
            }

            public void EditControlSelectAll()
            {
                this.editTextbox.SelectAll();
            }

            public void SetEditControlCaretPos(int pos)
            {
                this.editTextbox.CaretIndex = pos;
            }

            public void SetEditControlAlignment(ReoGridHorAlign align)
            {
                switch (align)
                {
                    default:
                    case ReoGridHorAlign.Left:
                        this.editTextbox.TextAlignment = TextAlignment.Left;
                        break;

                    case ReoGridHorAlign.Center:
                        this.editTextbox.TextAlignment = TextAlignment.Center;
                        break;

                    case ReoGridHorAlign.Right:
                        this.editTextbox.TextAlignment = TextAlignment.Right;
                        break;
                }
            }

            public void EditControlApplySystemMouseDown()
            {
                //PointF p = Mouse.Position;

                //PointF p2 = this.control.PointToScreen(this.editTextbox.Location);
                //p.X -= p2.X;
                //p.Y -= p2.Y;

                //try
                //{
                //    Win32.SendMessage(this.editTextbox.Handle, (uint)Win32.WMessages.WM_LBUTTONDOWN, new IntPtr(0), new IntPtr(Win32.CreateLParamPoint(p.X, p.Y)));
                //    Win32.SendMessage(this.editTextbox.Handle, (uint)Win32.WMessages.WM_LBUTTONUP, new IntPtr(0), new IntPtr(Win32.CreateLParamPoint(p.X, p.Y)));
                //}
                //catch { }
            }

            public void EditControlCopy()
            {
                Clipboard.Instance.Text = this.editTextbox.Text;
            }

            public void EditControlPaste()
            {
                this.editTextbox.Text = Clipboard.Instance.Text;
            }

            public void EditControlCut()
            {
                Clipboard.Instance.Text = this.editTextbox.Text;
                this.editTextbox.Text = "";
            }

            public void EditControlUndo()
            {
                //this.editTextbox.;
            }
            #endregion // Edit Control

            #region Control Owner

            public IVisualWorkbook ControlInstance { get { return this.control; } }

            public ControlAppearanceStyle ControlStyle { get { return this.control.controlStyle; } }

            public float BaseScale { get { return 0f; } }
            public float MinScale { get { return 0.1f; } }
            public float MaxScale { get { return 4f; } }

            public ISheetTabControl SheetTabControl
            {
                get { return this.control.sheetTab; }
            }

            public Rectangle GetContainerBounds()
            {
                int width = this.control.Bounds.Width;
                int height = this.control.Bounds.Height;

                if (width < 0) width = 0;
                if (height < 0) height = 0;

                return new Rectangle(0, 0, width, height);
            }

            public IRenderer Renderer { get { return this.control.Renderer; } }

            public void Invalidate()
            {
                this.control.Invalidate();
            }

            public void Focus()
            {
                if (!this.control.HasFocus) this.control.Focus();
            }

            public void ChangeBackgroundColor(SolidColor color)
            {
                this.control.BackgroundColor = color.ToEto();
            }

            public bool IsVisible { get { return this.control.Visible; } }
            #endregion Control Owner

            #region Timer
            private System.Threading.Timer dispatchTimer = null;

            public void StartTimer()
            {
                if (dispatchTimer == null)
                {
                    dispatchTimer = new System.Threading.Timer(TimerRun);
                }

                this.dispatchTimer.Change(100, 150);
            }

            public void StopTimer()
            {
                this.dispatchTimer.Change(0, 0);
            }

            /// <summary>
            /// Threading to update frames of focus highlighted range
            /// </summary>
            /// <param name="state"></param>
            public void TimerRun(object state)
            {
                this.control.currentWorksheet.TimerRun();
            }
            #endregion

            #region IEditableControlInterface Members

            public int GetEditControlCaretPos()
            {
                return this.editTextbox.Selection.Start;
            }

            public int GetEditControlCaretLine()
            {
                return this.editTextbox.CaretIndex;
            }

            #endregion

            #region IControlAdapter Members

            public Graphics.Point PointToScreen(Graphics.Point p)
            {
                var ep = this.control.PointToScreen(p.ToEto());
                return new Graphics.Point(ep.X, ep.Y);
            }

            public void ShowTooltip(Graphics.Point point, string content)
            {
            }

            #endregion // IControlAdapter Members
        }

        #endregion // Adapter

        #region Mouse

        /// <summary>
        /// Overrides mouse-down events
        /// </summary>
        /// <param name="e">Argument of mouse pressing event.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();

            this.OnWorksheetMouseDown(e.Location, (unvell.ReoGrid.Interaction.MouseButtons)e.Buttons);

            //this.Capture = true;
        }

        /// <summary>
        /// Overrides mouse-move events
        /// </summary>
        /// <param name="e">Argument of mouse moving event.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.OnWorksheetMouseMove((Point)e.Location, (unvell.ReoGrid.Interaction.MouseButtons)e.Buttons);
        }

        /// <summary>
        /// Overrides mouse-up events
        /// </summary>
        /// <param name="e">Argument of mouse release event.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.OnWorksheetMouseUp((Point)e.Location, (unvell.ReoGrid.Interaction.MouseButtons)e.Buttons);

            //this.Capture = false;
        }

        /// <summary>
        /// Overrides mouse-wheel events.
        /// </summary>
        /// <param name="e">Argument of mouse wheel event.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.currentWorksheet.OnMouseWheel(new Graphics.Point(e.Location.X, e.Location.Y), (int)e.Delta.Height, (unvell.ReoGrid.Interaction.MouseButtons)e.Buttons);
        }

        /// <summary>
        /// Overrides mouse-double-click events.
        /// </summary>
        /// <param name="e">Argument of mouse double click event.</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            this.currentWorksheet.OnMouseDoubleClick(new Graphics.Point(e.Location.X, e.Location.Y), (unvell.ReoGrid.Interaction.MouseButtons)e.Buttons);
        }

        //protected override void OnDragOver(DragEventArgs drgevent)
        //{
        //	//TODO
        //	MessageBox.Show("drag");
        //}

        #endregion // Mouse

        #region Keyboard

        /// <summary>
        /// Overrides key-down event
        /// </summary>
        /// <param name="e">Argument of key-down event</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.currentWorksheet.OnKeyDown((KeyCode)e.KeyData))
            {
                base.OnKeyDown(e);
            }
        }

        /// <summary>
        /// Overrides key-up event
        /// </summary>
        /// <param name="e">Argument of key-up event</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.currentWorksheet.OnKeyUp((KeyCode)e.KeyData);
        }

        #endregion // Keyboard

        #region Scroll

        //      private Panel rightPanel;
        //      private Panel horizontalScrollbarPanel;
        //      private Eto.Forms.hScrollBar = null;
        //private VScrollBar vScrollBar = null;

        //      private class ScrollBarCorner : Control
        //      {
        //          public ScrollBarCorner()
        //          {
        //              Size = new Size(20, 20);
        //          }

        //          protected override void OnMouseMove(MouseEventArgs e)
        //          {
        //              base.OnMouseMove(e);

        //              Cursor = Cursors.Default;
        //          }
        //      }

        //      private void OnHorScroll(object sender, ScrollEventArgs e)
        //      {
        //          if (this.currentWorksheet.ViewportController is IScrollableViewportController)
        //          {
        //              ((IScrollableViewportController)this.currentWorksheet.ViewportController).HorizontalScroll(e.NewValue);
        //          }
        //      }
        //      private void OnVerScroll(object sender, ScrollEventArgs e)
        //      {
        //          if (this.currentWorksheet.ViewportController is IScrollableViewportController)
        //          {
        //              ((IScrollableViewportController)this.currentWorksheet.ViewportController).VerticalScroll(e.NewValue);
        //          }
        //      }

        #endregion // Scroll

        #region Sheet Tab

        private SheetTabControl sheetTab;
        private ContextMenu sheetListMenu;
        private ContextMenu sheetContextMenu;

        /// <summary>
        /// Get or set menu strip of sheet tab control.
        /// </summary>
        public ContextMenu SheetTabContextMenu { get; set; }

        void sheetMenuItem_Click(object sender, EventArgs e)
        {
            var sheet = ((ToolItem)sender).Tag as Worksheet;

            if (sheet != null)
            {
                this.CurrentWorksheet = sheet;
            }
        }

        private void ShowSheetTabControl()
        {
            //if (!this.bottomPanel.Visible)
            //{
            //    this.bottomPanel.Visible = true;
            //}

            this.sheetTab.Visible = true;
        }

        private void HideSheetTabControl()
        {
            this.sheetTab.Visible = false;

            //if (!this.hScrollBar.Visible)
            //{
            //	this.bottomPanel.Visible = false;
            //}
        }

        private void ShowHorScrollBar()
        {
            //if (!this.hScrollBar.Visible)
            //{
            //	if (this.sheetTab.Visible)
            //	{
            //		this.sheetTab.Dock = DockStyle.Left;
            //	}

            //	if (!this.bottomPanel.Visible)
            //	{
            //		this.bottomPanel.Visible = true;
            //	}

            //	this.hScrollBar.Visible = true;
            //}
        }

        private void HideHorScrollBar()
        {
            //if (this.hScrollBar.Visible)
            //{
            //	this.hScrollBar.Visible = false;

            //	if (this.sheetTab.Visible)
            //	{
            //		this.sheetTab.Dock = DockStyle.Fill;
            //	}
            //	else
            //	{
            //		this.bottomPanel.Visible = false;
            //	}
            //}
        }

        private void ShowVerScrollBar()
        {
            //if (!this.vScrollBar.Visible)
            //{
            //	this.vScrollBar.Visible = true;
            //}
        }

        private void HideVerScrollBar()
        {
            //this.vScrollBar.Visible = false;
        }

        #endregion // Sheet Tab

        #region Context Menu

        private ContextMenu columnHeaderContextMenu;

        /// <summary>
        /// Context menu strip displayed when user click on header of column
        /// </summary>
        public ContextMenu ColumnHeaderContextMenu
        {
            get { return columnHeaderContextMenu; }
            set { columnHeaderContextMenu = value; }
        }

        private ContextMenu rowHeaderContextMenu;

        /// <summary>
        /// Context menu strip displayed when user click on header of row
        /// </summary>
        public ContextMenu RowHeaderContextMenu
        {
            get { return rowHeaderContextMenu; }
            set { rowHeaderContextMenu = value; }
        }

        private ContextMenu leadHeaderContextMenu;

        /// <summary>
        /// Context menu strip displayed when user click on header of row
        /// </summary>
        public ContextMenu LeadHeaderContextMenu
        {
            get { return leadHeaderContextMenu; }
            set { leadHeaderContextMenu = value; }
        }

        #endregion // Cursor & Context Menu

        #region Edit Control

        #region InputTextBox
        private class InputTextBox : Eto.Forms.TextBox
        {
            private ReoGridControl owner;
            internal bool TextWrap { get; set; }
            internal Size InitialSize { get; set; }
            internal ReoGridVerAlign VAlign { get; set; }
            private Eto.Drawing.Graphics graphics;

            internal InputTextBox(ReoGridControl owner)
                : base()
            {
                this.owner = owner;
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                var sheet = owner.currentWorksheet;

                if (sheet.currentEditingCell != null && Visible)
                {
                    bool isProcessed = false;

                    // in single line text
                    if (!TextWrap && Text.IndexOf('\n') == -1)
                    {
                        isProcessed = true;
                        if (e.Key == Keys.Up)
                        {
                            ProcessSelectionMoveKey(e, sheet, () => sheet.MoveSelectionUp());
                        }
                        else if (e.Key == Keys.Down)
                        {
                            ProcessSelectionMoveKey(e, sheet, () => sheet.MoveSelectionDown());
                        }
                        else if (e.Key == Keys.Left && Selection.Start == 0)
                        {
                            ProcessSelectionMoveKey(e, sheet, () => sheet.MoveSelectionLeft());
                        }
                        else if (e.Key == Keys.Right && Selection.Start == Text.Length)
                        {
                            ProcessSelectionMoveKey(e, sheet, () => sheet.MoveSelectionRight());
                        }
                        else
                        {
                            isProcessed = false;
                        }
                    }

                    if (!isProcessed)
                    {
                        //if (!Toolkit.IsKeyDown(Win32.VKey.VK_CONTROL) && e.Key == Keys.Enter)
                        //{
                        //    ProcessSelectionMoveKey(e, sheet, () => sheet.MoveSelectionForward());
                        //}
                    }
                }
            }

            private void ProcessSelectionMoveKey(KeyEventArgs e, Worksheet sheet, Action moveAction)
            {
                //e.SuppressKeyPress = true;
                sheet.EndEdit(Text);
                moveAction();
            }

            //         protected override bool ProcessCmdKey(ref Message msg, System.Windows.Forms.Keys keyData)
            //{
            //	var sheet = owner.currentWorksheet;

            //	if (keyData == System.Windows.Forms.Keys.Escape)
            //	{
            //		sheet.EndEdit(EndEditReason.Cancel);
            //		sheet.DropKeyUpAfterEndEdit = true;

            //		return true;
            //	}
            //	else if (keyData == System.Windows.Forms.Keys.Tab
            //		|| keyData == (System.Windows.Forms.Keys.Tab | System.Windows.Forms.Keys.Shift))
            //	{
            //		sheet.EndEdit(EndEditReason.NormalFinish);
            //		sheet.OnKeyDown((KeyCode)keyData);

            //		return true;
            //	}
            //	else
            //	{
            //		return base.ProcessCmdKey(ref msg, keyData);
            //	}
            //}

            protected override void OnTextChanged(EventArgs e)
            {
                base.OnTextChanged(e);

                this.Text = this.owner.currentWorksheet.RaiseCellEditTextChanging(this.Text);

                CheckAndUpdateWidth();
            }

            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);
                if (Visible)
                {
                    CheckAndUpdateWidth();
                }
            }

            private void CheckAndUpdateWidth()
            {
                int fieldWidth = 0;

                if (TextWrap)
                {
                    fieldWidth = InitialSize.Width;
                }
                else
                {
                    fieldWidth = 9999999; // todo: avoid unsafe magic number
                }

                Size size = Size.Round(graphics.MeasureString(Font, Text));

                if (TextWrap)
                {
                    this.SuspendLayout();

                    if (Height < size.Height)
                    {
                        int offset = size.Height - Height + 1;

                        Height += offset;

                        if (Height < Font.LineHeight)
                        {
                            offset = (int)Font.LineHeight - Height;
                        }

                        Height += offset;

                        switch (VAlign)
                        {
                            case ReoGridVerAlign.Top:
                                break;
                            default:
                            case ReoGridVerAlign.Middle:
                                this.Location.Offset(0, -offset / 2);
                                break;
                            case ReoGridVerAlign.Bottom:
                                this.Location.Offset(0, -offset);
                                break;
                        }
                    }

                    this.ResumeLayout();
                }
                else
                {
                    this.SuspendLayout();

                    if (Width < size.Width + 5)
                    {
                        int widthOffset = size.Width + 5 - Width;

                        switch (this.TextAlignment)
                        {
                            default:
                            case TextAlignment.Left:
                                break;
                            case TextAlignment.Right:
                                Location.Offset(-widthOffset, 0);
                                break;
                        }

                        Width += widthOffset;
                    }

                    if (Height < size.Height + 1)
                    {
                        int offset = size.Height - 1 - Height;
                        Location.Offset(0, -offset / 2);
                        Height = size.Height + 1;
                    }

                    this.ResumeLayout();
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (graphics != null) graphics.Dispose();

                base.Dispose(disposing);
            }
        }
        #endregion // InputTextBox

        private InputTextBox editTextbox;

        #endregion // Edit Control

        #region View

        /// <summary>
        /// Overrides on-resize process method
        /// </summary>
        /// <param name="e">Argument of on-resize event</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.currentWorksheet != null)
            {
                this.currentWorksheet.UpdateViewportControllBounds();
            }

            base.OnSizeChanged(e);

            if (this.sheetTab.Width < 60) this.sheetTab.Width = 60;
        }

        /// <summary>
        /// Overrides visible-changed process method
        /// </summary>
        /// <param name="e">Argument of visible-changed event</param>
        //protected override void OnVisibleChanged(EventArgs e)
        //{
        //    base.OnVisibleChanged(e);

        //    if (this.Visible)
        //    {
        //        this.currentWorksheet.UpdateViewportControllBounds();
        //    }
        //}

        private EtoRenderer.EtoRenderer Renderer;

        /// <summary>
        /// Overrides repaint process method
        /// </summary>
        /// <param name="e">Argument of visible-changed event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var sheet = this.currentWorksheet;

            if (Renderer == null) Renderer = new EtoRenderer.EtoRenderer(e.Graphics);

            if (sheet != null && sheet.ViewportController != null)
            {
                CellDrawingContext dc = new CellDrawingContext(this.currentWorksheet, DrawMode.View, Renderer);

                sheet.ViewportController.Draw(dc);

            }

        }

        #endregion // View
    }
}

#endif // WINFORM
