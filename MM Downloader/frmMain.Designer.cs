namespace MM_Downloader
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bLoad = new System.Windows.Forms.Button();
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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1240, 446);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lDownloadStatusSize);
            this.tabPage1.Controls.Add(this.lDownloadSize);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.lStatus);
            this.tabPage1.Controls.Add(this.pbTarget);
            this.tabPage1.Controls.Add(this.lvStandBy);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbAddress);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.bLoad);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1232, 418);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(103, 7);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(966, 23);
            this.tbAddress.TabIndex = 1;
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
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(1075, 6);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(151, 23);
            this.bLoad.TabIndex = 2;
            this.bLoad.Text = "다운로드";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // lDownloadStatusSize
            // 
            this.lDownloadStatusSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lDownloadStatusSize.AutoSize = true;
            this.lDownloadStatusSize.Location = new System.Drawing.Point(189, 374);
            this.lDownloadStatusSize.Name = "lDownloadStatusSize";
            this.lDownloadStatusSize.Size = new System.Drawing.Size(36, 15);
            this.lDownloadStatusSize.TabIndex = 47;
            this.lDownloadStatusSize.Text = "0 MB";
            // 
            // lDownloadSize
            // 
            this.lDownloadSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lDownloadSize.AutoSize = true;
            this.lDownloadSize.Location = new System.Drawing.Point(189, 359);
            this.lDownloadSize.Name = "lDownloadSize";
            this.lDownloadSize.Size = new System.Drawing.Size(36, 15);
            this.lDownloadSize.TabIndex = 46;
            this.lDownloadSize.Text = "0 MB";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(96, 374);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 15);
            this.label12.TabIndex = 45;
            this.label12.Text = "다운받은 크기 : ";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(80, 359);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 15);
            this.label11.TabIndex = 44;
            this.label11.Text = "총 다운로드 크기 : ";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 331);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 43;
            this.label9.Text = "다운로드 상태: ";
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatus.AutoSize = true;
            this.lStatus.BackColor = System.Drawing.Color.Transparent;
            this.lStatus.Location = new System.Drawing.Point(189, 331);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(34, 15);
            this.lStatus.TabIndex = 42;
            this.lStatus.Text = "0 / 0";
            // 
            // pbTarget
            // 
            this.pbTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTarget.Location = new System.Drawing.Point(109, 282);
            this.pbTarget.Maximum = 0;
            this.pbTarget.Name = "pbTarget";
            this.pbTarget.Size = new System.Drawing.Size(1038, 35);
            this.pbTarget.TabIndex = 41;
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
            this.lvStandBy.Location = new System.Drawing.Point(109, 76);
            this.lvStandBy.Name = "lvStandBy";
            this.lvStandBy.Size = new System.Drawing.Size(1038, 200);
            this.lvStandBy.TabIndex = 40;
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
            this.label5.Location = new System.Drawing.Point(106, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 39;
            this.label5.Text = "대기중인 항목";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 472);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "MM Downloader 0.1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bLoad;
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
    }
}

