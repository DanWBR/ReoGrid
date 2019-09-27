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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eto.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using Eto.Forms;

namespace unvell.UIControls
{
	/// <summary>
	/// Line with text label control
	/// </summary>
	public class FormLine : Label
	{
		/// <summary>
		/// Create control
		/// </summary>
		public FormLine()
		{
			BackgroundColor = Colors.Transparent;
		}

		private bool show3DLine = true;

		/// <summary>
		/// Indicates whether or not to show line in 3D style.
		/// </summary>
		[DefaultValue(true)]
		public virtual bool Show3DLine
		{
			get { return show3DLine; }
			set { show3DLine = value; Invalidate(); }
		}

		private Color lineColor;

		/// <summary>
		/// Get or set line color
		/// </summary>
		public virtual Color LineColor
		{
			get { return lineColor; }
			set { lineColor = value; Invalidate(); }
		}

		/// <summary>
		/// Get or set label text
		/// </summary>
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				Invalidate();
			}
		}

		private int textPadding = 14;

		/// <summary>
		/// Get or set text padding.
		/// </summary>
		[DefaultValue(14)]
		public virtual int TextPadding
		{
			get { return textPadding; }
			set { textPadding = value; Invalidate(); }
		}

	}
}
