/* Copyright (C) 2018. Hitomi Parser Developers */


using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy_3.VUI
{
    public class VUILabel
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Font Font { get; set; }
        public string Text { get; set; }
        public delegate void Action();
        public Action ClickEvent;

        public void Paint(Graphics g)
        {
            g.DrawString(Text, Font, Brushes.Black, Location);
        }

        public bool Intersect(Point P)
        {
            int sx = P.X - Location.X;
            int sy = P.Y - Location.Y;
            if (Size.Height >= sy && Size.Width >= sx) return true;
            return false;
        }
    }
}
