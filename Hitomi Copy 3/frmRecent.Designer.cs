namespace Hitomi_Copy_3
{
    partial class frmRecent
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
            this.RecommendPannel = new Hitomi_Copy_2.ScrollFixLayoutPanel();
            this.SuspendLayout();
            // 
            // RecommendPannel
            // 
            this.RecommendPannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RecommendPannel.AutoScroll = true;
            this.RecommendPannel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.RecommendPannel.Location = new System.Drawing.Point(12, 12);
            this.RecommendPannel.Name = "RecommendPannel";
            this.RecommendPannel.Size = new System.Drawing.Size(1117, 473);
            this.RecommendPannel.TabIndex = 5;
            // 
            // frmRecent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1141, 497);
            this.Controls.Add(this.RecommendPannel);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmRecent";
            this.Text = "Recently Added";
            this.ResumeLayout(false);

        }

        #endregion

        private Hitomi_Copy_2.ScrollFixLayoutPanel RecommendPannel;
    }
}