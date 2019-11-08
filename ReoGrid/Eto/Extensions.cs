﻿using System;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid.EtoRenderer
{
    public static class Extensions
    {

        public static void Add(this Eto.Forms.PixelLayout layout, Eto.Forms.Control control)
        {
            layout.Add(control, (Eto.Drawing.Point)control.Tag);
        }

        public static DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction.KeyCode ToKeyCode(this Eto.Forms.Keys keys)
        {
            try {
                var kc = (DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction.KeyCode)Enum.Parse(Type.GetType("DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction.KeyCode"), keys.ToString());
                return kc;
            } catch {
                return Interaction.KeyCode.None;
            }
        }

        public static DWSIM.CrossPlatform.UI.Controls.ReoGrid.Interaction.MouseButtons ToMouseButtons(this Eto.Forms.MouseButtons buttons)
        {
            switch (buttons)
            {
                default:
                case Eto.Forms.MouseButtons.Primary:
                    return Interaction.MouseButtons.Left;
                case Eto.Forms.MouseButtons.Middle:
                    return Interaction.MouseButtons.Middle;
                case Eto.Forms.MouseButtons.Alternate:
                    return Interaction.MouseButtons.Right;
            }
        }
        public static Eto.Drawing.PointF ToEto(this DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics.Point point)
        {
            var value = new Eto.Drawing.PointF(point.X, point.Y);
            return value;
        }

        public static Eto.Drawing.RectangleF ToEto(this DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics.Rectangle rect)
        {
            var value =  new Eto.Drawing.RectangleF(rect.X, rect.Y,rect.Width, rect.Height);
            return value;
        }

        public static DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics.Rectangle ToRectangle(this Eto.Drawing.Rectangle rect)
        {
            var value = new Graphics.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            return value;
        }

        public static Eto.Drawing.Color ToEto(this DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics.SolidColor color)
        {
            var value =  new Eto.Drawing.Color(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
            return value;
        }

        public static DWSIM.CrossPlatform.UI.Controls.ReoGrid.Graphics.SolidColor  ToSolidColor(this Eto.Drawing.Color color)
        {
            var value =  new Graphics.SolidColor((int)(color.A * 255), (int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));
            return value;
        }

    }
}