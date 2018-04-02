/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class PicElement : UserControl
    {
        Image image;
        bool selected = false;
        string label = "";
        Font font;
        bool mouse_enter = false;
        bool downloading = false;
        bool downloaded = false;
        HitomiArticle ha;
        PictureBox pb = new PictureBox();
        InfoForm info;
        Form parent;

        public PicElement(Form parent, ToolTip tooltip = null)
        {
            InitializeComponent();

            this.Paint += PicElement_Paint;
            this.BackColor = Color.WhiteSmoke;
            this.parent = parent;
        }

        private void PicElement_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            RectangleF LabelRect = new RectangleF(pb.Location.X, pb.Location.Y + pb.Size.Height + 2, pb.Width, 30);
            g.DrawString(label, font, Brushes.Black, LabelRect);
            if (downloaded == false)
            {
                if (selected)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(200, 203, 226, 233));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                    g.DrawRectangle(new Pen(Color.LightSkyBlue, 2), 2, 2, this.Width - 4, this.Height - 4);
                }
                else if (mouse_enter)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(100, 203, 226, 233));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                    g.DrawRectangle(new Pen(Color.FromArgb(0,120,215), 1), 1, 1, this.Width - 2, this.Height - 2);
                }
            }
            else
            {
                if (downloading)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(200, 200, 200, 0));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                }
                else
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(200, 200, 130, 130));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                }
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
            if (downloaded == false)
            {
                if (selected)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(170, 203, 226, 233));
                    e.Graphics.FillRectangle(basicBrushes, 0, 0, Width, Height);
                }
                else if (mouse_enter)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(100, 203, 226, 233));
                    e.Graphics.FillRectangle(basicBrushes, 0, 0, Width, Height);
                }
            }
            else
            {
                if (downloading)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(200, 200, 200, 0));
                    e.Graphics.FillRectangle(basicBrushes, 0, 0, Width, Height);
                }
                else
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(200, 200, 130, 130));
                    e.Graphics.FillRectangle(basicBrushes, 0, 0, Width, Height);
                }
            }

            if (callfrom_panel == false)
            {
                callfrom_paint = true;
                Invalidate();
            }
            callfrom_panel = false;
        }
        private void Invalidall()
        { callfrom_panel = callfrom_paint = false; Invalidate(); }
        private void Picture_MouseEnter(object sender, EventArgs e)
        { mouse_enter = true; info.Location = Cursor.Position; info.Show(); Invalidall(); }
        private void Picture_MouseLeave(object sender, EventArgs e)
        { mouse_enter = false; info.Location = Cursor.Position; info.Hide(); Invalidall(); }
        private void Picture_MouseMove(object sender, EventArgs e)
        { info.Location = new Point(Cursor.Position.X+15,Cursor.Position.Y); /*info.BringToFront();*/ }
        private void Picture_MouseClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { selected = !selected; Invalidall(); } }
        private void Picture_MouseDoubleClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { OpenInfo(); selected = false; } }

        public void OpenInfo()
        {
            (new frmGalleryInfo(parent, this)).Show();
        }

        public void OpenUrl()
        {
            System.Diagnostics.Process.Start(HitomiDef.HitomiGalleryAddress + ha.Magic + ".html");
        }

        public new virtual void Dispose()
        {
            image.Dispose();
            base.Dispose();
        }

        private new void Resize(object sedner, EventArgs e)
        {
            Invalidate();
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
            info.Size = new Size(image.Width*3/4, image.Height*3/4);
            this.Width = pannelw;
            this.Height = pannelh;
            this.Controls.Add(pb);
        }

        public void SetToolTip(ToolTip tooltip)
        {
            StringBuilder builder = new StringBuilder();
            //builder.Append($"매직 : {ha.Magic}\n");
            builder.Append($"제목 : {ha.Title}\n");
            builder.Append($"작가 : {ha.Artists}\n");
            builder.Append($"시리즈 : {ha.Series}\n");
            builder.Append($"타입 : {ha.Types}\n");
            builder.Append($"태그 : {string.Join(",", ha.Tags)}\n");
            tooltip.SetToolTip(this, builder.ToString());
            tooltip.SetToolTip(pb, builder.ToString());
        }

        public void SetContextMenuStrip(ContextMenuStrip menu)
        {
            ContextMenuStrip = menu;
            pb.ContextMenuStrip = menu;
        }

        public bool Selected
        { get { return selected; } set { selected = value; Invalidate(); } }
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
        public bool Downloaded
        { get { return downloaded; } set { downloaded = value; } }
        public bool Downloading
        { get { return downloading; } set { downloading = value; } }
    }
}
