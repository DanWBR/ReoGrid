/*****************************************************************************
 * 
 * ReoGrid - .NET Spreadsheet Control
 * 
 * http://reogrid.net/
 * 
 * Sheet Tab Control - Represents a lightweight and fast sheet tab control
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
using System.Linq;
using Eto.Drawing;
using System.ComponentModel;

using Common;
using System.Collections.Generic;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Views;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Main;
using Eto.Forms;
using System.Windows.Forms;
using MouseButtons = Eto.Forms.MouseButtons;
using MouseEventArgs = Eto.Forms.MouseEventArgs;
using Cursors = Eto.Forms.Cursors;
using PaintEventArgs = Eto.Forms.PaintEventArgs;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoRenderer
{
    /// <summary>
    /// Represents the sheet tab control.
    /// </summary>
    internal class SheetTabControl : Drawable, ISheetTabControl
    {
        private ReoGridControl grid;

        private Image newButtonImage;
        private Image newButtonDisableImage;

        public Eto.Drawing.Font Font { get; set; }

        /// <summary>
        /// Construct the control
        /// </summary>
        public SheetTabControl(ReoGridControl grid)
        {
            this.grid = grid;
            this.Font = SystemFonts.Default();
            this.NewButtonVisible = true;
            this.newButtonImage = Bitmap.FromResource("DWSIM.CrossPlatform.UI.Controls.ReoGrid.Resources.icons8-add_row_focused.png").WithSize(16, 16);
            this.newButtonDisableImage = Bitmap.FromResource("DWSIM.CrossPlatform.UI.Controls.ReoGrid.Resources.icons8-add_row.png").WithSize(16, 16);
        }

        protected override void Dispose(bool disposing)
        {
            if (g != null)
            {
                this.g.Dispose();
            }

            base.Dispose(disposing);
        }

        private List<SheetTabItem> tabs = new List<SheetTabItem>(3);

        /// <summary>
        /// Specifies the position of tab control
        /// </summary>
        public SheetTabControlPosition Position { get; set; }

        /// <summary>
        /// Specifies the border style of every tabs
        /// </summary>
        public SheetTabBorderStyle BorderStyle { get; set; }

        /// <summary>
        /// Show shadows beside of every tabs
        /// </summary>
        //[Description("Show shadows beside of every tabs")]
        //[DefaultValue(false)]
        public bool Shadow { get; set; } = true;

        ///// <summary>
        ///// Get or set the border color
        ///// </summary>
        //[Description("Get or set the border color")]
        //public Color BorderColor { get; set; }

        ///// <summary>
        ///// Get or set the background color for selected tab
        ///// </summary>
        //[Description("Get or set the background color for selected tab")]
        //public Color SelectedBackColor { get; set; }

        ///// <summary>
        ///// Get or set the text color for selected tab
        ///// </summary>
        //[Description("Get or set the text color for selected tab")]
        //public Color SelectedTextColor { get; set; }

        private int selectedIndex = 0;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;

                    Invalidate();

                    if (SelectedIndexChanged != null)
                    {
                        SelectedIndexChanged(this, null);
                    }
                }
            }
        }

        /// <summary>
        /// Determine whether or not allow to move tab by dragging mouse
        /// </summary>
        [Description("Determine whether or not allow to move tab by dragging mouse")]
        public bool AllowDragToMove { get; set; } = false;

        private const int leftPadding = 30, rightPadding = 28;
        private int maxWidth = 0;
        private int viewScroll = 0;

        private bool addButtonHover = false;

        public Rectangle GetItemBounds(int index)
        {
            return this.tabs[index].Bounds;
        }

        public float TranslateScrollPoint(int p)
        {
            return p - this.viewScroll;
        }

        private ResourcePoolManager resourceManager = new ResourcePoolManager();

        private Eto.Drawing.Graphics g;

        /// <summary>
        /// Override OnPaint method
        /// </summary>
        /// <param name="e">paint event argument</param>
        protected override void OnPaint(PaintEventArgs e)
        {

            g = e.Graphics;

            g.SaveTransform();

            int hh = (int)Math.Round(Height / 2f);

            GraphicsToolkit.FillTriangle(g, 8, new Graphics.Point(10, hh), GraphicsToolkit.TriangleDirection.Left,
                viewScroll > 0 ? new Pen(SystemColors.ControlText) : new Pen(SystemColors.DisabledText));

            int max = this.maxWidth - this.ClientSize.Width + rightPadding + 10;

            GraphicsToolkit.FillTriangle(g, 8, new Graphics.Point(20, hh), GraphicsToolkit.TriangleDirection.Right,
                viewScroll < max ? new Pen(SystemColors.ControlText) : new Pen(SystemColors.DisabledText));

            var controlStyle = this.grid.ControlStyle;

            var borderPen = this.resourceManager.GetPen(controlStyle[ControlAppearanceColors.SheetTabBorder]);
            var defaultTextBrush = Brushes.Black;

            Rectangle rect = new Rectangle(leftPadding, 0, ClientSize.Width - 4, ClientSize.Height - 2);

            #region Border
            if (this.BorderStyle != SheetTabBorderStyle.NoBorder)
            {
                switch (this.Position)
                {
                    default:
                    case SheetTabControlPosition.Top:
                        g.DrawLine(borderPen, 0, rect.Bottom - 1,
                                this.Bounds.Right, rect.Bottom - 1);
                        break;

                    case SheetTabControlPosition.Bottom:
                        g.DrawLine(borderPen, 0, rect.Top, Bounds.Right, rect.Top);
                        break;
                }
            }
            #endregion

            g.SetClip(new Rectangle(Bounds.Left + leftPadding, Bounds.Top,
                Bounds.Width - leftPadding - rightPadding, Bounds.Height));

            g.TranslateTransform(-this.viewScroll, 0);

            int textTopPadding = (this.Bounds.Height - (int)SystemFonts.Default().LineHeight) / 2;

            #region Unselected items

            for (int i = 0; i < tabs.Count; i++)
            {
                var tab = this.tabs[i];

                tab.Bounds = new Rectangle(tab.Left, 0, tab.Width, this.Bounds.Height);

                rect = tab.Bounds;

                if (i != selectedIndex)
                {
                    using (var bb = new SolidBrush(tab.BackgroundColor))
                    {
                        g.FillRectangle(bb, rect);
                    }

                    var tsize = g.MeasureString(Font, tab.Title);

                    using (var fb = new SolidBrush(tab.ForegroundColor))
                    {
                        g.DrawText(this.Font, fb, rect.X + rect.Width / 2 - tsize.Width / 2, rect.Y + rect.Height / 2 - tsize.Height / 2, tab.Title);
                    }

                    if (i > 0)
                    {
                        g.DrawLine(SystemColors.ControlBackground, rect.Left, rect.Top + 4, rect.Left, rect.Bottom - 4);
                    }
                }

                if (rect.Left > this.Width) break;
            }

            #endregion

            #region Selected item

            if (this.selectedIndex >= 0 && this.selectedIndex < this.tabs.Count)
            {
                var tab = this.tabs[this.selectedIndex];
                rect = tab.Bounds;

                tab.Bounds = new Rectangle(tab.Left, 0, tab.Width, this.Bounds.Height);

                if (rect.Right > this.viewScroll
                    || rect.Left < this.maxWidth - this.viewScroll)
                {
                    int x = rect.Left, x2 = rect.Right, y = 0, y2 = 0;

                    if (this.BorderStyle != SheetTabBorderStyle.NoBorder)
                    {
                        if (this.BorderStyle == SheetTabBorderStyle.SplitRouned)
                        {
                            x++; x2--;
                        }

                        switch (this.Position)
                        {
                            default:
                            case SheetTabControlPosition.Top:
                                y = rect.Top; y2 = rect.Bottom;

                                if (this.BorderStyle == SheetTabBorderStyle.SplitRouned)
                                {
                                    y++; y2--;
                                }

                                break;

                            case SheetTabControlPosition.Bottom:
                                y = rect.Bottom - 1; y2 = rect.Top;

                                if (this.BorderStyle == SheetTabBorderStyle.SplitRouned)
                                {
                                    y--; y2++;
                                }

                                break;
                        }

                        // left and right
                        g.DrawLine(borderPen, rect.Left, y, rect.Left, y2);
                        g.DrawLine(borderPen, rect.Right, y, rect.Right, y2);
                    }

                    SolidBrush selectedBackBrush = new SolidBrush(SystemColors.ControlBackground);
                    SolidBrush selectedTextBrush = new SolidBrush(SystemColors.ControlText);

                    // top or bottom
                    switch (this.Position)
                    {
                        default:
                        case SheetTabControlPosition.Top:
                            var bgrectTop = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 1, rect.Height - 1);

                            if (tab.BackgroundColor.A > 0)
                            {
                                using (var bb = new SolidBrush(tab.BackgroundColor))
                                {
                                    g.FillRectangle(bb, bgrectTop);
                                }
                            }
                            else
                            {
                                g.FillRectangle(selectedBackBrush, bgrectTop);
                            }

                            if (this.BorderStyle != SheetTabBorderStyle.NoBorder)
                            {
                                g.DrawLine(borderPen, x, rect.Top, x2, rect.Top);
                            }
                            break;

                        case SheetTabControlPosition.Bottom:

                            var bgrectBottom = new Rectangle(rect.Left + 1, rect.Top, rect.Width - 1, rect.Height - 1);

                            if (tab.BackgroundColor.A > 0)
                            {
                                using (var bb = new SolidBrush(tab.BackgroundColor))
                                {
                                    g.FillRectangle(bb, bgrectBottom);
                                }
                            }
                            else
                            {
                                g.FillRectangle(selectedBackBrush, bgrectBottom);
                            }

                            if (this.BorderStyle != SheetTabBorderStyle.NoBorder)
                            {
                                g.DrawLine(borderPen, x, rect.Bottom - 1, x2, rect.Bottom - 1);
                            }
                            break;
                    }

                    if (this.BorderStyle != SheetTabBorderStyle.NoBorder && Shadow)
                    {
                        g.DrawLine(borderPen, rect.Right + 1, rect.Top + 2, rect.Right + 1, rect.Bottom - 1);
                    }

                    var tsize = g.MeasureString(Font, tab.Title);

                    using (var fb = new SolidBrush(tab.ForegroundColor))
                    {
                        g.DrawText(this.Font, fb, rect.X + rect.Width / 2 - tsize.Width / 2, rect.Y + rect.Height / 2 - tsize.Height / 2, tab.Title);
                    }

                }
            }

            #endregion // Selected item

            g.ResetClip();
            //try
            //{
            g.RestoreTransform();
            //}
            //catch { }

            if (this.NewButtonVisible)
            {
                g.DrawImage(addButtonHover ? newButtonImage : newButtonDisableImage, this.Bounds.Width - 28, (this.Bounds.Height - 16) / 2);
            }

            for (int i = 4; i < this.Bounds.Height - 4; i += 4)
            {
                g.FillRectangle(new SolidBrush(SystemColors.ControlBackground), new Rectangle(this.Bounds.Right - 5, i, 2, 2));
            }

            if (this.movingHoverIndex >= 0 && this.movingHoverIndex <= this.tabs.Count
                && this.movingHoverIndex != this.movingStartIndex)
            {
                Rectangle itemRect = GetItemBounds(this.movingHoverIndex >= this.tabs.Count ?
                this.tabs.Count - 1 : this.movingHoverIndex);

                GraphicsToolkit.FillTriangle(g, 8, new Graphics.Point(
                    (this.movingHoverIndex >= this.tabs.Count ? itemRect.Right : itemRect.Left) - this.viewScroll,
                    itemRect.Top + 4), GraphicsToolkit.TriangleDirection.Up);
            }

            base.OnPaint(e);
        }

        private bool splitterPressed = false;
        private bool leftScrollPressed = false, rightScrollPressed = false;
        private Timer timer;

        /// <summary>
        /// Override system mouse down event
        /// </summary>
        /// <param name="e">mouse down event argument</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.leftScrollPressed = this.rightScrollPressed = false;

            // scroll buttons
            if (e.Location.X < leftPadding)
            {
                if (e.Buttons == MouseButtons.Primary)
                {
                    var hwp = leftPadding / 2;

                    if (e.Location.X < hwp)
                    {
                        this.leftScrollPressed = true;
                    }
                    else
                    {
                        this.rightScrollPressed = true;
                    }

                    if (this.leftScrollPressed || this.rightScrollPressed)
                    {
                        if (timer == null)
                        {
                            timer = new Timer { Interval = 10 };
                            timer.Tick += timer_Tick;
                        }

                        timer.Enabled = true;
                    }
                }
                else if (e.Buttons == MouseButtons.Alternate)
                {
                    if (this.SheetListClick != null)
                    {
                        this.SheetListClick(this, null);
                    }
                }
            }
            // splitter
            else if (e.Location.X > this.Bounds.Right - 8)
            {
                this.splitterPressed = true;
                //this.Mouse = true;
            }
            else if (e.Location.X < this.Bounds.Right - 8 && e.Location.X > this.Bounds.Right - 28
                && this.NewButtonVisible)
            {
                if (this.NewSheetClick != null)
                {
                    this.NewSheetClick(this, null);
                }
            }
            else
            {
                int index = GetItemByPoint((int)e.Location.X);

                if (index != -1)
                {
                    if (selectedIndex != index)
                    {
                        selectedIndex = index;

                        Invalidate();

                        if (SelectedIndexChanged != null)
                        {
                            SelectedIndexChanged(this, null);
                        }
                    }

                    bool processed = false;

                    if (this.TabMouseDown != null)
                    {
                        var arg = new SheetTabMouseEventArgs
                        {
                            Location = (Point)e.Location,
                            MouseButtons = e.Buttons.ToMouseButtons(),
                            Handled = false,
                        };

                        this.TabMouseDown(this, arg);

                        processed = arg.Handled;
                    }

                    if (!processed)
                    {
                        if (AllowDragToMove)
                        {
                            movingStartIndex = index;
                            movingHoverIndex = -1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Return index of item by specified x-coordinate point.
        /// </summary>
        /// <param name="x">value of x-coordinate point</param>
        /// <returns>zero-based number of tab at specified screen coordinate, returns -1 if no found</returns>
        public int GetItemByPoint(int x)
        {
            x += this.viewScroll;

            for (int i = 0; i < tabs.Count; i++)
            {
                var tab = this.tabs[i];
                var rect = tab.Bounds;

                if (x >= rect.Left && x <= rect.Right)
                {
                    return i;
                }
            }

            return -1;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (this.leftScrollPressed)
            {
                if (this.viewScroll > -leftPadding)
                {
                    this.viewScroll -= 5;
                    if (this.viewScroll < -leftPadding) this.viewScroll = -leftPadding;

                    this.Invalidate();
                }
            }
            else if (this.rightScrollPressed)
            {
                int max = this.maxWidth - this.Bounds.Width + rightPadding + 10;

                if (this.viewScroll < max)
                {
                    this.viewScroll += 5;
                    if (this.viewScroll > max) this.viewScroll = max;

                    this.Invalidate();
                }
            }
        }

        private int movingStartIndex = -1;
        private int movingHoverIndex = -1;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.splitterPressed)
            {
                Cursor = Cursors.HorizontalSplit;

                if (this.SplitterMoving != null)
                {
                    this.SplitterMoving(this, null);
                }
            }
            else if (e.Location.X > this.Bounds.Width - 8)
            {
                // hover on splitter?
                Rectangle rect = new Rectangle(this.Bounds.Right - 8, this.Bounds.Top, 8, this.Bounds.Height);
                Cursor = rect.Contains((int)e.Location.X, (int)e.Location.Y) ? Cursors.VerticalSplit : Cursors.Default;
            }
            else if (e.Location.X < this.Bounds.Right - 8 && e.Location.X > this.Bounds.Width - 28)
            {
                // hover on add button
                this.addButtonHover = true;
                Invalidate();
                Cursor = Cursors.Default;
            }
            else
            {
                addButtonHover = false;
                Cursor = Cursors.Default;

                if (this.movingStartIndex != -1)
                {
                    if (e.Location.X > this.maxWidth - this.viewScroll)
                    {
                        this.movingHoverIndex = this.tabs.Count;
                    }
                    else
                    {
                        this.movingHoverIndex = GetItemByPoint((int)e.Location.X);
                    }
                }

                Invalidate();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            this.addButtonHover = false;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.leftScrollPressed = false;
            this.rightScrollPressed = false;
            this.splitterPressed = false;

            if (this.timer != null)
            {
                this.timer.Enabled = false;
            }

            if (AllowDragToMove)
            {
                if (this.movingStartIndex >= 0 && this.movingStartIndex < this.tabs.Count
                    && this.movingHoverIndex != this.movingStartIndex
                    && this.movingHoverIndex >= 0 && this.movingHoverIndex <= this.tabs.Count)
                {
                    MoveItem(this.movingStartIndex, this.movingHoverIndex);
                }
            }

            this.movingStartIndex = -1;
            this.movingHoverIndex = -1;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Move item to specified position
        /// </summary>
        /// <param name="fromIndex">number of tab to be moved</param>
        /// <param name="targetIndex">position of moved to</param>
        public void MoveItem(int fromIndex, int targetIndex)
        {
            if (fromIndex < 0 || fromIndex >= this.tabs.Count)
                throw new ArgumentOutOfRangeException("index");

            if (targetIndex < 0 || targetIndex > this.tabs.Count)
                throw new ArgumentOutOfRangeException("targetIndex");

            var fromTab = this.tabs[fromIndex];

            int insertIndex = targetIndex;

            if (targetIndex < fromIndex)
            {
                var insertAfterTab = this.tabs[targetIndex];

                this.tabs.RemoveAt(fromIndex);
                this.tabs.Insert(insertIndex, fromTab);

                int diffWidth = fromTab.Bounds.Width;// -toTab.Bounds.Width;

                fromTab.Left = insertAfterTab.Left;

                for (int i = targetIndex + 1; i < this.tabs.Count && i <= fromIndex; i++)
                {
                    this.tabs[i].Left += diffWidth;
                }
            }
            else
            {
                insertIndex--;

                var insertBeforeTab = this.tabs[targetIndex - 1];

                this.tabs.RemoveAt(fromIndex);
                this.tabs.Insert(insertIndex, fromTab);

                int diffWidth = fromTab.Bounds.Width;// -toTab.Bounds.Width;

                fromTab.Left = insertBeforeTab.Left;

                for (int i = fromIndex; i < this.tabs.Count && i < insertIndex; i++)
                {
                    this.tabs[i].Left -= diffWidth;
                }
            }

            if (this.selectedIndex == fromIndex)
            {
                this.selectedIndex = insertIndex;
            }

            if (this.TabMoved != null)
            {
                this.TabMoved(this, new SheetTabMovedEventArgs
                {
                    Index = fromIndex,
                    TargetIndex = targetIndex,
                });
            }

            Invalidate();
        }

        /// <summary>
        /// Scroll to display specified tab item
        /// </summary>
        /// <param name="index">zero-based number of item to be displayed for scrolling</param>
        public void ScrollToItem(int index)
        {
            if (index >= 0 && index < this.tabs.Count)
            {
                Rectangle rect = GetItemBounds(index);

                int visibleWidth = this.Bounds.Width - leftPadding - rightPadding;

                if (rect.Width > visibleWidth || rect.Left < this.viewScroll + leftPadding)
                {
                    this.viewScroll = rect.Left - leftPadding;
                }
                else if (rect.Right - this.viewScroll > this.Bounds.Right - rightPadding)
                {
                    this.viewScroll = rect.Right - this.Bounds.Width + leftPadding;
                }
            }
        }

        /// <summary>
        /// Event raised when selected tab is changed
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Event raised when splitter is moved
        /// </summary>
        public event EventHandler SplitterMoving;

        /// <summary>
        /// Event raised when sheet list button is clicked
        /// </summary>
        public event EventHandler SheetListClick;

        /// <summary>
        /// Event raised when new sheet butotn is clicked
        /// </summary>
        public event EventHandler NewSheetClick;

        /// <summary>
        /// Event raised when mouse is pressed down on tab items
        /// </summary>
        public event EventHandler<SheetTabMouseEventArgs> TabMouseDown;

        /// <summary>
        /// Event raised when tab item is moved
        /// </summary>
        public event EventHandler<SheetTabMovedEventArgs> TabMoved;

        public int ControlWidth
        {
            get
            {
                return this.Width;
            }
            set
            {
                this.Width = value;
            }
        }

        public void AddTab(string title)
        {
            InsertTab(this.tabs.Count, title);
        }

        public void InsertTab(int index, string title)
        {
            if (index < 0 || index > this.tabs.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            int x = index <= 0 ? 0 : this.tabs[index - 1].Bounds.Right;

            this.tabs.Insert(index, new SheetTabItem
            {
                Title = title,
                Bounds = new Rectangle(x, 0, 75, this.Bounds.Height),
            });

            if (index < this.tabs.Count - 1)
            {
                var tab = this.tabs[index];

                int width = tab.Width;

                for (int i = index + 1; i < this.tabs.Count; i++)
                {
                    this.tabs[i].Left += width;
                }

                this.maxWidth += width;

            }

            this.Invalidate();

            UpdateTab(index, title, Colors.Transparent, SystemColors.ControlText);
        }

        public void RemoveTab(int index)
        {
            if (index < 0 || index >= this.tabs.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var tab = this.tabs[index];

            this.tabs.RemoveAt(index);

            int width = tab.Width;

            for (int i = index; i < this.tabs.Count; i++)
            {
                this.tabs[i].Left -= width;
            }

            this.maxWidth -= width;

            this.Invalidate();

        }

        public void UpdateTab(int index, string title, Color backgroundColor, Color textColor)
        {
            if (index < 0 || index >= this.tabs.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var tab = this.tabs[index];
            tab.Title = title;
            tab.BackgroundColor = backgroundColor;
            tab.ForegroundColor = textColor;

            int width = tab.Width;

            using (var newg = new Eto.Drawing.Graphics(new Eto.Drawing.Bitmap(1, 1, PixelFormat.Format24bppRgb)))
            {
                width = (int)Math.Round(newg.MeasureString(Font, title).Width + 20);
            }

            int diff = (width - tab.Width);
            tab.Width = width;

            for (int i = index + 1; i < this.tabs.Count; i++)
            {
                this.tabs[i].Left += diff;
            }

            this.maxWidth += diff;

            Invalidate();
        }

        public void ClearTabs()
        {
            this.tabs.Clear();
            this.Invalidate();

            this.maxWidth = 0;
        }

        //public int TabCount { get { return this.tabs.Count; } }

        private bool newButtonVisible;

        public bool NewButtonVisible
        {
            get
            {
                return this.newButtonVisible;
            }
            set
            {
                if (this.newButtonVisible != value)
                {
                    this.newButtonVisible = value;
                    this.Invalidate();
                }
            }
        }
    }

    class SheetTabItem
    {
        public string Title { get; set; }

        private Rectangle bounds;
        public Rectangle Bounds { get { return bounds; } set { this.bounds = value; } }

        public int Left { get { return this.bounds.X; } set { this.bounds.X = value; } }
        public int Width { get { return this.bounds.Width; } set { this.bounds.Width = value; } }

        public void Offset(int x, int y)
        {
            this.bounds.X -= x;
            this.bounds.Y -= y;
        }

        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
    }
}

#endif // WINFORM