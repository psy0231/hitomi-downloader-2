/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Drawing;

namespace Hitomi_Copy_3.VUI
{
    public abstract class VUIControl
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public delegate void Action();
        public Action ClickEvent;

        public Action MouseEnterEvent;
        public Action MouseMoveEvent;
        public Action MouseLeaveEvent;

        public abstract void Paint(Graphics g);

        public bool Intersect(Point P)
        {
            int sx = P.X - Location.X;
            int sy = P.Y - Location.Y;
            if (Size.Height >= sy && Size.Width >= sx) return true;
            return false;
        }
    }
}
