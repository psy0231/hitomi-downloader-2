/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy;
using Hitomi_Copy_2;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    public partial class PicDetailElement : UserControl, IPicElement
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
        Lazy<InfoForm> info;
        Form parent;

        public PicDetailElement(Form parent, ToolTip tooltip = null)
        {
            InitializeComponent();
            
            this.BackColor = Color.GhostWhite;
            this.parent = parent;
            this.DoubleBuffered = true;
            
            Disposed += OnDispose;
        }

        public PicDetailElement(int index)
        {
            Disposed += OnDispose;
        }

        private void OnDispose(object sender, EventArgs e)
        {
            if (image != null)
                image.Dispose();
            if (info != null && info.IsValueCreated)
                info.Value.Dispose();
            LogEssential.Instance.PushLog(() => $"Successful disposed! [PicDetailElement] {label}");
        }
        
        private void pb_MouseEnter(object sender, EventArgs e)
        { info.Value.Location = Cursor.Position; info.Value.Show(); }
        private void pb_MouseLeave(object sender, EventArgs e)
        { info.Value.Location = Cursor.Position; info.Value.Hide(); }
        private void pb_MouseMove(object sender, MouseEventArgs e)
        { info.Value.Location = new Point(Cursor.Position.X + 15, Cursor.Position.Y); }

        public void SetImageFromAddress(string addr, int pannelw, int pannelh, bool title = true)
        {
            Dock = DockStyle.Bottom;
            try
            {
                using (FileStream fs = new FileStream(addr, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose))
                {
                    pb.Image = image = Image.FromStream(fs);
                }
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                if (title)
                    info = new Lazy<InfoForm>(() => new InfoForm(Image, new Size(image.Width * 3 / 4, image.Height * 3 / 4)));
                else
                    info = new Lazy<InfoForm>(() => new InfoForm(Image, new Size(image.Width * 3 / 4 / 2, image.Height * 3 / 4 / 2)));
            }
            catch { }
        }

        private void PicDetailElement_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Downloading && !Downloaded)
            {
                selected = !selected;
                if (selected) BackColor = Color.FromArgb(200, 234, 202, 233);
                else BackColor = Color.GhostWhite;
            }
        }

        public bool Selected
        { get { return selected; } set { selected = value; if (!Downloading && !Downloaded) { if (selected) BackColor = Color.FromArgb(200, 234, 202, 233); else BackColor = Color.GhostWhite; } } }
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
        { get { return downloading; } set { downloading = value; if (Downloading) BackColor = Color.FromArgb(200, 200, 200, 0); else BackColor = Color.FromArgb(200, 200, 130, 130); } }
        public bool Overlap
        { get { return overlap; } set { overlap = value; } }

        private void PicDetailElement_Load(object sender, EventArgs e)
        {
            lTitle.Text = ha.Title;
            lArtist.Text = string.Join(",", ha.Artists ?? Enumerable.Empty<string>());
            lSeries.Text = string.Join(",", ha.Series ?? Enumerable.Empty<string>());
            lGroup.Text = string.Join(",", ha.Groups ?? Enumerable.Empty<string>());
            lCharacter.Text = string.Join(",", ha.Characters ?? Enumerable.Empty<string>());
            lDate.Text = HitomiDate.estimate_datetime(Convert.ToInt32(ha.Magic)).ToString();
            if (ha.Tags != null)
                ha.Tags.ToList().ForEach((a) =>
                {
                    a = HitomiCommon.LegalizeTag(a);
                    if (a.StartsWith("female:")) AddTagToPanel(a.Substring("female:".Length), 1);
                    else if (a.StartsWith("male:")) AddTagToPanel(a.Substring("male:".Length), 2);
                    else AddTagToPanel(a, 0);
                });
        }

        private void AddTagToPanel(string tag_data, int image)
        {
            Button b = new Button();
            b.Text = tag_data;
            b.UseVisualStyleBackColor = true;
            b.AutoSize = false;
            b.Font = new Font(Font.Name, 10);
            using (Graphics cg = this.CreateGraphics())
            {
                b.Width = (int)cg.MeasureString(tag_data, b.Font).Width + 13;
            }
            b.Height = 26;
            b.Padding = new Padding(0);
            b.Margin = new Padding(0);
            b.MouseClick += ButtonMessage;

            if (image == 1)
                b.ForeColor = Color.DeepPink;
            else if (image == 2)
                b.ForeColor = Color.DarkBlue;

            flowLayoutPanel1.Controls.Add(b);
        }

        public void ButtonMessage(object sender, EventArgs e)
        {
            if (((Button)sender).ForeColor == Color.DeepPink)
                (new frmTagInfo(parent, "female:" + ((Button)sender).Text)).Show();
            else if (((Button)sender).ForeColor == Color.DarkBlue)
                (new frmTagInfo(parent, "male:" + ((Button)sender).Text)).Show();
            else
                (new frmTagInfo(parent, ((Button)sender).Text)).Show();
        }
        
        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                (new frmArtistInfo(parent, ha.Artists[0])).Show();
            }
            catch { }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                (new frmGroupInfo(parent, ha.Groups[0])).Show();
            }
            catch { }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                (new frmPreview(parent, ha.Magic)).Show();
            }
            catch { }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"https://hitomi.la/galleries/{ha.Magic}.html");
        }

        private void PicDetailElement_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            (new frmGalleryInfo(parent, this)).Show(); selected = false; BackColor = Color.GhostWhite;
        }
    }
}
