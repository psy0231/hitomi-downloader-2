namespace Hitomi_Copy
{
    partial class frmStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatistics));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.lvRankGroup = new System.Windows.Forms.ListView();
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.lvRankCharacters = new System.Windows.Forms.ListView();
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRankSeries = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRankArtists = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lvRankTag = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lvHistory = new System.Windows.Forms.ListView();
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1477, 628);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.lvRankGroup);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.lvRankCharacters);
            this.tabPage2.Controls.Add(this.lvRankSeries);
            this.tabPage2.Controls.Add(this.lvRankArtists);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.lvRankTag);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1469, 600);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "순위";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1253, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "<그룹 순위>";
            // 
            // lvRankGroup
            // 
            this.lvRankGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvRankGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21});
            this.lvRankGroup.FullRowSelect = true;
            this.lvRankGroup.GridLines = true;
            this.lvRankGroup.Location = new System.Drawing.Point(1165, 47);
            this.lvRankGroup.Name = "lvRankGroup";
            this.lvRankGroup.Size = new System.Drawing.Size(279, 519);
            this.lvRankGroup.TabIndex = 15;
            this.lvRankGroup.UseCompatibleStateImageBehavior = false;
            this.lvRankGroup.View = System.Windows.Forms.View.Details;
            this.lvRankGroup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRankGroup_MouseDoubleClick);
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "순위";
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "항목";
            this.columnHeader20.Width = 127;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "카운트";
            this.columnHeader21.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(968, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "<캐릭터 순위>";
            // 
            // lvRankCharacters
            // 
            this.lvRankCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvRankCharacters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
            this.lvRankCharacters.FullRowSelect = true;
            this.lvRankCharacters.GridLines = true;
            this.lvRankCharacters.Location = new System.Drawing.Point(880, 47);
            this.lvRankCharacters.Name = "lvRankCharacters";
            this.lvRankCharacters.Size = new System.Drawing.Size(279, 519);
            this.lvRankCharacters.TabIndex = 13;
            this.lvRankCharacters.UseCompatibleStateImageBehavior = false;
            this.lvRankCharacters.View = System.Windows.Forms.View.Details;
            this.lvRankCharacters.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRankCharacters_MouseDoubleClick);
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "순위";
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "항목";
            this.columnHeader17.Width = 127;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "카운트";
            this.columnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lvRankSeries
            // 
            this.lvRankSeries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvRankSeries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader14,
            this.columnHeader15});
            this.lvRankSeries.FullRowSelect = true;
            this.lvRankSeries.GridLines = true;
            this.lvRankSeries.Location = new System.Drawing.Point(595, 47);
            this.lvRankSeries.Name = "lvRankSeries";
            this.lvRankSeries.Size = new System.Drawing.Size(279, 519);
            this.lvRankSeries.TabIndex = 12;
            this.lvRankSeries.UseCompatibleStateImageBehavior = false;
            this.lvRankSeries.View = System.Windows.Forms.View.Details;
            this.lvRankSeries.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRankSeries_MouseDoubleClick);
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "순위";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "항목";
            this.columnHeader14.Width = 127;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "카운트";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lvRankArtists
            // 
            this.lvRankArtists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvRankArtists.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lvRankArtists.FullRowSelect = true;
            this.lvRankArtists.GridLines = true;
            this.lvRankArtists.Location = new System.Drawing.Point(310, 47);
            this.lvRankArtists.Name = "lvRankArtists";
            this.lvRankArtists.Size = new System.Drawing.Size(279, 519);
            this.lvRankArtists.TabIndex = 11;
            this.lvRankArtists.UseCompatibleStateImageBehavior = false;
            this.lvRankArtists.View = System.Windows.Forms.View.Details;
            this.lvRankArtists.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRankArtists_MouseDoubleClick);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "순위";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "항목";
            this.columnHeader10.Width = 127;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "카운트";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(689, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "<시리즈 순위>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "<작가 순위>";
            // 
            // lvRankTag
            // 
            this.lvRankTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvRankTag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader7,
            this.columnHeader8});
            this.lvRankTag.FullRowSelect = true;
            this.lvRankTag.GridLines = true;
            this.lvRankTag.Location = new System.Drawing.Point(25, 47);
            this.lvRankTag.Name = "lvRankTag";
            this.lvRankTag.Size = new System.Drawing.Size(279, 519);
            this.lvRankTag.TabIndex = 7;
            this.lvRankTag.UseCompatibleStateImageBehavior = false;
            this.lvRankTag.View = System.Windows.Forms.View.Details;
            this.lvRankTag.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRankTag_MouseDoubleClick);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "순위";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "항목";
            this.columnHeader7.Width = 127;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "카운트";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "<태그 순위>";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lvHistory);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1469, 600);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "다운로드 히스토리";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.lvHistory.Location = new System.Drawing.Point(6, 6);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(1457, 588);
            this.lvHistory.TabIndex = 8;
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
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1049, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(433, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Copyright (C) 2018. DCInside Programming Gallery Union. All Rights Reserved.";
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1501, 652);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1517, 691);
            this.Name = "frmStatistics";
            this.Text = "Hitomi Statistics";
            this.Load += new System.EventHandler(this.frmStatistics_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvRankTag;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lvRankGroup;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvRankCharacters;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ListView lvRankSeries;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ListView lvRankArtists;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.ColumnHeader columnHeader28;
        private System.Windows.Forms.ColumnHeader columnHeader29;
    }
}