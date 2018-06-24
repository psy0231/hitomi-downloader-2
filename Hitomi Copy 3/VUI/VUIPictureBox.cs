/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy_3.VUI
{
    public class VUIPictureBox : VUIControl
    {
        public Image Image { get; set; }

        public override void Paint(Graphics g)
        {
            PictureBox pict = new PictureBox();
            pict.Size = Size;
            pict.SizeMode = PictureBoxSizeMode.Zoom;
            var bm = new Bitmap(pict.ClientSize.Width, pict.ClientSize.Height);
            pict.Image = Image;
            pict.DrawToBitmap(bm, pict.ClientRectangle);
            g.DrawImage(bm, pict.Bounds);
            pict.Dispose();
        }
    }
}
