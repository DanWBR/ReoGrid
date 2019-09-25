using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unvell.ReoGrid.EtoRenderer
{
    public static class Extensions
    {

        public static unvell.ReoGrid.Interaction.MouseButtons ToMouseButtons(this Eto.Forms.MouseButtons buttons)
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
        public static Eto.Drawing.PointF ToEto(this unvell.ReoGrid.Graphics.Point point)
        {
            var value = new Eto.Drawing.PointF(point.X, point.Y);
            return value;
        }

        public static Eto.Drawing.RectangleF ToEto(this unvell.ReoGrid.Graphics.Rectangle rect)
        {
            var value =  new Eto.Drawing.RectangleF(rect.X, rect.Y,rect.Width, rect.Height);
            return value;
        }

        public static Eto.Drawing.Color ToEto(this unvell.ReoGrid.Graphics.SolidColor color)
        {
            var value =  new Eto.Drawing.Color(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
            return value;
        }

        public static unvell.ReoGrid.Graphics.SolidColor  ToSolidColor(this Eto.Drawing.Color color)
        {
            var value =  new Graphics.SolidColor((int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255), (int)(color.A * 255));
            return value;
        }

    }
}
