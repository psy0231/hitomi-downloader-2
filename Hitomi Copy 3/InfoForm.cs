/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class InfoForm : Form
    {
        Image image;

        public InfoForm(Image image, Size size)
        {
            InitializeComponent();

            this.image = image;
            this.Size = size;
            Disposed += OnDispose;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
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
