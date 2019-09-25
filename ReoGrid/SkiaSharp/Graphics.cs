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

#if SKIASHARP

using System;
using System.Collections.Generic;

using Paint = SkiaSharp.SKPaint;
using Picture = SkiaSharp.SKPicture;
using Rect = SkiaSharp.SKRect;
using RectF = SkiaSharp.SKRect;
using Path = SkiaSharp.SKPath;
using Matrix = SkiaSharp.SKMatrix;
using Typeface = SkiaSharp.SKTypeface;

using IGraphics = unvell.ReoGrid.Graphics.IGraphics;
using Point = unvell.ReoGrid.Graphics.Point;
using IColor = unvell.ReoGrid.Graphics.IColor;
using SolidColor = unvell.ReoGrid.Graphics.SolidColor;
using Rectangle = unvell.ReoGrid.Graphics.Rectangle;
using LineStyles = unvell.ReoGrid.Graphics.LineStyles;
using HatchStyles = unvell.ReoGrid.Graphics.HatchStyles;

using PlatformGraphics = SkiaSharp.SKCanvas;
using SkiaSharp;
using unvell.ReoGrid.Graphics;

namespace unvell.ReoGrid.AndroidOS
{
	internal class SkiaSharpGraphics : IGraphics
	{
		protected SKCanvas canvas;

		public bool IsAntialias
		{
			get { return false;}
			set { }
		}

		public PlatformGraphics PlatformGraphics
		{
			get { return this.canvas; }
			set { this.canvas = value; }
		}

		public void DrawEllipse(Paint pen, Rectangle rectangle)
		{
			// TODO
		}

		public void DrawEllipse(SolidColor color, Rectangle rectangle)
		{
			// TODO
		}

		public void DrawEllipse(SolidColor color, float x, float y, float width, float height)
		{
			// TODO
		}

		public void DrawImage(Picture image, Rectangle rect)
		{
			this.canvas.DrawPicture(image, rect.Left, rect.Top);
    }

		public void DrawImage(Picture image, float x, float y, float width, float height)
		{
			this.DrawImage(image, new Rectangle(x, y, width, height));
		}

		public void DrawLine(Paint p, Point startPoint, Point endPoint)
		{
			this.canvas.DrawLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, p);
		}

