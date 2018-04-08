/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy_2
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private async void frmSplash_Load(object sender, EventArgs e)
        {
            if (HitomiData.Instance.CheckTagdataExist())
            {
                pbLoad.MarqueeAnimationSpeed = 10;
                await Task.Run(() => HitomiData.Instance.LoadTagdataJson());
            }
            else
            {
                lStatus.Text = "데이터를 다운로드 중 입니다... 이 작업은 수 분 정도 걸립니다.";
                await Task.Run(() => HitomiData.Instance.DownloadTagdata());
            }

            if (HitomiData.Instance.CheckMetadataExist())
            {
                pbLoad.MarqueeAnimationSpeed = 10;
                await Task.Run(() => HitomiData.Instance.LoadMetadataJson());
            }
            else
            {
                lStatus.Text = "데이터를 다운로드 중 입니다... 이 작업은 수 분 정도 걸립니다.";
                await Task.Run(() => HitomiData.Instance.DownloadMetadata());
            }

            (Application.OpenForms["frmMain"] as frmMain).OnTab();
            Application.OpenForms["frmMain"].BringToFront();
            this.Close();
        }
    }
}
