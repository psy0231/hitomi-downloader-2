/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class ArtistForm : Form
    {
        public ArtistForm()
        {
            InitializeComponent();
        }

        private void ArtistForm_Load(object sender, EventArgs e)
        {
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(linkLabel1);
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://blog.naver.com/h_doujin/220715489809");
        }
    }
}
