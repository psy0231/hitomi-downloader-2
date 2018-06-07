/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class InfoForm : Form
    {
        Image image;

        public InfoForm(Image image)
        {
            InitializeComponent();

            this.image = image;
            Disposed += OnDispose;
        }

        private void OnDispose(object sender, EventArgs e)
        {
            image.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.DrawImage(image, 0, 0, Width, Height);
        }
        
    }
}
