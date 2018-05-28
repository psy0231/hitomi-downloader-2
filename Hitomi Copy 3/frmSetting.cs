/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using System;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            tgWI.Checked = HitomiSetting.Instance.GetModel().WaitInfinite;
            tbWT.Text = HitomiSetting.Instance.GetModel().WaitTimeout.ToString();
            tgSJ.Checked = HitomiSetting.Instance.GetModel().SaveJson;
            tbRPS.Text = HitomiSetting.Instance.GetModel().RecommendPerScroll.ToString();
            tbTMA.Text = HitomiSetting.Instance.GetModel().TextMatchingAccuracy.ToString();
            tbMTS.Text = HitomiSetting.Instance.GetModel().MaximumThumbnailShow.ToString();
            tgRNM.Checked = HitomiSetting.Instance.GetModel().RecommendNMultipleWithLength;
            tgRL.Checked = HitomiSetting.Instance.GetModel().RecommendLanguageALL;
            tgRA.Checked = HitomiSetting.Instance.GetModel().ReplaceArtistsWithTitle;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            int tmp;
            if (!int.TryParse(tbWT.Text, out tmp))
            {
                MessageBox.Show( "Wait Timeout은 숫자여야 합니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(tbRPS.Text, out tmp))
            {
                MessageBox.Show("Recommend Per Scroll은 숫자여야 합니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(tbTMA.Text, out tmp))
            {
                MessageBox.Show("Text Matching Accuracy는 숫자여야 합니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            HitomiSetting.Instance.GetModel().WaitInfinite = tgWI.Checked;
            HitomiSetting.Instance.GetModel().WaitTimeout = Convert.ToInt32(tbWT.Text);
            HitomiSetting.Instance.GetModel().SaveJson = tgSJ.Checked;
            HitomiSetting.Instance.GetModel().RecommendPerScroll = Convert.ToInt32(tbRPS.Text);
            HitomiSetting.Instance.GetModel().TextMatchingAccuracy = Convert.ToInt32(tbTMA.Text);
            HitomiSetting.Instance.GetModel().MaximumThumbnailShow = Convert.ToInt32(tbMTS.Text);
            HitomiSetting.Instance.GetModel().RecommendNMultipleWithLength = tgRNM.Checked;
            HitomiSetting.Instance.GetModel().RecommendLanguageALL = tgRL.Checked;
            HitomiSetting.Instance.GetModel().ReplaceArtistsWithTitle = tgRA.Checked;
            HitomiSetting.Instance.Save();
            Close();
        }

        private void tgWI_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "파일 다운로드시 응답이 없을 경우 무제한 기다릴 것의 여부를 설정합니다.";
        }

        private void tbWT_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "무제한 기다리지 않는다면 몇 ms기다릴지 설정합니다.";
        }

        private void tgSJ_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "Article를 다운로드할 때 마다 폴더에 .json파일을 생성합니다.";
        }

        private void tbRPS_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "작가추천에서 스크롤할 때마다 몇 개의 추천목록을 보여줄지 설정합니다.";
        }

        private void tbTMA_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "작가추천에서 중복된 Article를 표시하지않도록 설정하는 고유값입니다.";
        }

        private void tbMTS_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "표시할 수 있는 검색 결과의 최대 개수입니다.";
        }

        private void tgRNM_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "작가추천 목록 생성시 해당 작가의 Article 수를 곱하지 않습니다. 이 설정은 단순한 작가추천 결과를 제공합니다.";
        }

        private void tgRL_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "작가추천시 모든 언어를 기반으로 목록을 생성합니다.";
        }

        private void tgRA_MouseEnter(object sender, EventArgs e)
        {
            tbInfo.Text = "{Aritsts}를 단어의 첫 글자 대문자로하여 치환합니다.";
        }

        private void MouseLeave_Event(object sender, EventArgs e)
        {
            tbInfo.Text = "";
        }

    }
}
