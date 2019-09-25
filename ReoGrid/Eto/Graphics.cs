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
using System.Collections.Generic;

using unvell.ReoGrid.Graphics;

using WFFontStyle = Eto.Drawing.FontStyle;
using PlatformGraphics = Eto.Drawing.Graphics;

using WFPointF = Eto.Drawing.PointF;
using WFRectF = Eto.Drawing.RectangleF;

using Point = unvell.ReoGrid.Graphics.Point;
using Rectangle = unvell.ReoGrid.Graphics.Rectangle;

using RGFloat = System.Single;

using RGPen = Eto.Drawing.Pen;
using RGBrush = Eto.Drawing.Brush;

using unvell.ReoGrid.Core;
using unvell.ReoGrid.Utility;
using unvell.ReoGrid.Drawing.Text;
using unvell.ReoGrid.Rendering;
using DrawMode = unvell.ReoGrid.Rendering.DrawMode;

using Eto.Drawing;
using unvell.Common;

namespace unvell.ReoGrid.EtoRenderer
{

    internal class EtoGraphics : IGraphics
    {
        protected ResourcePoolManager resourceManager = new ResourcePoolManager();

        private Eto.Drawing.Graphics g = null;

        public PlatformGraphics PlatformGraphics { get { return this.g; } set { this.g = value; } }

        public EtoGraphics()
        {

        }

        public EtoGraphics(PlatformGraphics g)
            : this()
        {
            this.g = g;
        }

        #region Line
        public void DrawLine(RGFloat x1, RGFloat y1, RGFloat x2, RGFloat y2, SolidColor color)
        {
            if (color.A > 0) this.g.DrawLine(this.resourceManager.GetPen(color), x1, y1, x2, y2);
        }
        public void DrawLine(Point startPoint, Point endPoint, SolidColor color)
        {
            if (color.A > 0) this.g.DrawLine(color.ToEto(), startPoint.ToEto(), endPoint.ToEto());
        }
        public void DrawLine(RGFloat x1, RGFloat y1, RGFloat x2, RGFloat y2, SolidColor color, RGFloat width, LineStyles style)
        {
            if (color.A > 0)
            {
                var p = this.resourceManager.GetPen(color, width, ToGDILineStyle(style));

                if (p != null)
                {
                    g.DrawLine(p, x1, y1, x2, y2);
                }
            }
        }
        public void DrawLine(Point startPoint, Point endPoint, SolidColor color, RGFloat width, LineStyles style)
        {
            if (color.A > 0)
            {
                var p = this.resourceManager.GetPen(color, width, ToGDILineStyle(style));
                if (p != null)
                {
                    this.g.DrawLine(p.Color, startPoint.ToEto(), endPoint.ToEto());
                }
            }
        }
        public void DrawLine(Pen p, RGFloat x1, RGFloat y1, RGFloat x2, RGFloat y2) { this.g.DrawLine(p, x1, y1, x2, y2); }
        public void DrawLine(Pen p, Point startPoint, Point endPoint) { this.g.DrawLine(p.Color, startPoint.ToEto(), endPoint.ToEto()); }
        public void DrawLines(Point[] points, int start, int length, SolidColor color, RGFloat width, LineStyles style)
        {
            if (color.A < 0) return;
            var p = this.resourceManager.GetPen(color, width, ToGDILineStyle(style));
            if (p == null) return;

            WFPointF[] pt = new WFPointF[length];
            for (int i = 0, k = start; i < pt.Length; i++, k++)
            {
                pt[i] = points[k].ToEto();
            }

            this.g.DrawLines(p, pt);
        }

        //public void DrawLine(SolidColor color, Point startPoint, Point endPoint, RGFloat width,
        //	LineStyles style, LineCapStyles startCap, LineCapStyles endCap)
        //{
        //	using (Pen p = new Pen(color, width))
        //	{
        //		if (startCap == LineCapStyles.Arrow)
        //		{
        //			p.StartCap = Eto.Drawing.Drawing2D.LineCap.Custom;
        //			p.CustomStartCap = new AdjustableArrowCap(width + 3, width + 3);
        //		}

