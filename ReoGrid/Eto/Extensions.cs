using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unvell.ReoGrid.EtoRenderer
{
    public static class Extensions
    {

        public static Eto.Drawing.PointF ToEto(this unvell.ReoGrid.Graphics.Point point)
        {
            return new Eto.Drawing.PointF(point.X, point.Y);
        }

        public static Eto.Drawing.RectangleF ToEto(this unvell.ReoGrid.Graphics.Rectangle rect)
        {
            return new Eto.Drawing.RectangleF(rect.X, rect.Y,rect.Width, rect.Height);
        }

        public static Eto.Drawing.Color ToEto(this unvell.ReoGrid.Graphics.SolidColor color)
        {
            return new Eto.Drawing.Color(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
        }

        public static unvell.ReoGrid.Graphics.SolidColor  ToSolidColor(this Eto.Drawing.Color color)
        {
            return new Graphics.SolidColor((int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255), (int)(color.A * 255));
        }

    }
}
