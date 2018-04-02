namespace Hitomi_Copy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cbType = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bTidy = new System.Windows.Forms.Button();
            this.bDownload = new System.Windows.Forms.Button();
            this.bCancleAll = new System.Windows.Forms.Button();
            this.bChooseAll = new System.Windows.Forms.Button();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.tbLang = new System.Windows.Forms.TextBox();
            this.lPages = new System.Windows.Forms.Label();
            this.numPages = new System.Windows.Forms.NumericUpDown();
            this.bLoad = new System.Windows.Forms.Button();
            this.tbSearchUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bSearch = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.bStat = new System.Windows.Forms.Button();
            this.boxData = new System.Windows.Forms.GroupBox();
            this.lIndex = new System.Windows.Forms.Label();
            this.bAddEntire = new System.Windows.Forms.Button();
            this.lvSearch = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bDataSearch = new System.Windows.Forms.Button();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbArtists = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTagExclude = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTagInclude = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bDataOpen = new System.Windows.Forms.Button();
            this.bDataNew = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.lStatus = new System.Windows.Forms.Label();
            this.pbTarget = new System.Windows.Forms.ProgressBar();
            this.lvStandBy = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbDownloadPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.이작가로검색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.ImagePanel = new Hitomi_Copy.ScrollFixLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPages)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.boxData.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Korean",
            "Artist",
            "Character",
            "Group",
            "Series",
            "Tag",
            "Type"});
            this.cbType.Location = new System.Drawing.Point(6, 6);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(99, 23);
            this.cbType.TabIndex = 1;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1135, 528);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bTidy);
            this.tabPage1.Controls.Add(this.bDownload);
            this.tabPage1.Controls.Add(this.bCancleAll);
            this.tabPage1.Controls.Add(this.bChooseAll);
            this.tabPage1.Controls.Add(this.tbSex);
            this.tabPage1.Controls.Add(this.tbLang);
            this.tabPage1.Controls.Add(this.lPages);
            this.tabPage1.Controls.Add(this.numPages);
            this.tabPage1.Controls.Add(this.bLoad);
            this.tabPage1.Controls.Add(this.tbSearchUrl);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.bSearch);
            this.tabPage1.Controls.Add(this.ImagePanel);
            this.tabPage1.Controls.Add(this.tbSearch);
            this.tabPage1.Controls.Add(this.cbType);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1127, 500);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "검색";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bTidy
            // 
            this.bTidy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bTidy.Location = new System.Drawing.Point(221, 462);
            this.bTidy.Name = "bTidy";
            this.bTidy.Size = new System.Drawing.Size(101, 23);
            this.bTidy.TabIndex = 15;
            this.bTidy.Text = "정리";
            this.bTidy.UseVisualStyleBackColor = true;
            this.bTidy.Click += new System.EventHandler(this.bTidy_Click);
            // 
            // bDownload
            // 
            this.bDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDownload.Location = new System.Drawing.Point(1020, 463);
            this.bDownload.Name = "bDownload";
            this.bDownload.Size = new System.Drawing.Size(101, 23);
            this.bDownload.TabIndex = 14;
            this.bDownload.Text = "다운로드";
            this.bDownload.UseVisualStyleBackColor = true;
            this.bDownload.Click += new System.EventHandler(this.bDownload_Click);
            // 
            // bCancleAll
            // 
            this.bCancleAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bCancleAll.Location = new System.Drawing.Point(822, 463);
            this.bCancleAll.Name = "bCancleAll";
            this.bCancleAll.Size = new System.Drawing.Size(101, 23);
            this.bCancleAll.TabIndex = 13;
            this.bCancleAll.Text = "모두 선택 취소";
            this.bCancleAll.UseVisualStyleBackColor = true;
            this.bCancleAll.Click += new System.EventHandler(this.bCancleAll_Click);
            // 
            // bChooseAll
            // 
            this.bChooseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bChooseAll.Location = new System.Drawing.Point(715, 463);
            this.bChooseAll.Name = "bChooseAll";
            this.bChooseAll.Size = new System.Drawing.Size(101, 23);
            this.bChooseAll.TabIndex = 12;
            this.bChooseAll.Text = "모두 선택";
            this.bChooseAll.UseVisualStyleBackColor = true;
            this.bChooseAll.Click += new System.EventHandler(this.bChooseAll_Click);
            // 
            // tbSex
            // 
            this.tbSex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSex.Location = new System.Drawing.Point(77, 463);
            this.tbSex.Name = "tbSex";
            this.tbSex.Size = new System.Drawing.Size(65, 23);
            this.tbSex.TabIndex = 11;
            this.tbSex.Text = "female";
            this.tbSex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbLang
            // 
            this.tbLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLang.Location = new System.Drawing.Point(6, 463);
            this.tbLang.Name = "tbLang";
            this.tbLang.ReadOnly = true;
            this.tbLang.Size = new System.Drawing.Size(65, 23);
            this.tbLang.TabIndex = 10;
            this.tbLang.Text = "korean";
            this.tbLang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lPages
            // 
            this.lPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lPages.AutoSize = true;
            this.lPages.Location = new System.Drawing.Point(473, 467);
            this.lPages.Name = "lPages";
            this.lPages.Size = new System.Drawing.Size(23, 15);
            this.lPages.TabIndex = 9;
            this.lPages.Text = "/ 1";
            // 
            // numPages
            // 
            this.numPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numPages.Location = new System.Drawing.Point(397, 463);
            this.numPages.Name = "numPages";
            this.numPages.Size = new System.Drawing.Size(70, 23);
            this.numPages.TabIndex = 8;
            this.numPages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bLoad.Location = new System.Drawing.Point(522, 463);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(101, 23);
            this.bLoad.TabIndex = 7;
            this.bLoad.Text = "로드";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // tbSearchUrl
            // 
            this.tbSearchUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchUrl.Location = new System.Drawing.Point(111, 35);
            this.tbSearchUrl.Name = "tbSearchUrl";
            this.tbSearchUrl.ReadOnly = true;
            this.tbSearchUrl.Size = new System.Drawing.Size(903, 23);
            this.tbSearchUrl.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "검색 주소: ";
            // 
            // bSearch
            // 
            this.bSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSearch.Location = new System.Drawing.Point(1020, 5);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(101, 23);
            this.bSearch.TabIndex = 4;
            this.bSearch.Text = "검색";
            this.bSearch.UseVisualStyleBackColor = true;
            this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(111, 6);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(903, 23);
            this.tbSearch.TabIndex = 2;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pbLoad);
            this.tabPage4.Controls.Add(this.bStat);
            this.tabPage4.Controls.Add(this.boxData);
            this.tabPage4.Controls.Add(this.bDataOpen);
            this.tabPage4.Controls.Add(this.bDataNew);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1127, 500);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "고급검색";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pbLoad
            // 
            this.pbLoad.Location = new System.Drawing.Point(590, 19);
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(503, 14);
            this.pbLoad.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbLoad.TabIndex = 4;
            this.pbLoad.Visible = false;
            // 
            // bStat
            // 
            this.bStat.Enabled = false;
            this.bStat.Location = new System.Drawing.Point(380, 6);
            this.bStat.Name = "bStat";
            this.bStat.Size = new System.Drawing.Size(181, 37);
            this.bStat.TabIndex = 3;
            this.bStat.Text = "통계";
            this.bStat.UseVisualStyleBackColor = true;
            this.bStat.Click += new System.EventHandler(this.bStat_Click);
            // 
            // boxData
            // 
            this.boxData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxData.Controls.Add(this.lIndex);
            this.boxData.Controls.Add(this.bAddEntire);
            this.boxData.Controls.Add(this.lvSearch);
            this.boxData.Controls.Add(this.bDataSearch);
            this.boxData.Controls.Add(this.tbTitle);
            this.boxData.Controls.Add(this.label7);
            this.boxData.Controls.Add(this.tbArtists);
            this.boxData.Controls.Add(this.label6);
            this.boxData.Controls.Add(this.tbTagExclude);
            this.boxData.Controls.Add(this.label5);
            this.boxData.Controls.Add(this.tbTagInclude);
            this.boxData.Controls.Add(this.label3);
            this.boxData.Enabled = false;
            this.boxData.Location = new System.Drawing.Point(6, 49);
            this.boxData.Name = "boxData";
            this.boxData.Size = new System.Drawing.Size(1115, 445);
            this.boxData.TabIndex = 2;
            this.boxData.TabStop = false;
            this.boxData.Text = "Advanced Search";
            // 
            // lIndex
            // 
            this.lIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lIndex.AutoSize = true;
            this.lIndex.Location = new System.Drawing.Point(347, 406);
            this.lIndex.Name = "lIndex";
            this.lIndex.Size = new System.Drawing.Size(122, 15);
            this.lIndex.TabIndex = 12;
            this.lIndex.Text = "0 개의 항목이 검색됨";
            // 
            // bAddEntire
            // 
            this.bAddEntire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddEntire.Location = new System.Drawing.Point(813, 394);
            this.bAddEntire.Name = "bAddEntire";
            this.bAddEntire.Size = new System.Drawing.Size(196, 38);
            this.bAddEntire.TabIndex = 11;
            this.bAddEntire.Text = "모두 추가";
            this.bAddEntire.UseVisualStyleBackColor = true;
            this.bAddEntire.Click += new System.EventHandler(this.bAddEntire_Click);
            // 
            // lvSearch
            // 
            this.lvSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvSearch.FullRowSelect = true;
            this.lvSearch.GridLines = true;
            this.lvSearch.Location = new System.Drawing.Point(55, 150);
            this.lvSearch.Name = "lvSearch";
            this.lvSearch.Size = new System.Drawing.Size(954, 238);
            this.lvSearch.TabIndex = 9;
            this.lvSearch.UseCompatibleStateImageBehavior = false;
            this.lvSearch.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Id";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "제목";
            this.columnHeader5.Width = 291;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "작가";
            this.columnHeader6.Width = 208;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "태그";
            this.columnHeader7.Width = 353;
            // 
            // bDataSearch
            // 
            this.bDataSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDataSearch.Location = new System.Drawing.Point(1015, 34);
            this.bDataSearch.Name = "bDataSearch";
            this.bDataSearch.Size = new System.Drawing.Size(87, 23);
            this.bDataSearch.TabIndex = 8;
            this.bDataSearch.Text = "검색";
            this.bDataSearch.UseVisualStyleBackColor = true;
            this.bDataSearch.Click += new System.EventHandler(this.bDataSearch_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(140, 121);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(869, 23);
            this.tbTitle.TabIndex = 7;
            this.tbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBoxEnterKey_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "제목 : ";
            // 
            // tbArtists
            // 
            this.tbArtists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbArtists.Location = new System.Drawing.Point(140, 92);
            this.tbArtists.Name = "tbArtists";
            this.tbArtists.Size = new System.Drawing.Size(869, 23);
            this.tbArtists.TabIndex = 5;
            this.tbArtists.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBoxEnterKey_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(92, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "작가 : ";
            // 
            // tbTagExclude
            // 
            this.tbTagExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTagExclude.Location = new System.Drawing.Point(140, 63);
            this.tbTagExclude.Name = "tbTagExclude";
            this.tbTagExclude.Size = new System.Drawing.Size(869, 23);
            this.tbTagExclude.TabIndex = 3;
            this.tbTagExclude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBoxEnterKey_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "태그 미포함 : ";
            // 
            // tbTagInclude
            // 
            this.tbTagInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTagInclude.Location = new System.Drawing.Point(140, 34);
            this.tbTagInclude.Name = "tbTagInclude";
            this.tbTagInclude.Size = new System.Drawing.Size(869, 23);
            this.tbTagInclude.TabIndex = 1;
            this.tbTagInclude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBoxEnterKey_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "태그 포함 : ";
            // 
            // bDataOpen
            // 
            this.bDataOpen.Location = new System.Drawing.Point(193, 6);
            this.bDataOpen.Name = "bDataOpen";
            this.bDataOpen.Size = new System.Drawing.Size(181, 37);
            this.bDataOpen.TabIndex = 1;
            this.bDataOpen.Text = "데이터 불러오기";
            this.bDataOpen.UseVisualStyleBackColor = true;
            this.bDataOpen.Click += new System.EventHandler(this.bDataOpen_Click);
            // 
            // bDataNew
            // 
            this.bDataNew.Location = new System.Drawing.Point(6, 6);
            this.bDataNew.Name = "bDataNew";
            this.bDataNew.Size = new System.Drawing.Size(181, 37);
            this.bDataNew.TabIndex = 0;
            this.bDataNew.Text = "새로운 데이터 작성";
            this.bDataNew.UseVisualStyleBackColor = true;
            this.bDataNew.Click += new System.EventHandler(this.bDataNew_ClickAsync);
            // 
            // tabPage2
            // 
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
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 376);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "다운로드 상태: ";
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatus.AutoSize = true;
            this.lStatus.BackColor = System.Drawing.Color.Transparent;
            this.lStatus.Location = new System.Drawing.Point(130, 376);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(34, 15);
            this.lStatus.TabIndex = 9;
            this.lStatus.Text = "0 / 0";
            // 
            // pbTarget
            // 
            this.pbTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTarget.Location = new System.Drawing.Point(44, 338);
            this.pbTarget.Maximum = 0;
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(1038, 35);
            this.pbTarget.TabIndex = 5;
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
            this.lvStandBy.Location = new System.Drawing.Point(44, 113);
            this.lvStandBy.Name = "lvStandBy";
            this.lvStandBy.Size = new System.Drawing.Size(1038, 219);
            this.lvStandBy.TabIndex = 1;
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
            this.label2.Location = new System.Drawing.Point(41, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "대기중인 항목";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbDownloadPath);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1127, 500);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "설정";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbDownloadPath
            // 
            this.tbDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDownloadPath.Location = new System.Drawing.Point(169, 72);
            this.tbDownloadPath.Name = "tbDownloadPath";
            this.tbDownloadPath.Size = new System.Drawing.Size(851, 23);
            this.tbDownloadPath.TabIndex = 1;
            this.tbDownloadPath.Text = "C:\\Hitomi\\{Artists}\\{Title}\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "다운로드 경로: ";
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.이작가로검색ToolStripMenuItem});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(155, 26);
            // 
            // 이작가로검색ToolStripMenuItem
            // 
            this.이작가로검색ToolStripMenuItem.Name = "이작가로검색ToolStripMenuItem";
            this.이작가로검색ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.이작가로검색ToolStripMenuItem.Text = "이 작가로 검색";
            this.이작가로검색ToolStripMenuItem.Click += new System.EventHandler(this.이작가로검색ToolStripMenuItem_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(707, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(433, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Copyright (C) 2018. DCInside Programming Gallery Union. All Rights Reserved.";
            // 
            // ImagePanel
            // 
            this.ImagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePanel.AutoScroll = true;
            this.ImagePanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ImagePanel.Location = new System.Drawing.Point(6, 64);
            this.ImagePanel.Name = "ImagePanel";
            this.ImagePanel.Size = new System.Drawing.Size(1115, 393);
            this.ImagePanel.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 552);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1175, 591);
            this.Name = "frmMain";
            this.Text = "Robust Hitomi Copy Machine 1.6";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPages)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.boxData.ResumeLayout(false);
            this.boxData.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbSearch;
        private ScrollFixLayoutPanel ImagePanel;
        private System.Windows.Forms.Button bSearch;
        private System.Windows.Forms.TextBox tbSearchUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lPages;
        private System.Windows.Forms.NumericUpDown numPages;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.TextBox tbLang;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.Button bCancleAll;
        private System.Windows.Forms.Button bChooseAll;
        private System.Windows.Forms.Button bDownload;
        private System.Windows.Forms.ListView lvStandBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ProgressBar pbTarget;
        private System.Windows.Forms.TextBox tbDownloadPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button bTidy;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem 이작가로검색ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button bDataOpen;
        private System.Windows.Forms.Button bDataNew;
        private System.Windows.Forms.GroupBox boxData;
        private System.Windows.Forms.TextBox tbTagExclude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTagInclude;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbArtists;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvSearch;
        private System.Windows.Forms.Button bDataSearch;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button bAddEntire;
        private System.Windows.Forms.Label lIndex;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bStat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ProgressBar pbLoad;
    }
}