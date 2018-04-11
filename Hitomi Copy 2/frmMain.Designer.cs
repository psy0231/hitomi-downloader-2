namespace Hitomi_Copy_2
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.MainTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.bTidy = new System.Windows.Forms.Button();
            this.lStatusSearch = new System.Windows.Forms.Label();
            this.tbLang = new System.Windows.Forms.TextBox();
            this.bDownload = new System.Windows.Forms.Button();
            this.bCancleAll = new System.Windows.Forms.Button();
            this.bChooseAll = new System.Windows.Forms.Button();
            this.bSearch = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lDownloadStatusSize = new System.Windows.Forms.Label();
            this.lDownloadSize = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lStatus = new System.Windows.Forms.Label();
            this.pbTarget = new System.Windows.Forms.ProgressBar();
            this.lvStandBy = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.bShowSearch = new System.Windows.Forms.Button();
            this.bStatistics = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lvRecommendArtists = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMyTagRank = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pbSync = new System.Windows.Forms.ProgressBar();
            this.bSync = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbDownloadPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbExcludeTag = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox1 = new Hitomi_Copy_2.AutoCompleteListBox();
            this.ImagePanel = new Hitomi_Copy_2.ScrollFixLayoutPanel();
            this.MainTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTab
            // 
            this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTab.Controls.Add(this.tabPage1);
            this.MainTab.Controls.Add(this.tabPage2);
            this.MainTab.Controls.Add(this.tabPage3);
            this.MainTab.Controls.Add(this.tabPage4);
            this.MainTab.Enabled = false;
            this.MainTab.Location = new System.Drawing.Point(12, 12);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(1135, 528);
            this.MainTab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pbLoad);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.bTidy);
            this.tabPage1.Controls.Add(this.lStatusSearch);
            this.tabPage1.Controls.Add(this.tbLang);
            this.tabPage1.Controls.Add(this.bDownload);
            this.tabPage1.Controls.Add(this.bCancleAll);
            this.tabPage1.Controls.Add(this.bChooseAll);
            this.tabPage1.Controls.Add(this.ImagePanel);
            this.tabPage1.Controls.Add(this.bSearch);
            this.tabPage1.Controls.Add(this.tbSearch);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1127, 500);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "검색";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pbLoad
            // 
            this.pbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLoad.Location = new System.Drawing.Point(269, 478);
            this.pbLoad.Maximum = 0;
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(346, 8);
            this.pbLoad.TabIndex = 21;
            this.pbLoad.Visible = false;
            // 
            // bTidy
            // 
            this.bTidy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bTidy.Location = new System.Drawing.Point(639, 471);
            this.bTidy.Name = "bTidy";
            this.bTidy.Size = new System.Drawing.Size(101, 23);
            this.bTidy.TabIndex = 20;
            this.bTidy.Text = "정리";
            this.bTidy.UseVisualStyleBackColor = true;
            this.bTidy.Click += new System.EventHandler(this.bTidy_Click);
            // 
            // lStatusSearch
            // 
            this.lStatusSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatusSearch.AutoSize = true;
            this.lStatusSearch.Location = new System.Drawing.Point(97, 475);
            this.lStatusSearch.Name = "lStatusSearch";
            this.lStatusSearch.Size = new System.Drawing.Size(110, 15);
            this.lStatusSearch.TabIndex = 19;
            this.lStatusSearch.Text = "0 개 항목이 검색됨";
            // 
            // tbLang
            // 
            this.tbLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLang.Location = new System.Drawing.Point(6, 471);
            this.tbLang.Name = "tbLang";
            this.tbLang.ReadOnly = true;
            this.tbLang.Size = new System.Drawing.Size(65, 23);
            this.tbLang.TabIndex = 18;
            this.tbLang.Text = "korean";
            this.tbLang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bDownload
            // 
            this.bDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDownload.Location = new System.Drawing.Point(1020, 471);
            this.bDownload.Name = "bDownload";
            this.bDownload.Size = new System.Drawing.Size(101, 23);
            this.bDownload.TabIndex = 17;
            this.bDownload.Text = "다운로드";
            this.bDownload.UseVisualStyleBackColor = true;
            this.bDownload.Click += new System.EventHandler(this.bDownload_Click);
            // 
            // bCancleAll
            // 
            this.bCancleAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancleAll.Location = new System.Drawing.Point(877, 471);
            this.bCancleAll.Name = "bCancleAll";
            this.bCancleAll.Size = new System.Drawing.Size(101, 23);
            this.bCancleAll.TabIndex = 16;
            this.bCancleAll.Text = "모두 선택 취소";
            this.bCancleAll.UseVisualStyleBackColor = true;
            this.bCancleAll.Click += new System.EventHandler(this.bCancleAll_Click);
            // 
            // bChooseAll
            // 
            this.bChooseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bChooseAll.Location = new System.Drawing.Point(770, 471);
            this.bChooseAll.Name = "bChooseAll";
            this.bChooseAll.Size = new System.Drawing.Size(101, 23);
            this.bChooseAll.TabIndex = 15;
            this.bChooseAll.Text = "모두 선택";
            this.bChooseAll.UseVisualStyleBackColor = true;
            this.bChooseAll.Click += new System.EventHandler(this.bChooseAll_Click);
            // 
            // bSearch
            // 
            this.bSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSearch.Location = new System.Drawing.Point(1027, 6);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(94, 23);
            this.bSearch.TabIndex = 1;
            this.bSearch.Text = "검색";
            this.bSearch.UseVisualStyleBackColor = true;
            this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(6, 6);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(1015, 23);
            this.tbSearch.TabIndex = 0;
            this.tbSearch.Text = "recent:0-25";
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            this.tbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lDownloadStatusSize);
            this.tabPage2.Controls.Add(this.lDownloadSize);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.lStatus);
            this.tabPage2.Controls.Add(this.pbTarget);
            this.tabPage2.Controls.Add(this.lvStandBy);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1127, 500);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "다운로드";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lDownloadStatusSize
            // 
            this.lDownloadStatusSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lDownloadStatusSize.AutoSize = true;
            this.lDownloadStatusSize.Location = new System.Drawing.Point(134, 442);
            this.lDownloadStatusSize.Name = "lDownloadStatusSize";
            this.lDownloadStatusSize.Size = new System.Drawing.Size(36, 15);
            this.lDownloadStatusSize.TabIndex = 29;
            this.lDownloadStatusSize.Text = "0 MB";
            // 
            // lDownloadSize
            // 
            this.lDownloadSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lDownloadSize.AutoSize = true;
            this.lDownloadSize.Location = new System.Drawing.Point(134, 427);
            this.lDownloadSize.Name = "lDownloadSize";
            this.lDownloadSize.Size = new System.Drawing.Size(36, 15);
            this.lDownloadSize.TabIndex = 28;
            this.lDownloadSize.Text = "0 MB";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 442);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 15);
            this.label12.TabIndex = 27;
            this.label12.Text = "다운받은 크기 : ";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 427);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 15);
            this.label11.TabIndex = 26;
            this.label11.Text = "총 다운로드 크기 : ";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 399);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "다운로드 상태: ";
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatus.AutoSize = true;
            this.lStatus.BackColor = System.Drawing.Color.Transparent;
            this.lStatus.Location = new System.Drawing.Point(134, 399);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(34, 15);
            this.lStatus.TabIndex = 21;
            this.lStatus.Text = "0 / 0";
            // 
            // pbTarget
            // 
            this.pbTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTarget.Location = new System.Drawing.Point(48, 361);
            this.pbTarget.Maximum = 0;
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(1038, 35);
            this.pbTarget.TabIndex = 20;
            // 
            // lvStandBy
            // 
            this.lvStandBy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStandBy.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvStandBy.FullRowSelect = true;
            this.lvStandBy.GridLines = true;
            this.lvStandBy.Location = new System.Drawing.Point(48, 70);
            this.lvStandBy.Name = "lvStandBy";
            this.lvStandBy.Size = new System.Drawing.Size(1038, 285);
            this.lvStandBy.TabIndex = 19;
            this.lvStandBy.UseCompatibleStateImageBehavior = false;
            this.lvStandBy.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "큐";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "제목";
            this.columnHeader2.Width = 586;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "이미지 링크";
            this.columnHeader3.Width = 351;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "대기중인 항목";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.bShowSearch);
            this.tabPage3.Controls.Add(this.bStatistics);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.lvRecommendArtists);
            this.tabPage3.Controls.Add(this.lvMyTagRank);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1127, 500);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "통계";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // bShowSearch
            // 
            this.bShowSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bShowSearch.Location = new System.Drawing.Point(864, 438);
            this.bShowSearch.Name = "bShowSearch";
            this.bShowSearch.Size = new System.Drawing.Size(185, 33);
            this.bShowSearch.TabIndex = 16;
            this.bShowSearch.Text = "검색";
            this.bShowSearch.UseVisualStyleBackColor = true;
            this.bShowSearch.Click += new System.EventHandler(this.bShowSearch_Click);
            // 
            // bStatistics
            // 
            this.bStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bStatistics.Location = new System.Drawing.Point(864, 399);
            this.bStatistics.Name = "bStatistics";
            this.bStatistics.Size = new System.Drawing.Size(185, 33);
            this.bStatistics.TabIndex = 11;
            this.bStatistics.Text = "통계";
            this.bStatistics.UseVisualStyleBackColor = true;
            this.bStatistics.Click += new System.EventHandler(this.bStatistics_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(454, 474);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "<추천 작가>";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 474);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "<태그 카운트>";
            // 
            // lvRecommendArtists
            // 
            this.lvRecommendArtists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRecommendArtists.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7});
            this.lvRecommendArtists.FullRowSelect = true;
            this.lvRecommendArtists.GridLines = true;
            this.lvRecommendArtists.Location = new System.Drawing.Point(258, 21);
            this.lvRecommendArtists.MultiSelect = false;
            this.lvRecommendArtists.Name = "lvRecommendArtists";
            this.lvRecommendArtists.Size = new System.Drawing.Size(463, 450);
            this.lvRecommendArtists.TabIndex = 8;
            this.lvRecommendArtists.UseCompatibleStateImageBehavior = false;
            this.lvRecommendArtists.View = System.Windows.Forms.View.Details;
            this.lvRecommendArtists.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRecommendArtists_MouseDoubleClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "순위";
            this.columnHeader6.Width = 54;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "작가";
            this.columnHeader4.Width = 126;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "점수";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 147;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "최다태그";
            this.columnHeader7.Width = 96;
            // 
            // lvMyTagRank
            // 
            this.lvMyTagRank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvMyTagRank.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9});
            this.lvMyTagRank.FullRowSelect = true;
            this.lvMyTagRank.GridLines = true;
            this.lvMyTagRank.Location = new System.Drawing.Point(32, 21);
            this.lvMyTagRank.Name = "lvMyTagRank";
            this.lvMyTagRank.Size = new System.Drawing.Size(220, 450);
            this.lvMyTagRank.TabIndex = 7;
            this.lvMyTagRank.UseCompatibleStateImageBehavior = false;
            this.lvMyTagRank.View = System.Windows.Forms.View.Details;
            this.lvMyTagRank.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMyTagRank_MouseDoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "항목";
            this.columnHeader8.Width = 127;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "카운트";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pbSync);
            this.tabPage4.Controls.Add(this.bSync);
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Controls.Add(this.tbDownloadPath);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.tbExcludeTag);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1127, 500);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "설정";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pbSync
            // 
            this.pbSync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSync.Location = new System.Drawing.Point(218, 453);
            this.pbSync.MarqueeAnimationSpeed = 10;
            this.pbSync.Name = "pbSync";
            this.pbSync.Size = new System.Drawing.Size(661, 10);
            this.pbSync.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbSync.TabIndex = 6;
            this.pbSync.Visible = false;
            // 
            // bSync
            // 
            this.bSync.Location = new System.Drawing.Point(845, 34);
            this.bSync.Name = "bSync";
            this.bSync.Size = new System.Drawing.Size(191, 38);
            this.bSync.TabIndex = 5;
            this.bSync.Text = "데이터 동기화";
            this.bSync.UseVisualStyleBackColor = true;
            this.bSync.Click += new System.EventHandler(this.bSync_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(92, 136);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(944, 287);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // tbDownloadPath
            // 
            this.tbDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDownloadPath.Location = new System.Drawing.Point(185, 78);
            this.tbDownloadPath.Name = "tbDownloadPath";
            this.tbDownloadPath.Size = new System.Drawing.Size(851, 23);
            this.tbDownloadPath.TabIndex = 3;
            this.tbDownloadPath.Text = "C:\\Hitomi\\{Artists}\\[{Id}] {Title}\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "다운로드 경로: ";
            // 
            // tbExcludeTag
            // 
            this.tbExcludeTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExcludeTag.Location = new System.Drawing.Point(185, 107);
            this.tbExcludeTag.Name = "tbExcludeTag";
            this.tbExcludeTag.Size = new System.Drawing.Size(851, 23);
            this.tbExcludeTag.TabIndex = 1;
            this.tbExcludeTag.Text = "female:mother, male:anal";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "제외할 태그 : ";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(711, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(433, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Copyright (C) 2018. DCInside Programming Gallery Union. All Rights Reserved.";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(117, 481);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(281, 109);
            this.listBox1.TabIndex = 7;
            this.listBox1.Visible = false;
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // ImagePanel
            // 
            this.ImagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePanel.AutoScroll = true;
            this.ImagePanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ImagePanel.Location = new System.Drawing.Point(6, 35);
            this.ImagePanel.Name = "ImagePanel";
            this.ImagePanel.Size = new System.Drawing.Size(1115, 430);
            this.ImagePanel.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 552);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.MainTab);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1175, 591);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hitomi Copy 2.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MainTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl MainTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bSearch;
        private System.Windows.Forms.TextBox tbSearch;
        private ScrollFixLayoutPanel ImagePanel;
        private System.Windows.Forms.Button bDownload;
        private System.Windows.Forms.Button bCancleAll;
        private System.Windows.Forms.Button bChooseAll;
        private System.Windows.Forms.TextBox tbLang;
        private System.Windows.Forms.TextBox tbExcludeTag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbDownloadPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lStatusSearch;
        private System.Windows.Forms.Label lDownloadStatusSize;
        private System.Windows.Forms.Label lDownloadSize;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.ProgressBar pbTarget;
        private System.Windows.Forms.ListView lvStandBy;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bTidy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvRecommendArtists;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ListView lvMyTagRank;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button bStatistics;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button bShowSearch;
        private System.Windows.Forms.ProgressBar pbSync;
        private System.Windows.Forms.Button bSync;
        private AutoCompleteListBox listBox1;
        private System.Windows.Forms.ProgressBar pbLoad;
    }
}