		public void DrawLine(Point startPoint, Point endPoint, SolidColor color)
		{
			this.DrawLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, color);
		}

		public void DrawLine(Paint p, float x1, float y1, float x2, float y2)
		{
			this.canvas.DrawLine(x1, y1, x2, y2, p);
		}

		public void DrawLine(Point startPoint, Point endPoint, SolidColor color, float width, LineStyles style)
		{
			this.DrawLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, color, width, style);
		}

		public void DrawLine(float x1, float y1, float x2, float y2, SolidColor color)
		{
			this.DrawLine(x1, y1, x2, y2, color, 1, LineStyles.Solid);
		}

		public void DrawLine(float x1, float y1, float x2, float y2, SolidColor color, float width, LineStyles style)
		{
			using (var p = new Paint())
			{
				p.Color = SKColor.Parse(color.ToStringHex());
				p.StrokeWidth = width;

				switch (style)
				{
					case LineStyles.Dot:
						p.PathEffect = SKPathEffect.CreateDash(new float[] { 1, 1 }, 1);
						break;
				}

				this.canvas.DrawLine(x1, y1, x2, y2, p);
			}
		}

		public void DrawLines(Point[] points, int start, int length, SolidColor color, float width, LineStyles style)
		{
			// TODO
		}

		public void DrawPath(SolidColor color, Path graphicsPath)
		{
            canvas.DrawPath(graphicsPath, new Paint() { Color = SKColor.Parse(color.ToStringHex()), IsStroke = true });
		}

		public void FillPath(IColor color, Path graphicsPath)
		{
            canvas.DrawPath(graphicsPath, new Paint() { Color = SKColor.Parse(color.ToSolidColor().ToStringHex()), IsStroke = false });
        }

        public void DrawText(string text, string fontName, float size, SolidColor color, Rectangle rect)
		{
			this.DrawText(text, fontName, size, color, rect, ReoGridHorAlign.General, ReoGridVerAlign.General);
		}

		public void DrawText(string text, string fontName, float size, SolidColor color, Rectangle rect, ReoGridHorAlign halign, ReoGridVerAlign valign)
		{
			using (var p = new Paint())
			{
                using (var font = Typeface.CreateDefault())
                {
                    this.DrawText(text, p, font, size, rect, halign, valign);
                }
			}
		}

		internal void DrawText(string text, Paint p, Typeface font, float size, Rectangle rect, ReoGridHorAlign halign, ReoGridVerAlign valign)
		{
			p.Typeface = font;
			p.TextSize = size;

			var measuredRect = new Rect();
			p.MeasureText(text, ref measuredRect);

			float x = rect.Left, y = rect.Top;

			switch (halign)
			{
				case ReoGridHorAlign.General:
				case ReoGridHorAlign.Left:
					x = rect.Left;
					break;

				case ReoGridHorAlign.Center:
					x = rect.Left + (rect.Width - measuredRect.Width) / 2;
					break;

				case ReoGridHorAlign.Right:
					x = rect.Right - measuredRect.Width;
					break;
			}

			switch (valign)
			{
				case ReoGridVerAlign.Top:
					y = rect.Top + measuredRect.Height;
					break;

				case ReoGridVerAlign.Middle:
					y = rect.Bottom - (rect.Height - measuredRect.Height) / 2;
					break;

				case ReoGridVerAlign.General:
				case ReoGridVerAlign.Bottom:
					y = rect.Bottom;
					break;
			}

			this.canvas.DrawText(text, x, y, p);
		}

		public void FillEllipse(Paint b, Rectangle rectangle)
		{
			// TODO
		}

		public void FillEllipse(IColor fillColor, Rectangle rectangle)
		{
			// TODO
		}

		public void FillEllipse(Paint b, float x, float y, float widht, float height)
		{
			// TODO
		}
		
		public void DrawAndFillRectangle(SolidColor lineColor, IColor fillColor, Rectangle rect)
		{
			if (fillColor is SolidColor)
			{
				this.FillRectangle(rect, (SolidColor)fillColor);
			}

			this.DrawRectangle(rect, lineColor);
		}

		public void DrawAndFillRectangle(SolidColor lineColor, IColor fillColor, Rectangle rect, float weight, LineStyles lineStyle)
		{
			if (fillColor is SolidColor)
			{
				this.FillRectangle(rect, (SolidColor)fillColor);
			}

			this.DrawRectangle(rect, lineColor, weight, lineStyle);
		}

		public void DrawRectangle(Rectangle rect, SolidColor color)
		{
			this.DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, color);
		}

		public void DrawRectangle(Rectangle rect, SolidColor color, float width, LineStyles lineStyle)
		{
			using (var p = new Paint())
			{
				p.Color = SKColor.Parse(color.ToStringHex());
				p.StrokeWidth = width;

				this.DrawRectangle(p, rect);
			}
		}

		public void DrawRectangle(float x, float y, float width, float height, SolidColor color)
		{
			using (var p = new Paint())
			{
				p.Color = SKColor.Parse(color.ToStringHex());

				this.DrawRectangle(p, x, y, width, height);
			}
		}

		public void DrawRectangle(Paint p, Rectangle rect)
		{
			this.DrawRectangle(p, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void DrawRectangle(Paint p, float x, float y, float width, float height)
		{
			p.Style = SKPaintStyle.Stroke;

			this.canvas.DrawRect(x, y, x + width, y + height, p);
		}
	
		public void FillRectangle(Rectangle rect, IColor color)
		{
			this.FillRectangle(rect.X, rect.Y, rect.Width, rect.Height, color);
		}

		public void FillRectangle(float x, float y, float width, float height, IColor color)
		{
			if (color is SolidColor)
			{
				var solidColor = (SolidColor)color;

				using (var p = new Paint())
				{
					p.Color = SKColor.Parse(solidColor.ToStringHex());

					this.FillRectangle(p, x, y, width, height);
				}
			}
		}

		public void FillRectangle(Paint p, float x, float y, float width, float height)
		{
			p.Style = SKPaintStyle.Fill;
			this.canvas.DrawRect(x, y, x + width, y + height, p);
		}

		public void FillRectangle(HatchStyles style, SolidColor hatchColor, SolidColor bgColor, Rectangle rect)
		{
			// TODO
		}

		public void FillRectangle(HatchStyles style, SolidColor hatchColor, SolidColor bgColor, float x, float y, float width, float height)
		{
			// TODO
		}

		public void FillRectangleLinear(SolidColor startColor, SolidColor endColor, float angle, Rectangle rect)
		{
			//using (var p = new Paint())
			//{
			//	p.Shader = SKShader.CreateLinearGradient(rect.Top, rect.Bottom,
			//		startColor, endColor, SKShaderTileMode.Clamp);

			//	this.canvas.DrawRect(Rect.Create(rect.X, rect.Y, rect.Width, rect.Height), p);
			//}
		}


		public Matrix PopTransform()
		{
			this.canvas.Restore();
			return this.canvas.TotalMatrix;
		}

		private Stack<RectF> clipStack = new Stack<RectF>();

		public void PushClip(Rectangle clip)
		{
			Rect oldClip = new Rect();
			this.canvas.ClipRect(oldClip);
			clipStack.Push(RectF.Create(oldClip.Left,oldClip.Top,oldClip.Width,oldClip.Height));
			this.canvas.ClipRect(RectF.Create(clip.Left, clip.Top, clip.Width, clip.Height), SKClipOperation.Intersect);
		}

		public void PopClip()
		{
			RectF oldClip = this.clipStack.Pop();
			//this.canvas.Restore();
			this.canvas.ClipRect(RectF.Create(oldClip.Left, oldClip.Top, oldClip.Width, oldClip.Height), SKClipOperation.Difference);
		}

		public void PushTransform()
		{
			this.canvas.Save();
		}

		public void PushTransform(Matrix t)
		{
			this.canvas.Save();
			this.canvas.SetMatrix(t);
		}

		public void Reset()
		{
			this.canvas.ResetMatrix();
			this.clipStack.Clear();
		}

		public void ResetTransform()
		{
			this.canvas.ResetMatrix();
		}

		public void RotateTransform(float angle)
		{
			this.canvas.RotateDegrees(angle);
		}

		public void ScaleTransform(float sx, float sy)
		{
			this.canvas.Scale(sx, sy);
		}

		public void TranslateTransform(float x, float y)
		{
			this.canvas.Translate(x, y);
		}

        public void DrawAndFillRectangle(Rectangle rect, SolidColor lineColor, IColor fillColor)
        {
           // TODO
        }

        public void DrawAndFillRectangle(Rectangle rect, SolidColor lineColor, IColor fillColor, float weight, LineStyles lineStyle)
        {
            // TODO
        }

        public void DrawPolygon(SolidColor color, float lineWidth, LineStyles lineStyle, params Point[] points)
        {
            // TODO
        }

        public void FillPolygon(IColor color, params Point[] points)
        {
            // TODO
        }
    }
}

#endif // SKIASHARP