        //		if (endCap == LineCapStyles.Arrow)
        //		{
        //			p.EndCap = Eto.Drawing.Drawing2D.LineCap.Custom;
        //			p.CustomEndCap = new AdjustableArrowCap(width + 3, width + 3);
        //		}

        //		this.g.DrawLine(p, startPoint, endPoint);
        //	}
        //}
        #endregion // Line

        #region Rectangle
        public void DrawRectangle(Rectangle rect, SolidColor color)
        {
            if (color.A > 0) this.g.DrawRectangle(this.resourceManager.GetPen(color), rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawRectangle(RGFloat x, RGFloat y, RGFloat width, RGFloat height, SolidColor color)
        {
            if (color.A > 0) this.g.DrawRectangle(this.resourceManager.GetPen(color), x, y, width, height);
        }
        public void DrawRectangle(Pen p, Rectangle rect) { this.g.DrawRectangle(p, rect.X, rect.Y, rect.Width, rect.Height); }
        public void DrawRectangle(Pen p, RGFloat x, RGFloat y, RGFloat width, RGFloat height) { this.g.DrawRectangle(p, x, y, width, height); }
        public void DrawRectangle(Rectangle rect, SolidColor color, RGFloat width, LineStyles lineStyle)
        {
            var p = this.resourceManager.GetPen(color, width, ToGDILineStyle(lineStyle));
            this.g.DrawRectangle(p, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillRectangle(HatchStyles style, SolidColor hatchColor, SolidColor bgColor, Rectangle rect)
        {
            //this.g.FillRectangle(this.resourceManager.GetHatchBrush(style, hatchColor, bgColor), rect);
        }
        public void FillRectangle(HatchStyles style, SolidColor hatchColor, SolidColor bgColor, RGFloat x, RGFloat y, RGFloat width, RGFloat height)
        {
            //this.g.FillRectangle(this.resourceManager.GetHatchBrush(style, hatchColor, bgColor), x, y, width, height);
        }
        public void FillRectangle(Brush b, RGFloat x, RGFloat y, RGFloat w, RGFloat h) { this.g.FillRectangle(b, x, y, w, h); }

        public void FillRectangle(Brush b, Rectangle rect) { this.g.FillRectangle(b, rect.ToEto()); }

        public void FillRectangle(Eto.Drawing.SolidBrush style, SolidColor hatchColor, SolidColor bgColor, Rectangle rect)
        {
            this.FillRectangle(style, hatchColor, bgColor, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void FillRectangle(object style, SolidColor hatchColor, SolidColor bgColor, RGFloat x, RGFloat y, RGFloat width, RGFloat height)
        {
            //Eto.Drawing.SolidBrush hb = this.resourceManager.GetHatchBrush(style, hatchColor, bgColor);
            //this.g.FillRectangle(hb, x, y, width, height);
        }
        public void FillRectangle(Rectangle rect, IColor color)
        {
            if (color is SolidColor)
            {
                this.g.FillRectangle(color.ToSolidColor().ToEto(), rect.ToEto());
            }
        }
        public void FillRectangle(RGFloat x, RGFloat y, RGFloat w, RGFloat h, IColor color)
        {
            this.g.FillRectangle(color.ToSolidColor().ToEto(), x, y, w, h);
        }

        public void DrawAndFillRectangle(Rectangle rect, SolidColor lineColor, IColor fillColor)
        {
            FillRectangle(rect, fillColor);
            DrawRectangle(rect, lineColor);
        }

        public void DrawAndFillRectangle(Rectangle rect, SolidColor lineColor, IColor fillColor, RGFloat weight, LineStyles lineStyle)
        {
            FillRectangle(rect, fillColor);
            DrawRectangle(rect, lineColor, weight, lineStyle);
        }

        public void FillRectangleLinear(SolidColor startColor, SolidColor endColor, RGFloat angle, Rectangle rect)
        {
            using (Eto.Drawing.LinearGradientBrush lgb =
                new Eto.Drawing.LinearGradientBrush(new WFRectF(rect.X, rect.Y, rect.Width, rect.Height), startColor.ToEto(), endColor.ToEto(), angle))
            {
                this.g.PixelOffsetMode = Eto.Drawing.PixelOffsetMode.Half;
                this.g.FillRectangle(lgb, rect.ToEto());
                this.g.PixelOffsetMode = Eto.Drawing.PixelOffsetMode.None;
            }
        }
        #endregion // Rectangle

        #region Image
        public void DrawImage(Image image, RGFloat x, RGFloat y, RGFloat width, RGFloat height) { this.g.DrawImage(image, x, y, width, height); }
        public void DrawImage(Image image, Rectangle bounds) { this.g.DrawImage(image, bounds.Location.ToEto()); }
        #endregion // Image

        #region Ellipse
        public void DrawEllipse(SolidColor color, Rectangle rectangle)
        {
            if (!color.IsTransparent)
            {
                g.DrawEllipse(color.ToEto(), rectangle.ToEto());
            }
        }
        public void DrawEllipse(SolidColor color, RGFloat x, RGFloat y, RGFloat width, RGFloat height)
        {
            if (!color.IsTransparent)
            {
                g.DrawEllipse(this.resourceManager.GetPen(color), x, y, width, height);
            }
        }
        public void DrawEllipse(Pen pen, Rectangle rect) { this.g.DrawEllipse(pen.Color, rect.ToEto()); }
        public void FillEllipse(IColor fillColor, Rectangle rectangle)
        {
            this.g.FillEllipse(fillColor.ToSolidColor().ToEto(), rectangle.ToEto());
        }
        public void FillEllipse(Brush br, Rectangle rect)
        {
            this.g.FillEllipse(((SolidBrush)br).Color, rect.ToEto());
        }

        public void FillEllipse(Brush b, RGFloat x, RGFloat y, RGFloat widht, RGFloat height) { this.g.FillEllipse(b, x, y, widht, height); }
        #endregion // Ellipse

        #region Polygon

        public void DrawPolygon(SolidColor color, RGFloat lineWidth, LineStyles lineStyle, params Point[] points)
        {
            if (!color.IsTransparent)
            {
                var p = resourceManager.GetPen(color, lineWidth, ToGDILineStyle(lineStyle));

                var wfpoints = new WFPointF[points.Length];
                for (int i = 0; i < points.Length; i++)
                {
                    wfpoints[i] = points[i].ToEto();
                }

                this.g.DrawPolygon(p, wfpoints);
            }
        }

        public void FillPolygon(IColor color, params Point[] points)
        {
            if (!color.IsTransparent)
            {
                var wfpoints = new WFPointF[points.Length];
                for (int i = 0; i < points.Length; i++)
                {
                    wfpoints[i] = points[i].ToEto();
                }

                this.g.FillPolygon(color.ToSolidColor().ToEto(), wfpoints);
            }
        }

        #endregion // Polygon

        #region Path
        public void FillPath(IColor color, GraphicsPath graphicsPath)
        {
            if (!color.IsTransparent)
            {
                g.FillPath(color.ToSolidColor().ToEto(), graphicsPath);
            }
        }

        public void DrawPath(SolidColor color, GraphicsPath graphicsPath)
        {
            if (!color.IsTransparent)
            {
                var p = this.resourceManager.GetPen(color.ToSolidColor());
                if (p != null) g.DrawPath(p, graphicsPath);
            }
        }
        #endregion // Path

        #region Text
        //public void DrawText(string text, string fontName, RGFloat size, SolidColor color, Rectangle rect)
        //{
        //	if (color.IsTransparent)
        //	{
        //		return;
        //	}

        //	var font = this.resourceManager.GetFont(fontName, size, WFFontStyle.Regular);

        //	if (font != null)
        //	{
        //		lock (this.sf)
        //		{
        //			sf.Alignment = StringAlignment.Near;
        //			sf.LineAlignment = StringAlignment.Near;

        //			g.DrawString(text, font, this.resourceManager.GetBrush(color), rect, sf);
        //		}
        //	}
        //}

        public void DrawText(string text, string fontName, RGFloat size, SolidColor color, Rectangle rect,
            ReoGridHorAlign halign, ReoGridVerAlign valign)
        {
            if (color.IsTransparent)
            {
                return;
            }

            var font = this.resourceManager.GetFont(fontName, size, WFFontStyle.None);

            if (font != null)
            {
                //switch (halign)
                //{
                //    default: sf.Alignment = StringAlignment.Near; break;
                //    case ReoGridHorAlign.Center: sf.Alignment = StringAlignment.Center; break;
                //    case ReoGridHorAlign.Right: sf.Alignment = StringAlignment.Far; break;
                //}

                //switch (valign)
                //{
                //    default: sf.LineAlignment = StringAlignment.Near; break;
                //    case ReoGridVerAlign.Middle: sf.LineAlignment = StringAlignment.Center; break;
                //    case ReoGridVerAlign.Bottom: sf.LineAlignment = StringAlignment.Far; break;
                //}

                g.DrawText(font, color.ToEto(), rect.X, rect.Y, text);

            }
        }

        //public Graphics.Size MeasureText(string text, string fontName, RGFloat size, Graphics.Size areaSize)
        //{
        //	var font = this.resourceManager.GetFont(fontName, size, WFFontStyle.Regular);

        //	if (font == null)
        //	{
        //		return Graphics.Size.Zero;
        //	}

        //	lock (sf)
        //	{
        //		return g.MeasureString(text, font, areaSize, sf);
        //	}
        //}

        #endregion // Text

        #region Clip
        private Stack<WFRectF> clipStack = new Stack<WFRectF>();

        public void PushClip(Rectangle clip)
        {
            clipStack.Push(this.g.ClipBounds);
            this.g.SetClip(clip.ToEto());
        }

        public void PopClip()
        {
            WFRectF cb = clipStack.Pop();
            this.g.SetClip(cb);
        }
        #endregion // Clip

        #region Transform
        private Stack<IMatrix> transformStack = new Stack<IMatrix>();

        /// <summary>
        /// Push a new transform matrix into stack. (Backup current transform matrix)
        /// </summary>
        public void PushTransform()
        {
            this.PushTransform(Matrix.Create());
        }

        /// <summary>
        /// Push specified transform matrix into stack. (Backup current transform matrix)
        /// </summary>
        public void PushTransform(IMatrix m)
        {
            var t = Matrix.Create(this.g.CurrentTransform.Elements);

            this.transformStack.Push(t);

            this.g.MultiplyTransform(Matrix.Create(t.Elements));
        }

        /// <summary>
        /// Pop the last transform matrix from stack. (Restore transform matrix to previous status)
        /// </summary>
        public IMatrix PopTransform()
        {
            IMatrix m = this.transformStack.Pop();
            this.g.MultiplyTransform(m);
            return m;
        }

        /// <summary>
        /// Scale current transform matrix.
        /// </summary>
        /// <param name="sx">X-factor to be scaled.</param>
        /// <param name="sy">Y-factor to be scaled.</param>
        public void ScaleTransform(RGFloat sx, RGFloat sy)
        {
            if (sx != 0 && sy != 0)
            {
                this.g.ScaleTransform(sx, sy);
            }
        }

        /// <summary>
        /// Translate current transform matrix.
        /// </summary>
        /// <param name="x">X-offset value to be translated.</param>
        /// <param name="y">Y-offset value to be translated.</param>
        public void TranslateTransform(RGFloat x, RGFloat y)
        {
            this.g.TranslateTransform(x, y);
        }

        /// <summary>
        /// Rotate current transform matrix by specified angle.
        /// </summary>
        /// <param name="angle"></param>
        public void RotateTransform(RGFloat angle)
        {
            this.g.RotateTransform(angle);
        }

        /// <summary>
        /// Reset current transform matrix. (Load identity)
        /// </summary>
        public void ResetTransform()
        {
            this.g.RestoreTransform();
        }
        #endregion // Transform

        #region Utility
        public bool IsAntialias
        {
            get { return this.g.AntiAlias; }
            set { this.g.AntiAlias = value; }
        }

        public void Reset()
        {
            this.transformStack.Clear();
            this.clipStack.Clear();
        }

        internal static Eto.Drawing.DashStyle ToGDILineStyle(LineStyles dashStyle)
        {
            switch (dashStyle)
            {
                default:
                case LineStyles.Solid: return Eto.Drawing.DashStyles.Solid;
                case LineStyles.Dash: return Eto.Drawing.DashStyles.Dash;
                case LineStyles.Dot: return Eto.Drawing.DashStyles.Dot;
                case LineStyles.DashDot: return Eto.Drawing.DashStyles.DashDot;
                case LineStyles.DashDotDot: return Eto.Drawing.DashStyles.DashDotDot;
            }
        }
        #endregion // Utility

    }

    #region EtoRenderer

    internal class EtoRenderer : EtoGraphics, IRenderer
    {
        private Eto.Drawing.Graphics cachedGraphics;

        internal static readonly Eto.Drawing.Font HeaderFont = new Eto.Drawing.Font(
            Eto.Drawing.SystemFonts.Default().FamilyName, 8f, Eto.Drawing.FontStyle.None);

        internal EtoRenderer(Eto.Drawing.Graphics cg)
        {
            this.cachedGraphics = cg;
        }

        public void DrawRunningFocusRect(RGFloat x, RGFloat y, RGFloat w, RGFloat h, SolidColor color, int runnerOffset)
        {
            using (var p = new Pen(color.ToEto()))
            {
                base.PlatformGraphics.DrawRectangle(p, x, y, w, h);
            }
        }

        #region Capped Line
        private Pen cappedLinePen;
        private Graphics.LineCap lineCap;

        public void BeginCappedLine(LineCapStyles startStyle, Graphics.Size startSize,
            LineCapStyles endStyle, Graphics.Size endSize, SolidColor color, RGFloat width)
        {
            lineCap.startStyle = startStyle;
            lineCap.startSize = startSize;
            lineCap.endStyle = endStyle;
            lineCap.endSize = endSize;
            lineCap.startColor = color;
            lineCap.endColor = color;

            if ((startStyle == LineCapStyles.None && endStyle == LineCapStyles.None))
            {
                return;
            }

            //PlatformGraphics.AntiAlias = true;

            if (cappedLinePen == null)
            {
                this.cappedLinePen = new Pen(color.ToEto(), width);
            }
            else
            {
                this.cappedLinePen.Color = color.ToEto();
                this.cappedLinePen.Thickness = width;
            }

            if (startStyle == LineCapStyles.Arrow)
            {
                cappedLinePen.LineCap = PenLineCap.Butt;
            }
            else
            {
                cappedLinePen.LineCap = PenLineCap.Square;
            }

            //if (endStyle == LineCapStyles.Arrow)
            //{
            //    cappedLinePen.CustomEndCap = this.cappedEndArrowCap;
            //}
            //else
            //{
            //    this.cappedLinePen.EndCap = Eto.Drawing.Drawing2D.LineCap.NoAnchor;
            //}
        }

        private void DrawEllipseForCappedLine(RGFloat x, RGFloat y, Graphics.Size size)
        {
            RGFloat ellipseSize = size.Width + size.Height / 2;
            RGFloat halfOfEllipse = ellipseSize / 2f;

            using (var b = new SolidBrush(this.cappedLinePen.Color))
            {
                PlatformGraphics.FillEllipse(b, x - halfOfEllipse, y - halfOfEllipse, ellipseSize, ellipseSize);
            }
        }

        public void DrawCappedLine(RGFloat x1, RGFloat y1, RGFloat x2, RGFloat y2)
        {
            PlatformGraphics.DrawLine(this.cappedLinePen, x1, y1, x2, y2);

            if (this.lineCap.startStyle == LineCapStyles.Ellipse)
            {
                this.DrawEllipseForCappedLine(x1, y1, this.lineCap.startSize);
            }

            if (this.lineCap.endStyle == LineCapStyles.Ellipse)
            {
                this.DrawEllipseForCappedLine(x2, y2, this.lineCap.endSize);
            }
        }

        public void EndCappedLine()
        {
            //PlatformGraphics.AntiAlias = true;
        }
        #endregion // Capped Line

        #region Line
        private Pen linePen;

        public void BeginDrawLine(RGFloat width, SolidColor color)
        {
            if (linePen == null)
            {
                linePen = new Pen(color.ToEto(), width);
            }
            else
            {
                linePen.Color = color.ToEto();
                linePen.Thickness = width;
            }
        }

        public void DrawLine(RGFloat x1, RGFloat y1, RGFloat x2, RGFloat y2)
        {
            PlatformGraphics.DrawLine(this.linePen, x1, y1, x2, y2);
        }

        public void EndDrawLine()
        {
        }

        #endregion Line

        #region Cell
        public void DrawCellText(Cell cell, SolidColor textColor, DrawMode drawMode, RGFloat scale)
        {
            var sheet = cell.Worksheet;

            if (sheet == null) return;

            var b = this.GetBrush(textColor);
            if (b == null) return;

            Rectangle textBounds;
            Eto.Drawing.Font scaledFont;

            #region Determine text bounds
            switch (drawMode)
            {
                default:
                case DrawMode.View:
                    textBounds = cell.TextBounds;
                    scaledFont = cell.RenderFont;
                    break;

                case DrawMode.Preview:
                case DrawMode.Print:
                    textBounds = cell.PrintTextBounds;
                    scaledFont = this.resourceManager.GetFont(cell.RenderFont.FamilyName,
                        cell.InnerStyle.FontSize * scale, cell.RenderFont.Typeface.FontStyle);
                    break;
            }
            #endregion // Determine text bounds

            //#region Set sf wrap
            //if (cell.InnerStyle.TextWrapMode == TextWrapMode.NoWrap)
            //{
            //    sf.FormatFlags |= Eto.Drawing.StringFormatFlags.NoWrap;
            //}
            //else
            //{
            //    sf.FormatFlags &= ~Eto.Drawing.StringFormatFlags.NoWrap;
            //}
            //#endregion // Set sf wrap

            var g = base.PlatformGraphics;

            #region Rotate text
            if (cell.InnerStyle.RotationAngle != 0)
            {
#if DEBUG1
					g.DrawRectangle(Pens.Red, (Eto.Drawing.Rectangle)textBounds);
#endif // DEBUG

                this.PushTransform();

                this.TranslateTransform(textBounds.OriginX, textBounds.OriginY);
                this.RotateTransform(-cell.InnerStyle.RotationAngle);

                //sf.LineAlignment = StringAlignment.Center;
                //sf.Alignment = StringAlignment.Center;

                g.DrawText(scaledFont, ((SolidBrush)b).Color, 0, 0, cell.DisplayText);

                this.PopTransform();
            }
            else
            #endregion // Rotate text
            {
                #region Align StringFormat
                switch (cell.RenderHorAlign)
                {
                    default:
                    case ReoGridRenderHorAlign.Left:
                        //sf.Alignment = Eto.Drawing.StringAlignment.Near;
                        break;

                    case ReoGridRenderHorAlign.Center:
                        //sf.Alignment = Eto.Drawing.StringAlignment.Center;
                        break;

                    case ReoGridRenderHorAlign.Right:
                        //sf.Alignment = Eto.Drawing.StringAlignment.Far;
                        break;
                }

                switch (cell.InnerStyle.VAlign)
                {
                    case ReoGridVerAlign.Top:
                        //sf.LineAlignment = Eto.Drawing.StringAlignment.Near;
                        break;

                    default:
                    case ReoGridVerAlign.Middle:
                        //sf.LineAlignment = Eto.Drawing.StringAlignment.Center;
                        break;

                    case ReoGridVerAlign.Bottom:
                        //sf.LineAlignment = Eto.Drawing.StringAlignment.Far;
                        break;
                }
                #endregion // Align StringFormat

                g.DrawText(scaledFont, ((SolidBrush)b).Color, textBounds.X, textBounds.Y, cell.DisplayText);

            }

        }

        public void UpdateCellRenderFont(Cell cell, Core.UpdateFontReason reason)
        {
            var sheet = cell.Worksheet;

            if (sheet == null) return;

            var style = cell.InnerStyle;

            var fontStyle = StyleUtility.CreateFontStyle(style);

            // unknown bugs happened here (several times)
            // cell.Style is null (cell.Style.FontSize is zero)
            if (style.FontSize <= 0) style.FontSize = 6f;

            RGFloat fontSize = (RGFloat)Math.Round(style.FontSize * sheet.renderScaleFactor, 1);

            var renderFont = cell.RenderFont;

            if (renderFont == null
                || renderFont.FamilyName != style.FontName
                || renderFont.Size != fontSize)
            {
                cell.RenderFont = this.resourceManager.GetFont(style.FontName, fontSize,
                    (Eto.Drawing.FontStyle)fontStyle);
            }
        }

        public Graphics.Size MeasureCellText(Cell cell, DrawMode drawMode, RGFloat scale)
        {
            var sheet = cell.Worksheet;

            if (sheet == null) return Graphics.Size.Zero;

            WorksheetRangeStyle style = cell.InnerStyle;

            int fieldWidth = 0;

            double s = 0, c = 0;

            //if (sf == null) sf = new Eto.Drawing.StringFormat(Eto.Drawing.StringFormat.GenericTypographic);

            if (cell.Style.RotationAngle != 0)
            {
                double d = (style.RotationAngle * Math.PI / 180.0d);
                s = Math.Sin(d);
                c = Math.Cos(d);
            }

            //lock (sf)
            //{
            // merged cell need word break automatically
            if (style.TextWrapMode == TextWrapMode.NoWrap)
            {
                //// no word break
                //fieldWidth = 9999999; // TODO: avoid magic number
                //sf.FormatFlags |= Eto.Drawing..NoWrap;
            }
            else
            {
                // get cell available width
                RGFloat cellWidth = 0;

                if (cell.Style.RotationAngle != 0)
                {
                    cellWidth = (RGFloat)(Math.Abs(cell.Bounds.Width * c) + Math.Abs(cell.Bounds.Height * s));
                }
                else
                {
                    cellWidth = cell.Bounds.Width;

                    if (cell.InnerStyle != null)
                    {
                        if ((cell.InnerStyle.Flag & PlainStyleFlag.Indent) == PlainStyleFlag.Indent)
                        {
                            cellWidth -= (int)(cell.InnerStyle.Indent * sheet.IndentSize);
                        }
                    }

                    // border width
                    cellWidth -= 2;
                }

                // word break
                fieldWidth = (int)Math.Round(cellWidth * scale);
                //sf.FormatFlags &= ~Eto.Drawing.StringFormatFlags.NoWrap;
            }

            if (cell.FontDirty)
            {
                sheet.UpdateCellRenderFont(this, cell, drawMode, UpdateFontReason.FontChanged);
            }

            var g = this.cachedGraphics;

            Eto.Drawing.Font scaledFont;
            switch (drawMode)
            {
                default:
                case DrawMode.View:
                    scaledFont = cell.RenderFont;
                    break;

                case DrawMode.Preview:
                case DrawMode.Print:
                    scaledFont = this.resourceManager.GetFont(cell.RenderFont.FamilyName,
                        style.FontSize * scale, cell.RenderFont.Typeface.FontStyle);
                    g = this.PlatformGraphics;
                    System.Diagnostics.Debug.Assert(g != null);
                    break;
            }

            SizeF size = g.MeasureString(scaledFont, cell.DisplayText);
            size.Height++;

            if (style.RotationAngle != 0)
            {
                RGFloat w = (RGFloat)(Math.Abs(size.Width * c) + Math.Abs(size.Height * s));
                RGFloat h = (RGFloat)(Math.Abs(size.Width * s) + Math.Abs(size.Height * c));

                size = new SizeF(w + 1, h + 1);
            }

            return new Graphics.Size(Convert.ToSingle(size.Width), Convert.ToSingle(size.Height));
            //}
        }

        #endregion // Cell

        #region Header

        private Font scaledHeaderFont;


        public void BeginDrawHeaderText(RGFloat scale)
        {
            scaledHeaderFont = this.resourceManager.GetFont(HeaderFont.FamilyName, HeaderFont.Size * scale, FontStyle.None);
        }

        public void DrawHeaderText(string text, RGBrush brush, Rectangle rect)
        {
            base.PlatformGraphics.DrawText(scaledHeaderFont, ((SolidBrush)brush), rect.X, rect.Y, text);
        }

        #endregion // Header

        public void DrawLeadHeadArrow(Graphics.Rectangle bounds, SolidColor startColor, SolidColor endColor)
        {
            using (Eto.Drawing.GraphicsPath leadHeadPath = new Eto.Drawing.GraphicsPath())
            {
                leadHeadPath.AddLines(new WFPointF[] { new PointF(bounds.Right - 4, bounds.Y + 4),
                        new PointF(bounds.Right - 4, bounds.Bottom - 4),
                        new PointF(bounds.Right - bounds.Height + 4, bounds.Bottom - 4)});

                leadHeadPath.CloseFigure();

                using (Eto.Drawing.LinearGradientBrush lgb
                    = new Eto.Drawing.LinearGradientBrush(bounds.ToEto(), startColor.ToEto(), endColor.ToEto(), 90f))
                {
                    base.PlatformGraphics.FillPath(lgb, leadHeadPath);
                }
            }
        }

        public RGPen GetPen(SolidColor color)
        {
            return this.resourceManager.GetPen(color);
        }

        public void ReleasePen(RGPen pen)
        {
        }

        public RGBrush GetBrush(SolidColor color)
        {
            return new SolidBrush(color.ToEto());
        }

        public Eto.Drawing.Font GetFont(string name, RGFloat size, FontStyles style)
        {
            return this.resourceManager.GetFont(name, size, PlatformUtility.ToWFFontStyle(style));
        }

        public ResourcePoolManager ResourcePoolManager
        {
            get { return this.resourceManager; }
        }

        public void Dispose()
        {
            if (this.cappedLinePen != null)
            {
                this.cappedLinePen.Dispose();
                this.cappedLinePen = null;
            }

            //if (this.cappedStartArrowCap != null)
            //{
            //    this.cappedStartArrowCap.Dispose();
            //    this.cappedStartArrowCap = null;
            //}

            //if (this.cappedEndArrowCap != null)
            //{
            //    this.cappedEndArrowCap.Dispose();
            //    this.cappedEndArrowCap = null;
            //}

            if (this.linePen != null)
            {
                this.linePen.Dispose();
            }

            if (this.cachedGraphics != null)
            {
                this.cachedGraphics.Dispose();
                this.cachedGraphics = null;
            }

            //if (this.sf != null)
            //{
            //    this.sf.Dispose();
            //    this.sf = null;
            //}

            //if (this.headerSf != null)
            //{
            //    this.headerSf.Dispose();
            //    this.headerSf = null;
            //}
        }
    }
    #endregion // GDIRenderer
}

#endif
