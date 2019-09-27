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

using SkiaSharp;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics;
using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid
{
	partial class ReoGridCell
	{
		internal SKTypeface renderFont;
	}
}

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.Rendering
{
	partial class PlatformUtility
	{
		internal static float GetDPI()
		{
			return Eto.Forms.Screen.PrimaryScreen.DPI;
		}

		internal static bool IsKeyDown(KeyCode key)
		{
            // TODO
            return false;
		}
	}

	partial class StaticResources
	{
		internal static readonly SolidColor SystemColor_Window = SolidColor.White;
		internal static readonly SolidColor SystemColor_WindowText = SolidColor.Black;
		internal static readonly SolidColor SystemColor_Highlight = SolidColor.SkyBlue;
		internal static readonly SolidColor SystemColor_Control = SolidColor.Silver;
		internal static readonly SolidColor SystemColor_ControlDark = SolidColor.Gray;
	}
}

#endif // SKIASHARP