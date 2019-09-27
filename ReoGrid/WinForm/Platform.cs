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
 * Author: Jing <lujing at unvell.com>
 *
 * Copyright (c) 2012-2016 Jing <lujing at unvell.com>
 * Copyright (c) 2012-2016 unvell.com, all rights reserved.
 * 
 ****************************************************************************/

#if WINFORM

using System.Drawing;
using WFFontStyle = System.Drawing.FontStyle;

using Common
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Drawing.Text;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.WinForm
{
	class GDIFont : BaseFont
	{
		internal System.Drawing.Font Font { get; set; }
	}
}

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid
{
	partial class Cell
	{
		/// <summary>
		/// Render font is the final font used to render text inside cell.
		/// Render font is scaled according to worksheet's scaling.
		/// Render font could be set to null, then it will be updated when cell was required to be rendered.
		/// </summary>
		internal System.Drawing.Font RenderFont { get; set; }
	}
}

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.Rendering
{
	#region PlatformUtility
	partial class PlatformUtility
	{
		private static float dpi = 0;

		public static float GetDPI()
		{
			if (dpi == 0)
			{
				dpi = unvell.common.ResourcePoolManager.CachedGDIGraphics.DpiX;
			}

			return dpi;
		}

		internal static StringFormat sf = new StringFormat(StringFormat.GenericTypographic)
		{
			FormatFlags = StringFormatFlags.MeasureTrailingSpaces
		};

		private static ResourcePoolManager resourcePoolManager;// = new ResourcePoolManager();

		internal static Graphics.Size MeasureText(IRenderer r, string text, string fontName, float fontSize, Drawing.Text.FontStyles style)
		{
			ResourcePoolManager resManager;
			Font font = null;

			if (r == null)
			{
				if (resourcePoolManager == null) resourcePoolManager = new ResourcePoolManager();

				resManager = resourcePoolManager;
			}
			else
			{
				resManager = r.ResourcePoolManager;
			}

			font = resManager.GetFont(fontName, fontSize, ToWFFontStyle(style));

			if (font == null)
			{
				return Graphics.Size.Zero;
			}

			var g = ResourcePoolManager.CachedGDIGraphics;

			lock (g)
			{
				lock (sf)
				{
					var size = ResourcePoolManager.CachedGDIGraphics.MeasureString(text, font, 99999999, sf);

					size.Width+=2;
					size.Height+=2;

					return size;
				}
			}
		}

		internal static WFFontStyle ToWFFontStyle(FontStyles style)
		{
			FontStyle fs = FontStyle.Regular;

			if ((style & FontStyles.Bold) == FontStyles.Bold)
			{
				fs |= WFFontStyle.Bold;
			}
			if ((style & FontStyles.Italic) == FontStyles.Italic)
			{
				fs |= WFFontStyle.Italic;
			}
			if ((style & FontStyles.Strikethrough) == FontStyles.Strikethrough)
			{
				fs |= WFFontStyle.Strikeout;
			}
			if ((style & FontStyles.Underline) == FontStyles.Underline)
			{
				fs |= WFFontStyle.Underline;
			}

			return fs;
		}

		internal static bool IsKeyDown(KeyCode key)
		{
			return Toolkit.IsKeyDown((Common.Win32Lib.Win32.VKey)key);
		}
	}
	#endregion // PlatformUtility

	#region StaticResources
	partial class StaticResources
	{
		internal static readonly string SystemDefaultFontName = System.Drawing.SystemFonts.DefaultFont.Name;
		internal static readonly float SystemDefaultFontSize = System.Drawing.SystemFonts.DefaultFont.Size;

		internal static readonly SolidColor EmptyColor = System.Drawing.Color.Empty;

		internal static readonly SolidColor SystemColor_Highlight = System.Drawing.SystemColors.Highlight;
		internal static readonly SolidColor SystemColor_Window = System.Drawing.SystemColors.Window;
		internal static readonly SolidColor SystemColor_WindowText = System.Drawing.SystemColors.WindowText;
		internal static readonly SolidColor SystemColor_Control = System.Drawing.SystemColors.Control;
		internal static readonly SolidColor SystemColor_ControlDark = System.Drawing.SystemColors.ControlDark;
	}
	#endregion // StaticResources
}

#endif // WINFORM