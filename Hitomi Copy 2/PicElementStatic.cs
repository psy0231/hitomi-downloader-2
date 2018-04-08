/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy_2
{
    public partial class PicElementStatic : UserControl
    {
        Form parent;
        HitomiArticle ha;
        PictureBox pb = new PictureBox();
        Image image;
        string label = "";
        Font font;
        InfoForm info;
        bool mouse_enter = false;

        public PicElementStatic(Form parent)
        {
            InitializeComponent();
        }

        private void Thumbnail_MouseEnter(object sender, EventArgs e)
        { ((PicElementStatic)sender).MouseIn = true; }
        private void Thumbnail_MouseLeave(object sender, EventArgs e)
        { ((PicElementStatic)sender).MouseIn = false; }
        private new void Resize(object sedner, EventArgs e)
        { Invalidate(); }
        public new virtual void Dispose()
        { image.Dispose(); base.Dispose(); }

        private void PicElement_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            RectangleF LabelRect = new RectangleF(pb.Location.X, pb.Location.Y + pb.Size.Height + 2, pb.Width, 30);
            g.DrawString(label, font, Brushes.Black, LabelRect);
            
            if (mouse_enter)
            {
                SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(100, 203, 226, 233));
                g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                g.DrawRectangle(new Pen(Color.FromArgb(0, 120, 215), 1), 1, 1, this.Width - 2, this.Height - 2);
            }

            if (callfrom_paint == false)
            {
                callfrom_panel = true;
                pb.Invalidate();
            }
            callfrom_paint = false;
        }

        bool callfrom_paint = false;
        bool callfrom_panel = false;

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            if (mouse_enter)
            {
                SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(100, 203, 226, 233));
                e.Graphics.FillRectangle(basicBrushes, 0, 0, Width, Height);
            }

            if (callfrom_panel == false)
            {
                callfrom_paint = true;
                Invalidate();
            }
            callfrom_panel = false;
        }

        public void SetImageFromAddress(string addr, int pannelw, int pannelh)
        {
            pb.Location = new Point(3, 3);
            pb.Size = new Size(pannelw - 6, pannelh - 30);
            pb.Image = image = Image.FromFile(addr);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Paint += Picture_Paint;
            pb.MouseEnter += Picture_MouseEnter;
            pb.MouseLeave += Picture_MouseLeave;
            pb.MouseClick += Picture_MouseClick;
            pb.MouseMove += Picture_MouseMove;
            pb.MouseDoubleClick += Picture_MouseDoubleClick;
            info = new InfoForm(Image);
            info.Size = new Size(image.Width * 3 / 4, image.Height * 3 / 4);
            this.Width = pannelw;
            this.Height = pannelh;
            this.Controls.Add(pb);
        }

        private void Invalidall()
        { callfrom_panel = callfrom_paint = false; Invalidate(); }
        private void Picture_MouseEnter(object sender, EventArgs e)
        { mouse_enter = true; info.Location = Cursor.Position; info.Show(); Invalidall(); }
        private void Picture_MouseLeave(object sender, EventArgs e)
        { mouse_enter = false; info.Location = Cursor.Position; info.Hide(); Invalidall(); }
        private void Picture_MouseMove(object sender, EventArgs e)
        { info.Location = new Point(Cursor.Position.X + 15, Cursor.Position.Y);  }
        private void Picture_MouseClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) {  Invalidall(); } }
        private void Picture_MouseDoubleClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) {  } }

        public bool MouseIn
        { get { return mouse_enter; } set { mouse_enter = value; Invalidate(); } }
        public Image Image
        { get { return image; } set { image = value; } }
        public string Label
        { get { return label; } set { label = value; } }
        public HitomiArticle Article
        { get { return ha; } set { ha = value; } }
        public override Font Font
        { set { font = value; } }
        public PictureBox Picture
        { get { return pb; } }
    }
}
