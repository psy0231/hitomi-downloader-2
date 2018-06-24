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
        public Action MouseClickEvent;

        public abstract void Paint(Graphics g);

        public bool Intersect(Point P)
        {
            int sx = P.X - Location.X;
            int sy = P.Y - Location.Y;
            if (Size.Height >= sy && Size.Width >= sx) return true;
            return false;
        }

        #region Event Wrapper
        bool mouse_enter = true;

        public void MouseMove(Point clientPosition)
        {
            if (Intersect(clientPosition))
            {
                if (mouse_enter == false)
                {
                    mouse_enter = true;
                    MouseEnterEvent();
                    MouseMoveEvent();
                }
                else
                {
                    MouseMoveEvent();
                }
            }
            else if (mouse_enter == true)
            {
                MouseLeaveEvent();
                mouse_enter = false;
            }
        }

        public void MouseLeave()
        {
            if (mouse_enter == true)
            {
                MouseLeaveEvent();
                mouse_enter = false;
            }
        }
        #endregion

    }
}
