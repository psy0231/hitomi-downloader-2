/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy_3;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public interface IPicElement
    {
        bool Selected { get; set; }
        bool MouseIn { get; set; }
        Image Image { get; set; }
        string Label { get; set; }
        HitomiArticle Article { get; set; }
        Font Font { set; }
        PictureBox Picture { get; }
        bool Downloaded { get; set; }
        bool Downloading { get; set; }
        bool Overlap { get; set; }
        void SetImageFromAddress(string addr, int pannelw, int pannelh, bool title = true);
        void Invalidate();
    }

    public partial class PicElement : UserControl, IPicElement
    {
        Image image;
        bool selected = false;
        string label = "";
        Font font;
        bool mouse_enter = false;
        bool downloading = false;
        bool downloaded = false;
        bool overlap = false;
        HitomiArticle ha;
        PictureBox pb = new PictureBox();
        Form parent;

        public PicElement(Form parent, ToolTip tooltip = null)
        {
            InitializeComponent();

            this.Paint += PicElement_Paint;
            this.BackColor = Color.WhiteSmoke;
            this.parent = parent;
            this.DoubleBuffered = true;

            MouseEnter += Thumbnail_MouseEnter;
            MouseLeave += Thumbnail_MouseLeave;
            MouseClick += Thunbnail_MouseClick;
            
            Disposed += OnDispose;
        }

        private void Thumbnail_MouseEnter(object sender, EventArgs e)
        { ((PicElement)sender).MouseIn = true; }
        private void Thumbnail_MouseLeave(object sender, EventArgs e)
        { ((PicElement)sender).MouseIn = false; }
        private void Thunbnail_MouseClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { ((PicElement)sender).Selected = !((PicElement)sender).Selected; } }

        private void PicElement_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            RectangleF LabelRect = new RectangleF(pb.Location.X, pb.Location.Y + pb.Size.Height + 2, pb.Width, 30);
            g.DrawString(label, font, Brushes.Black, LabelRect);
            if (downloaded == false)
            {
                if (selected)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(200, 234, 202, 233));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                    g.DrawRectangle(new Pen(Color.LightPink, 2), 2, 2, this.Width - 4, this.Height - 4);
                }
                else if (mouse_enter)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(100, 234, 202, 233));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 174, 201), 1), 1, 1, this.Width - 2, this.Height - 2);

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
            ViewBuffer buffer = new ViewBuffer();
            buffer.CreateGraphics(pb.Width, pb.Height);

            Graphics g = buffer.g;

            if (downloaded == false)
            {
                if (selected)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(170, 234, 202, 233));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
                }
                else if (mouse_enter)
                {
                    SolidBrush basicBrushes = new SolidBrush(Color.FromArgb(100, 234, 202, 233));
                    g.FillRectangle(basicBrushes, 0, 0, Width, Height);
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

            if (callfrom_panel == false)
            {
                callfrom_paint = true;
                Invalidate();
            }
            callfrom_panel = false;

            buffer.Draw(e.Graphics);
            buffer.Dispose();
        }

        private void Invalidall()
        { callfrom_panel = callfrom_paint = false; Invalidate(); }
        private void Picture_MouseEnter(object sender, EventArgs e)
        { mouse_enter = true; if (!downloading) Invalidall(); }
        private void Picture_MouseLeave(object sender, EventArgs e)
        { mouse_enter = false; if (!downloading) { Invalidall(); } }
        private void Picture_MouseClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { selected = !selected; Invalidall(); } }
        private void Picture_MouseDoubleClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { OpenInfo(); selected = false; } }

        public void OpenInfo()
        {
            (new frmGalleryInfo(parent, this)).Show();
        }

        private void OnDispose(object sender, EventArgs e)
        {
            if (image != null) image.Dispose();
            LogEssential.Instance.PushLog(() => $"Successful disposed! [PicElement] {label}");
        }

        private new void Resize(object sedner, EventArgs e)
        {
            Invalidate();
        }

        public void SetImageFromStream(Stream stream, int panelX, int panelY, bool title = true)
            => SetImage(Image.FromStream(stream), panelX, panelY, title);

        public void SetImage(Image image, int panelX, int panelY, bool title = true) => this.Post(() => {
            Dock = DockStyle.Bottom;
            try
            {
                pb.Location = new Point(3, 3);
                pb.Size = new Size(panelX - 6, panelY - (title ? 30 : 6));
                pb.Image = Image = image;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Paint += Picture_Paint;
                pb.MouseEnter += Picture_MouseEnter;
                pb.MouseLeave += Picture_MouseLeave;
                if (title)
                {
                    pb.MouseClick += Picture_MouseClick;
                    pb.MouseDoubleClick += Picture_MouseDoubleClick;
                }
                int divi = 4 * (title ? 1 : 2);
                var popupSize = new Size(image.Width * 3 / divi, image.Height * 3 / divi);
                if (parent is frmPreview)
                    new LazyPicturePopup(pb, image.Size, LazyPicturePopup.PopupType.Corner);
                else
                    new LazyPicturePopup(pb, popupSize);

                Width = panelX;
                Height = panelY;
                Controls.Add(pb);
            }
            catch { }
        });

        public void SetImageFromAddress(string addr, int pannelw, int pannelh, bool title = true)
        {
            try
            {
                Image image = Image.FromStream(new MemoryStream(File.ReadAllBytes(addr)));
                File.Delete(addr);

                SetImage(image, pannelw, pannelh, title);
            }
            catch { }
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
        public bool Overlap
        { get { return overlap; } set { overlap = value; } }
    }
}
