/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Drawing;

namespace Hitomi_Copy_3.VUI
{
    public class VUILabel : VUIControl
    {
        public Font Font { get; set; }
        public string Text { get; set; }

        public override void Paint(Graphics g)
        {
            g.DrawString(Text, Font, Brushes.Black, Location);
        }
    }
}
