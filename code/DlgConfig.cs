using System;
using System.Diagnostics;
using System.Windows.Forms; 
namespace LinkHome
{
    public partial class DlgConfig : Form
    {
        public DlgConfig()
        {
            InitializeComponent();
        }

        public DialogResult Show(Config config)
        {
            txtAccessKeyID.Text = config.AccessKeyID;
            txtAccessKeySecret.Text = config.AccessKeySecret;
            txtInterval.Value = config.Interval;
            txtTTL.Value = config.TTL;
            chkHideForm.Checked = config.HideForm;
            return this.ShowDialog();
        }

        public Config Config;

        private void btnOK_Click(object sender, EventArgs e)
        {
            var config = new Config();
            config.AccessKeyID = txtAccessKeyID.Text;
            config.AccessKeySecret = txtAccessKeySecret.Text;
            config.Interval = (int)txtInterval.Value;
            config.TTL = (int)txtTTL.Value;
            config.HideForm = chkHideForm.Checked;
            Config = config;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnHowToGetAccessKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ram.console.aliyun.com/manage/ak?spm=5176.21213303.J_6704733920.8.47a753c9LLSirX&scm=20140722.ID_qacard@@gtsqa@@10040.P_180.MO_1342-ST_6739-V_1-ID_qacard@@gtsqa@@10040-OR_rec-RL_accesskeyid");
        }
    }
}
