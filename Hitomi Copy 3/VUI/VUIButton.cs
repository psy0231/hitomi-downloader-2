/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy_3.VUI
{
    public class VUIButton : VUIControl
    {
        public Font Font { get; set; }
        public string Text { get; set; }

        public VUIButton()
        {
            MouseMoveEvent = mouse_move_action;
            MouseLeaveEvent = mouse_leave_action;
        }

        bool hover = false;
        private void mouse_move_action()
        {
            hover = true;
        }
        private void mouse_leave_action()
        {
            hover = false;
        }

        public override void Paint(Graphics g)
        {
            if (hover == false)
                ButtonRenderer.DrawButton(g, new Rectangle(Location, Size), Text, Font, true, System.Windows.Forms.VisualStyles.PushButtonState.Default); //ControlPaint.DrawButton(g, new Rectangle(Location, Size), ButtonState.Normal);
            else
                ButtonRenderer.DrawButton(g, new Rectangle(Location, Size), Text, Font, true, System.Windows.Forms.VisualStyles.PushButtonState.Pressed);//ControlPaint.DrawButton(g, new Rectangle(Location, Size), ButtonState.Pushed);

        }
    }
}
