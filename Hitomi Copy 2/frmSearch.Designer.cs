namespace Hitomi_Copy_2
{
    partial class frmSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearch));
            this.lvHistory = new System.Windows.Forms.ListView();
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.bSearch = new System.Windows.Forms.Button();
            this.listBox1 = new Hitomi_Copy_2.AutoCompleteListBox();
            this.SuspendLayout();
            // 
            // lvHistory
            // 
            this.lvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader29,
            this.columnHeader24,
            this.columnHeader27,
            this.columnHeader25,
            this.columnHeader28,
            this.columnHeader26});
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.GridLines = true;
            this.lvHistory.Location = new System.Drawing.Point(12, 41);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(1454, 592);
            this.lvHistory.TabIndex = 10;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            this.lvHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvHistory_MouseDoubleClick);
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "아이디";
            this.columnHeader22.Width = 79;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "제목";
            this.columnHeader23.Width = 354;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "타입";
            this.columnHeader29.Width = 86;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "작가";
            this.columnHeader24.Width = 105;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "그룹";
            this.columnHeader27.Width = 151;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "시리즈";
            this.columnHeader25.Width = 144;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "캐릭터";
            this.columnHeader28.Width = 143;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "태그";
            this.columnHeader26.Width = 356;
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(12, 12);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(1354, 23);
            this.tbSearch.TabIndex = 11;
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            this.tbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyUp);
            // 
            // bSearch
            // 
            this.bSearch.Location = new System.Drawing.Point(1372, 11);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(94, 23);
            this.bSearch.TabIndex = 12;
            this.bSearch.Text = "검색";
            this.bSearch.UseVisualStyleBackColor = true;
            this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(599, 268);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(281, 109);
            this.listBox1.TabIndex = 13;
            this.listBox1.Visible = false;
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1478, 645);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.bSearch);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.lvHistory);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSearch";
            this.Text = "검색";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.ColumnHeader columnHeader28;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button bSearch;
        private AutoCompleteListBox listBox1;
    }
}