namespace Hitomi_Copy_2
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.lStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MarqueeColorBar = new Hitomi_Copy_3.MarqueeColorBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lStatus.Location = new System.Drawing.Point(183, 75);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(148, 15);
            this.lStatus.TabIndex = 1;
            this.lStatus.Text = "데이터를 로딩 중 입니다...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 13F);
            this.label2.Location = new System.Drawing.Point(181, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hitomi Copy Machine V3.0";
            // 
            // MarqueeColorBar
            // 
            this.MarqueeColorBar.BackColor = System.Drawing.SystemColors.Control;
            this.MarqueeColorBar.Location = new System.Drawing.Point(186, 109);
            this.MarqueeColorBar.Name = "MarqueeColorBar";
            this.MarqueeColorBar.Size = new System.Drawing.Size(382, 18);
            this.MarqueeColorBar.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Hitomi_Copy_3.Properties.Resources.i14434391215;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(580, 163);
            this.Controls.Add(this.MarqueeColorBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hitomi Copy";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSplash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Label label2;
        private Hitomi_Copy_3.MarqueeColorBar MarqueeColorBar;
    }
}

