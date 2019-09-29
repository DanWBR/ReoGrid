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

using Common;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Views;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Rendering;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Main;

using DrawMode = DWSIM.CrossPlatform.UI.Controls.ReoGrid.Rendering.DrawMode;
using Rectangle = DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics.Rectangle;

using Cursor = Eto.Forms.Mouse;

using Point = Eto.Drawing.Point;
using Size = Eto.Drawing.Size;
using WFRect = Eto.Drawing.Rectangle;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoRenderer;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid
{
    /// <summary>
    /// ReoGrid - .NET Spreadsheet Component for Windows Form
    /// </summary>

    public partial class ReoGridControl : Eto.Forms.Drawable, IVisualWorkbook, IContextMenuControl, IPersistenceWorkbook, IActionControl, IWorkbook, IScrollableWorksheetContainer
    {

        #region Constructor, Init & Dispose

        private EtoControlAdapter adapter;

        //public Scrollable ScrollableParent;

        public PixelLayout PixelLayoutParent;

        private Point prevscroll = new Point();

        //private Graphics canvasGraphics;
        /// <summary>
        /// Create component instance.
        /// </summary>
        public ReoGridControl(PixelLayout pixparent)
        {

            //ScrollableParent = parent;

            PixelLayoutParent = pixparent;

            SuspendLayout();

            #region Sheet tabs

            this.sheetTab = new SheetTabControl(this)
            {
                Height = 30,
                Position = SheetTabControlPosition.Bottom
            };

            //this.ScrollableParent.MouseWheel += (sender, e) =>
            //{
            //    this.OnMouseWheel(e);
            //};

            //this.ScrollableParent.Scroll += (sender, e) =>
            //{
            //    if (!Application.Instance.Platform.IsMac)
            //    {
            //        float dx, dy;
            //        dx = e.ScrollPosition.X - prevscroll.X;
            //        dy = e.ScrollPosition.Y - prevscroll.Y;
            //        if (Math.Abs(dx) > 0 || Math.Abs(dy) > 0)
            //        {
            //            var sheet = this.currentWorksheet;
            //            if (sheet != null && sheet.ViewportController != null)
            //            {
            //                if (dx > 0 && dy == 0)
            //                {
            //                    ((NormalViewportController)sheet.ViewportController).ScrollViews(ScrollDirection.Horizontal, dx, dy);
            //                }
            //                else if (dx == 0 && dy > 0)
            //                {
            //                    ((NormalViewportController)sheet.ViewportController).ScrollViews(ScrollDirection.Vertical, dx, dy);
            //                }
            //                else
            //                {
            //                    ((NormalViewportController)sheet.ViewportController).ScrollViews(ScrollDirection.Both, dx, dy);
            //                }
            //            }
            //            prevscroll = e.ScrollPosition;
            //        }
            //    }
            //};

            this.sheetTab.MouseMove += (s, e) => this.Cursor = Cursors.Default;

            this.sheetTab.SplitterMoving += (s, e) =>
            {

                var p = this.bottomPanel.PointFromScreen(Eto.Forms.Mouse.Position);
                int newWidth = (int)p.X - (int)this.sheetTab.Bounds.Left;

                if (newWidth < 50)
                {
                    newWidth = 50;
                }

                this.sheetTab.Width = newWidth;
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
                if (e.MouseButtons == Interaction.MouseButtons.Right)
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
                                var index = this.sheetTab.SelectedIndex;
                                if (index >= 0 && index < this.workbook.worksheets.Count)
                                {
                                    var sheet = this.workbook.worksheets[this.sheetTab.SelectedIndex];
                                    if (sheet != null)
                                    {
                                        using (var rsd = new DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoControls.RenameSheetDialog())
                                        {
                                            var rect = this.sheetTab.GetItemBounds(this.sheetTab.SelectedIndex);

                                            var p = (Point)this.sheetTab.PointToScreen(rect.Location);
                                            p.X -= (rsd.Width - rect.Width) / 2;
                                            p.Y -= rsd.Height + 5;

                                            rsd.Location = p;
                                            rsd.SheetName = sheet.Name;

                                            if (rsd.ShowModal())
                                            {
                                                sheet.Name = rsd.SheetName;
                                            }
                                        }
                                    }
                                }
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

            #region Bottom Panel

            this.bottomPanel = new Panel
            {
                Height = 30,
                BackgroundColor = SystemColors.Control,
            };

            this.bottomPanel.Content = this.sheetTab;

            #endregion // Bottom Panel

            this.InitControl();

            ResumeLayout();

            this.adapter = new EtoControlAdapter(this);

            this.editTextbox = new InputTextBox(this) { Visible = false, TextWrap = true };
            this.adapter.editTextbox = this.editTextbox;

            this.InitWorkbook(adapter);

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
        protected override void Dispose(bool disposing)
        {
            if (Renderer != null)
            {
                Renderer.Dispose();
            }

            base.Dispose(disposing);

            this.workbook.Dispose();

        }

        #endregion // Constructor

        #region Adapter

        private class EtoControlAdapter : IControlAdapter//, IPlatformDependencyInterface
        {
            private readonly ReoGridControl control;
            internal InputTextBox editTextbox;

            //private Scrollable ScrollableParent;

            public EtoControlAdapter(ReoGridControl control)
            {
                this.control = control;
                //this.ScrollableParent = control.ScrollableParent;
            }

            #region Scroll
            public int ScrollBarHorizontalMaximum { get { return control.Bounds.Width; } set { } }

            public int ScrollBarHorizontalMinimum { get; set; }

            public int ScrollBarHorizontalValue
            {
                get { return 0; }//ScrollableParent.ScrollPosition.X; }
                set
                {
                    //ScrollableParent.ScrollPosition = new Point(value, ScrollableParent.ScrollPosition.Y);
                }
            }

            public int ScrollBarHorizontalLargeChange { get; set; }

            public int ScrollBarVerticalMaximum { get { return control.Bounds.Height; } set { } }

            public int ScrollBarVerticalMinimum { get; set; }

            public int ScrollBarVerticalValue
            {
                get { return 0; } // ScrollableParent.ScrollPosition.Y; }
                set
                {
                    //ScrollableParent.ScrollPosition = new Point(ScrollableParent.ScrollPosition.X, value);
                }
            }

            public int ScrollBarVerticalLargeChange { get; set; }

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
                    case CursorStyle.PlatformDefault:
                        this.control.Cursor = Cursors.Default;
                        break;
                    case CursorStyle.Selection:
                        this.control.Cursor = Cursors.Arrow;
                        break;
                    case CursorStyle.Hand:
                        this.control.Cursor = Cursors.Pointer;
                        break;
                    case CursorStyle.ResizeHorizontal:
                    case CursorStyle.ChangeColumnWidth:
                        control.Cursor = Cursors.VerticalSplit;
                        break;
                    case CursorStyle.ResizeVertical:
                    case CursorStyle.ChangeRowHeight:
                        control.Cursor = Cursors.HorizontalSplit;
                        break;
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
                editTextbox.Size = new Size(rect.Size);
                var position = rect.Location;
                position.Offset(0, 102);
                control.PixelLayoutParent.Move(editTextbox, position);
                editTextbox.TextWrap = cell.IsMergedCell || cell.InnerStyle.TextWrapMode != TextWrapMode.NoWrap;
                editTextbox.InitialSize = rect.Size;
                editTextbox.VAlign = cell.InnerStyle.VAlign;
                editTextbox.Font = cell.RenderFont;
                //editTextbox.TextColor = SystemColors.ControlText;
                //editTextbox.BackgroundColor = cell.InnerStyle.HasStyle(PlainStyleFlag.BackColor)
                //    ? cell.InnerStyle.BackColor.ToEto() : this.control.ControlStyle[ControlAppearanceColors.GridBackground].ToEto();
                editTextbox.ResumeLayout();
                editTextbox.Visible = true;
                editTextbox.Focus();
            }

            public void HideEditControl()
            {
                editTextbox.Visible = false;
                control.doubleclicked = false;
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
                //if (Application.Instance.Platform.IsWinForms)
                //{
                //    int width = this.ScrollableParent.ClientSize.Width;
                //    int height = this.ScrollableParent.ClientSize.Height;

                //    int left = this.ScrollableParent.ScrollPosition.X;
                //    int top = this.ScrollableParent.ScrollPosition.Y;

                //    if (width < 0) width = 0;
                //    if (height < 0) height = 0;

                //    return new Rectangle(left, top, width, height);
                //}
                //else
                //{
                return this.control.Bounds.ToRectangle();
                //}
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
            if (!doubleclicked)
            {
                this.Focus();
                this.OnWorksheetMouseDown(e.Location, e.Buttons.ToMouseButtons());
            }
        }

        /// <summary>
        /// Overrides mouse-move events
        /// </summary>
        /// <param name="e">Argument of mouse moving event.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.OnWorksheetMouseMove((Point)e.Location, e.Buttons.ToMouseButtons());
        }

        /// <summary>
        /// Overrides mouse-up events
        /// </summary>
        /// <param name="e">Argument of mouse release event.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.OnWorksheetMouseUp((Point)e.Location, e.Buttons.ToMouseButtons());
        }

        /// <summary>
        /// Overrides mouse-wheel events.
        /// </summary>
        /// <param name="e">Argument of mouse wheel event.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
#if DEBUG
            Console.WriteLine(String.Format("OnMouseWheel: {0}", e.Delta));
#endif
            if (Application.Instance.Platform.IsWinForms)
            {
                this.currentWorksheet.OnMouseWheel(new Graphics.Point(e.Location.X, e.Location.Y), (int)(e.Delta.Width * 20), (int)(e.Delta.Height * 20), e.Buttons.ToMouseButtons());
            }
            else if (Application.Instance.Platform.IsWpf)
            {
                this.currentWorksheet.OnMouseWheel(new Graphics.Point(e.Location.X, e.Location.Y), (int)(e.Delta.Width * 40), (int)(e.Delta.Height * 40), e.Buttons.ToMouseButtons());
            }
            else if (Application.Instance.Platform.IsMac)
            {
                this.currentWorksheet.OnMouseWheel(new Graphics.Point(e.Location.X, e.Location.Y), (int)(e.Delta.Width * 10), (int)(e.Delta.Height * 10), e.Buttons.ToMouseButtons());
            }
        }

        protected bool doubleclicked = false;

        /// <summary>
        /// Overrides mouse-double-click events.
        /// </summary>
        /// <param name="e">Argument of mouse double click event.</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            doubleclicked = true;
            this.currentWorksheet.OnMouseDoubleClick(new Graphics.Point(e.Location.X, e.Location.Y), e.Buttons.ToMouseButtons());
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
            if (!this.currentWorksheet.OnKeyDown(e.KeyData.ToKeyCode()))
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
            this.currentWorksheet.OnKeyUp(e.KeyData.ToKeyCode());
        }

        #endregion // Keyboard

        #region Sheet Tab

        private SheetTabControl sheetTab;
        public Panel bottomPanel;
        private ContextMenu sheetListMenu;
        private ContextMenu sheetContextMenu;

        /// <summary>
        /// Get or set menu strip of sheet tab control.
        /// </summary>
        public ContextMenu SheetTabContextMenu { get; set; }

        void sheetMenuItem_Click(object sender, EventArgs e)
        {
            var sheet = ((MenuItem)sender).Tag as Worksheet;

            if (sheet != null)
            {
                this.CurrentWorksheet = sheet;
            }
        }

        private void ShowSheetTabControl()
        {
            this.bottomPanel.Visible = true;
        }

        private void HideSheetTabControl()
        {
            this.bottomPanel.Visible = false;
        }

        private void ShowHorScrollBar()
        {
        }

        private void HideHorScrollBar()
        {
        }

        private void ShowVerScrollBar()
        {
        }

        private void HideVerScrollBar()
        {
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
        public class InputTextBox : Eto.Forms.TextBox
        {
            private ReoGridControl owner;
            internal bool TextWrap { get; set; }
            internal Size InitialSize { get; set; }
            internal ReoGridVerAlign VAlign { get; set; }

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
                        if (e.Key == Keys.Enter || e.Key == Keys.Tab)
                        {
                            ProcessSelectionMoveKey(e, sheet, () => sheet.MoveSelectionForward());
                        }
                    }
                }
            }

            private void ProcessSelectionMoveKey(KeyEventArgs e, Worksheet sheet, Action moveAction)
            {
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
                // NOT USED
            }

        }
        #endregion // InputTextBox

        public InputTextBox editTextbox;

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

        private int paintcalls = 0;

        /// <summary>
        /// Overrides repaint process method
        /// </summary>
        /// <param name="e">Argument of visible-changed event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var sheet = this.currentWorksheet;
#if DEBUG
            var starttime = DateTime.Now;
            paintcalls += 1;
            Console.WriteLine(String.Format("OnPaint() called {0} times", paintcalls));
#endif
            if (Renderer == null)
            {
                Renderer = new EtoRenderer.EtoRenderer(e.Graphics);
#if DEBUG
                Console.WriteLine(String.Format("Created New Renderer"));
#endif
            }
            else
            {
                Renderer.PlatformGraphics = e.Graphics;
            }

            if (sheet != null && sheet.ViewportController != null)
            {
                CellDrawingContext dc = new CellDrawingContext(this.currentWorksheet, DrawMode.View, Renderer);
                if (Application.Instance.Platform.IsWinForms)
                {
                    sheet.UpdateViewportControllBounds();
                }
                sheet.ViewportController.Draw(dc);

            }

            //Renderer = null;

#if DEBUG
            Console.WriteLine(String.Format("Total Rendering Time: {0} ms", (DateTime.Now - starttime).TotalMilliseconds));
#endif

        }

        #endregion // View
    }
}

#endif // WINFORM
