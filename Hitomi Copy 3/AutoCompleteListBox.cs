/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy_2
{
    public partial class AutoCompleteListBox : ListBox
    {
        public int MaxColoredTextLength = 0;

        public AutoCompleteListBox()
        {
            Hide();
            BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            ScrollAlwaysVisible = true;
            DrawMode = DrawMode.OwnerDrawVariable;

            DrawItem += ExListBox_DrawItem;
            MeasureItem += ExListBox_MeasureItem;
        }

        public static Size MeasureText(string Text, Font Font)
        {
            TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix;
            Size szProposed = new Size(int.MaxValue, int.MaxValue);
            Size sz1 = TextRenderer.MeasureText(".", Font, szProposed, flags);
            Size sz2 = TextRenderer.MeasureText(Text + Convert.ToString("."), Font, szProposed, flags);
            return new Size(sz2.Width - sz1.Width, sz2.Height);
        }

        private void ExListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }

            try
            {
                string firstdraw = Items[e.Index].ToString().Substring(0, MaxColoredTextLength);
                e.Graphics.DrawString(firstdraw, Font, Brushes.Orange, new PointF(e.Bounds.X, e.Bounds.Y));
                int mesaure = MeasureText(firstdraw, Font).Width;
                e.Graphics.DrawString(Items[e.Index].ToString().Substring(MaxColoredTextLength), this.Font, Brushes.White, new PointF(e.Bounds.X + mesaure, e.Bounds.Y));
                e.DrawFocusRectangle();
            }
            catch { }
        }

        private void ExListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (this.Items.Count > 0)
            {
                SizeF size = e.Graphics.MeasureString(Items[e.Index].ToString(), Font);
                e.ItemHeight = (int)size.Height;
                e.ItemWidth = (int)size.Width;
            }
        }
    }
}
