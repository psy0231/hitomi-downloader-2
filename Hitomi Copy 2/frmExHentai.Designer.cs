namespace Hitomi_Copy_2
{
    partial class frmExHentai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExHentai));
            this.label1 = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.bLoad = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTags = new System.Windows.Forms.TextBox();
            this.lTitle = new System.Windows.Forms.Label();
            this.lArtist = new System.Windows.Forms.Label();
            this.bDownload = new System.Windows.Forms.Button();
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbProxy = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "다운로드 주소 : ";
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(103, 7);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(966, 23);
            this.tbAddress.TabIndex = 1;
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(1075, 6);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(151, 23);
            this.bLoad.TabIndex = 2;
            this.bLoad.Text = "불러오기";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1240, 412);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbProxy);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.bDownload);
            this.tabPage1.Controls.Add(this.lArtist);
            this.tabPage1.Controls.Add(this.lTitle);
            this.tabPage1.Controls.Add(this.tbTags);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.pbImage);
            this.tabPage1.Controls.Add(this.tbAddress);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.bLoad);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1232, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "로드";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.SystemColors.Control;
            this.pbImage.Location = new System.Drawing.Point(6, 35);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(265, 343);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 3;
            this.pbImage.TabStop = false;
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
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1232, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "다운로드";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label2.Location = new System.Drawing.Point(325, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "작가 : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label3.Location = new System.Drawing.Point(325, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "제목 : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label4.Location = new System.Drawing.Point(325, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "태그 : ";
            // 
            // tbTags
            // 
            this.tbTags.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tbTags.Location = new System.Drawing.Point(389, 113);
            this.tbTags.Multiline = true;
            this.tbTags.Name = "tbTags";
            this.tbTags.ReadOnly = true;
            this.tbTags.Size = new System.Drawing.Size(738, 179);
            this.tbTags.TabIndex = 7;
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.lTitle.Location = new System.Drawing.Point(385, 55);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(42, 21);
            this.lTitle.TabIndex = 8;
            this.lTitle.Text = "제목";
            // 
            // lArtist
            // 
            this.lArtist.AutoSize = true;
            this.lArtist.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.lArtist.Location = new System.Drawing.Point(385, 84);
            this.lArtist.Name = "lArtist";
            this.lArtist.Size = new System.Drawing.Size(42, 21);
            this.lArtist.TabIndex = 9;
            this.lArtist.Text = "작가";
            // 
            // bDownload
            // 
            this.bDownload.Location = new System.Drawing.Point(961, 315);
            this.bDownload.Name = "bDownload";
            this.bDownload.Size = new System.Drawing.Size(166, 43);
            this.bDownload.TabIndex = 10;
            this.bDownload.Text = "다운로드";
            this.bDownload.UseVisualStyleBackColor = true;
            this.bDownload.Click += new System.EventHandler(this.bDownload_Click);
            // 
            // lDownloadStatusSize
            // 
            this.lDownloadStatusSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lDownloadStatusSize.AutoSize = true;
            this.lDownloadStatusSize.Location = new System.Drawing.Point(148, 352);
            this.lDownloadStatusSize.Name = "lDownloadStatusSize";
            this.lDownloadStatusSize.Size = new System.Drawing.Size(36, 15);
            this.lDownloadStatusSize.TabIndex = 38;
            this.lDownloadStatusSize.Text = "0 MB";
            // 
            // lDownloadSize
            // 
            this.lDownloadSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lDownloadSize.AutoSize = true;
            this.lDownloadSize.Location = new System.Drawing.Point(148, 337);
            this.lDownloadSize.Name = "lDownloadSize";
            this.lDownloadSize.Size = new System.Drawing.Size(36, 15);
            this.lDownloadSize.TabIndex = 37;
            this.lDownloadSize.Text = "0 MB";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(55, 352);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 15);
            this.label12.TabIndex = 36;
            this.label12.Text = "다운받은 크기 : ";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 337);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 15);
            this.label11.TabIndex = 35;
            this.label11.Text = "총 다운로드 크기 : ";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 309);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 34;
            this.label9.Text = "다운로드 상태: ";
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatus.AutoSize = true;
            this.lStatus.BackColor = System.Drawing.Color.Transparent;
            this.lStatus.Location = new System.Drawing.Point(148, 309);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(34, 15);
            this.lStatus.TabIndex = 33;
            this.lStatus.Text = "0 / 0";
            // 
            // pbTarget
            // 
            this.pbTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTarget.Location = new System.Drawing.Point(62, 271);
            this.pbTarget.Maximum = 0;
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(1038, 35);
            this.pbTarget.TabIndex = 32;
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
            this.lvStandBy.Location = new System.Drawing.Point(62, 46);
            this.lvStandBy.Name = "lvStandBy";
            this.lvStandBy.Size = new System.Drawing.Size(1038, 219);
            this.lvStandBy.TabIndex = 31;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 30;
            this.label5.Text = "대기중인 항목";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label6.Location = new System.Drawing.Point(442, 328);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "Proxy : ";
            // 
            // tbProxy
            // 
            this.tbProxy.Location = new System.Drawing.Point(514, 326);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Size = new System.Drawing.Size(418, 23);
            this.tbProxy.TabIndex = 12;
            // 
            // frmExHentai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1264, 436);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmExHentai";
            this.Text = "ExHentai Downloader";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lArtist;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.TextBox tbTags;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bDownload;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbProxy;
        private System.Windows.Forms.Label label6;
    }
}