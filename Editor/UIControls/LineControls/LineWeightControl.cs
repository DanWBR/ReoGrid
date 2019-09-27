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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Eto.Forms;

namespace unvell.UIControls
{
	public partial class LineWeightControl : DropDown
	{
		public LineWeightControl()
		{
			base.Items.Add("0.2");
			base.Items.Add("0.5");
			base.Items.Add("1.0");
			base.Items.Add("1.5");
            base.Items.Add("2.0");
            base.Items.Add("2.5");
            base.Items.Add("3.0");
            base.Items.Add("4.0");
            base.Items.Add("5.0");
            base.Items.Add("7.5");
            base.Items.Add("10.0");
		}
	}
}
