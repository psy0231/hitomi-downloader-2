/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy;
using Hitomi_Copy.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Hitomi_Copy_2
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            HitomiDataQuery query = new HitomiDataQuery();
            List<string> positive_data = new List<string>();

            tbSearch.Text.Trim().Split(' ').ToList().ForEach((a) => { if (!a.Contains(":")) positive_data.Add(a.Trim()); });
            query.Common = positive_data;
            foreach (var elem in tbSearch.Text.Trim().Split(' '))
            {
                if (!elem.Contains(":")) continue;
                if (elem.StartsWith("tag:"))
                    if (query.TagInclude == null)
                        query.TagInclude = new List<string>() { elem.Substring("tag:".Length) };
                    else
                        query.TagInclude.Add(elem);
                else if (elem.StartsWith("artist:"))
                    if (query.Artists == null)
                        query.Artists = new List<string>() { elem.Substring("artist:".Length) };
                    else
                        query.Artists.Add(elem);
                else if (elem.StartsWith("series:"))
                    if (query.Series == null)
                        query.Series = new List<string>() { elem.Substring("series:".Length) };
                    else
                        query.Series.Add(elem);
                else if (elem.StartsWith("group:"))
                    if (query.Groups == null)
                        query.Groups = new List<string>() { elem.Substring("group:".Length) };
                    else
                        query.Groups.Add(elem);
                else if (elem.StartsWith("character:"))
                    if (query.Characters == null)
                        query.Characters = new List<string>() { elem.Substring("character:".Length) };
                    else
                        query.Characters.Add(elem);
                else if (elem.StartsWith("tagx:"))
                    if (query.TagExclude == null)
                        query.TagExclude = new List<string>() { elem.Substring("tagx:".Length) };
                    else
                        query.TagExclude.Add(elem);
                else
                {
                    MessageBox.Show($"알 수 없는 규칙입니다. \"{elem}\"", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            var query_result = HitomiDataSearch.Search2(query);
            if (query_result.Count == 0)
            {
                MessageBox.Show("검색된 항목이 없습니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            List<ListViewItem> lvil = new List<ListViewItem>();
            for (int i = 0; i < query_result.Count; i++)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    query_result[i].ID.ToString(),
                    query_result[i].Name,
                    query_result[i].Type,
                    string.Join(",", query_result[i].Artists ?? Enumerable.Empty<string>()),
                    string.Join(",", query_result[i].Groups ?? Enumerable.Empty<string>()),
                    string.Join(",", query_result[i].Parodies ?? Enumerable.Empty<string>()),
                    string.Join(",", query_result[i].Characters ?? Enumerable.Empty<string>()),
                    string.Join(",", query_result[i].Tags ?? Enumerable.Empty<string>())
                }));
            }
            lvil.Sort((a, b) => Convert.ToUInt32(b.SubItems[0].Text).CompareTo(Convert.ToUInt32(a.SubItems[0].Text)));
            lvHistory.Items.Clear();
            lvHistory.Items.AddRange(lvil.ToArray());
        }

        #region 검색창

        int global_position = 0;
        string global_text = "";
        bool selected_part = true;

        private int GetCaretWidthFromTextBox(int pos)
        {
            return TextRenderer.MeasureText(tbSearch.Text.Substring(0, pos), tbSearch.Font).Width;
        }
        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                bSearch.PerformClick();
            else
            {
                if (listBox1.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        listBox1.SelectedIndex = 0;
                        listBox1.Focus();
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        listBox1.Focus();
                    }
                }

                if (selected_part)
                {
                    selected_part = false;
                    if (e.KeyCode != Keys.Back)
                    {
                        tbSearch.SelectionStart = global_position;
                        tbSearch.SelectionLength = 0;
                    }
                }

            }
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            int position = tbSearch.SelectionStart;
            while (position > 0 && tbSearch.Text[position - 1] != ' ')
                position -= 1;

            string word = "";
            for (int i = position; i < tbSearch.TextLength; i++)
            {
                if (tbSearch.Text[i] == ' ') break;
                word += tbSearch.Text[i];
            }

            if (word == "") { listBox1.Visible = false; return; }

            List<string> match = null;
            if (word.Contains(":"))
            {
                if (word.StartsWith("artist:"))
                {
                    word = word.Substring("artist:".Length);
                    position += "artist:".Length;
                    match = HitomiData.Instance.GetArtistList(word);
                }
                else if (word.StartsWith("tag:"))
                {
                    word = word.Substring("tag:".Length);
                    position += "tag:".Length;
                    match = HitomiData.Instance.GetTagList(word);
                }
                else if (word.StartsWith("tagx:"))
                {
                    word = word.Substring("tagx:".Length);
                    position += "tagx:".Length;
                    match = HitomiData.Instance.GetTagList(word);
                }
                else if (word.StartsWith("character:"))
                {
                    word = word.Substring("character:".Length);
                    position += "character:".Length;
                    match = HitomiData.Instance.GetCharacterList(word);
                }
                else if (word.StartsWith("group:"))
                {
                    word = word.Substring("group:".Length);
                    position += "group:".Length;
                    match = HitomiData.Instance.GetGroupList(word);
                }
                else if (word.StartsWith("series:"))
                {
                    word = word.Substring("series:".Length);
                    position += "series:".Length;
                    match = HitomiData.Instance.GetSeriesList(word);
                }
            }

            if (match != null && match.Count > 0)
            {
                listBox1.Visible = true;
                listBox1.Items.Clear();
                foreach (var item in match)
                    listBox1.Items.Add(item);
                listBox1.Location = new Point(tbSearch.Left + GetCaretWidthFromTextBox(position),
                    tbSearch.Top + tbSearch.Font.Height + 5);
                listBox1.MaxColoredTextLength = word.Length;
            }
            else { listBox1.Visible = false; return; }

            global_position = position;
            global_text = word;

            if (e.KeyCode == Keys.Down)
            {
                listBox1.SelectedIndex = 0;
                listBox1.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.Focus();
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                PutStringIntoTextBox(listBox1.Items[0].ToString());
            }
        }

        private void PutStringIntoTextBox(string text)
        {
            tbSearch.Text = tbSearch.Text.Substring(0, global_position) +
                text +
                tbSearch.Text.Substring(global_position + global_text.Length);
            listBox1.Hide();

            tbSearch.SelectionStart = global_position;
            tbSearch.SelectionLength = text.Length;
            tbSearch.Focus();

            global_position = global_position + tbSearch.SelectionLength;
            selected_part = true;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                PutStringIntoTextBox(listBox1.SelectedItem.ToString());
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (listBox1.SelectedItems.Count > 0)
                    PutStringIntoTextBox(listBox1.SelectedItem.ToString());
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                listBox1.Hide();
                tbSearch.Focus();
            }
        }
        #endregion

        private void lvHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                var hitomi_data = HitomiData.Instance.metadata_collection;
                foreach (var metadata in hitomi_data)
                {
                    if (metadata.ID.ToString() == lvHistory.SelectedItems[0].SubItems[0].Text)
                    {
                        (new frmGalleryInfo(this, metadata)).Show();
                        return;
                    }
                }
            }
        }
    }
}
