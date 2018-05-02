/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    public partial class MarqueeColorBar : UserControl
    {
        Timer timer;
        int value = 0;
        bool flip = false;

        public MarqueeColorBar()
        {
            InitializeComponent();

            this.Paint += MarqueeColorBar_Paint;
            this.DoubleBuffered = true;

            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 10;
            timer.Start();
        }

        private void MarqueeColorBar_Paint(object sender, PaintEventArgs e)
        {
            double Width = this.Width + this.Width / 5;
            double speed = Width * Math.Tanh(((double)Width / 2 - value) * 4 / Width) / 2 + Width / 2 - this.Width / 5;
            int width = this.Width / 5;

            ViewBuffer buffer = new ViewBuffer();
            buffer.CreateGraphics(this.Width, Height);
            buffer.g.FillRectangle(new SolidBrush(Color.FromArgb(200, 231,113,189)), (float)speed, 0, width, Height);
            buffer.Draw(e.Graphics);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double Width = this.Width + this.Width / 5;
            double speed = Width * Math.Tanh(((double)Width / 2 - value) * 4 / Width) / 2 + Width / 2 - this.Width / 5;

            if (flip == false)
                value += 8;
            else
                value -= 8;

            if (speed < -(double)this.Width / 5 / 2)
                flip = true;
            else if (speed > this.Width - this.Width / 5 / 2)
                flip = false;
            
            this.Invalidate();
        }
    }
}
