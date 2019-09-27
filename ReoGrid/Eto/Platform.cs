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
using WFFontStyle = Eto.Drawing.FontStyle;

using Common;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Drawing.Text;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoRenderer;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoRenderer
{
    class EtoFont : BaseFont
    {
        internal Eto.Drawing.Font Font { get; set; }
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
        internal Eto.Drawing.Font RenderFont { get; set; }
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
                dpi = Eto.Forms.Screen.PrimaryScreen.DPI;
            }

            return dpi;
        }

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

            var size = resManager.CachedGDIGraphics.MeasureString(font, text);

            size.Width += 2;
            size.Height += 2;

            return new Graphics.Size(size.Width, size.Height);
        }

        internal static WFFontStyle ToWFFontStyle(FontStyles style)
        {
            FontStyle fs = FontStyle.None;

            if ((style & FontStyles.Bold) == FontStyles.Bold)
            {
                fs |= WFFontStyle.Bold;
            }
            if ((style & FontStyles.Italic) == FontStyles.Italic)
            {
                fs |= WFFontStyle.Italic;
            }

            return fs;
        }

        internal static bool IsKeyDown(KeyCode key)
        {
            return false; // Toolkit.IsKeyDown((Common.Win32Lib.Win32.VKey)key);
        }
    }
    #endregion // PlatformUtility

    #region StaticResources
    partial class StaticResources
    {
        internal static readonly string SystemDefaultFontName = Eto.Drawing.SystemFonts.Default().FamilyName;
        internal static readonly float SystemDefaultFontSize = System.Drawing.SystemFonts.DefaultFont.Size;

        internal static readonly SolidColor EmptyColor = Eto.Drawing.Colors.Transparent.ToSolidColor();

        internal static readonly SolidColor SystemColor_Highlight = Eto.Drawing.SystemColors.Highlight.ToSolidColor();
        internal static readonly SolidColor SystemColor_Window = Eto.Drawing.SystemColors.WindowBackground.ToSolidColor();
        internal static readonly SolidColor SystemColor_WindowText = Eto.Drawing.SystemColors.ControlText.ToSolidColor();
        internal static readonly SolidColor SystemColor_Control = Eto.Drawing.SystemColors.Control.ToSolidColor();
        internal static readonly SolidColor SystemColor_ControlDark = Eto.Drawing.SystemColors.ControlBackground.ToSolidColor();
    }
    #endregion // StaticResources
}

#endif // WINFORM