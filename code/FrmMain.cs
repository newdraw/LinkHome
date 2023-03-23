using Aliyun.Acs.Alidns.Model.V20150109;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace LinkHome
{
    public partial class FrmMain : Form
    { 

        [Serializable]
        class DomainInfo
        {
            public string Subdomain;
            public bool DDns;

            [NonSerialized]
            public string ID;

            [NonSerialized]
            public DataGridViewRow Row;

            [NonSerialized]
            public string LastIP;


            public override string ToString()
            {
                return $"{Subdomain}, {DDns}, {Row}";
            }
        }

        const string settingsPath = @".\settings.bin";

        /// <summary>
        /// 本地缓存的域名信息
        /// </summary>
        Dictionary<string, DomainInfo> infos;
        /// <summary>
        /// 配置
        /// </summary>
        Config config;

        AliDDnsUtils ali;

        public FrmMain()
        {
            InitializeComponent(); 
        }   


        T invoke<T>(Func<T> act)
        {
            return (T)this.Invoke(act);
        }

        void invoke<T>(Action act)
        { 
            this.Invoke(act);
        }


        string makeSubdomain(string rr, string domain)
        {
            return rr == "@" ? domain : $"{rr}.{domain}";
        }

        T @try<T>(Func<T> func, T defaultValue)
        {
            try
            {
                return func();
            }
            catch
            {
                return defaultValue;
            }
        }

        void errorHandler(string message)
        {
            lblStatus.Text = message;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            loadSettings();
            notifyIcon.Text = this.Text;
            notifyIcon.Icon = this.Icon;
            FrmMain_Resize(null, null);
            new Thread(refreshProc) { IsBackground = true }.Start();
        }

        void showInfo(string text)
        {
            invoke(() => lblStatus.Text = text);
        }

        void refresh()
        {
            //检查是否进行了配置
            if (string.IsNullOrWhiteSpace(config.AccessKeyID) || string.IsNullOrWhiteSpace(config.AccessKeySecret))
            {
                showInfo("请先在设置中配置AccessKeyID和AccessKeySecret");
                return;
            }

            lock (infos)
            {
                var ip = ServerIP.Get(errorHandler);
                var domains = ali.GetDomains();
                var exist = new HashSet<string>(); //存在于线上的域名
                foreach (var domain in domains) //遍历所有域名
                {
                    foreach (var rec in ali.GetRecords(domain)) //遍历所有解析记录
                    {
                        if (rec.Type != "A" && rec.Type != "CNAME")
                        {
                            continue;
                        }

                        var subdomian = makeSubdomain(rec.RR, rec.DomainName);
                        exist.Add(subdomian);

                        infos.TryGetValue(subdomian, out var info);//本地缓存的域名信息

                        if (info == null) //没有本地缓存则简历
                        {
                            info = new DomainInfo
                            {
                                DDns = false,
                                Subdomain = subdomian
                            };
                            infos.Add(subdomian, info);
                        }

                        if (info.Row == null) //没有对应的列表行则简历
                        {
                            var rowIndex = invoke(() => grid.Rows.Add(subdomian, info.DDns, ""));
                            info.Row = grid.Rows[rowIndex];
                        }

                        //
                        var status = "";
                        if (info.DDns) //打开了ddns
                        {
                            var localDnsStatus = @try(() => Dns.GetHostAddresses(subdomian).FirstOrDefault(i => i.ToString() == ip) != null, false);
                            var dnsConfigStatus = rec._Value == ip && rec.Type == "A";
                            
                            if (localDnsStatus && dnsConfigStatus)
                            {
                                status = "正常";
                            }
                            else
                            {
                                status = $"DNS配置{(dnsConfigStatus ? "正确" : "不正确")}，本地解析{(localDnsStatus ? "正确" : "不正确")}";
                            }

                            //
                            if (rec._Value != ip)
                            {
                                ali.UpdateRecordIP(rec, ip);
                                info.LastIP = ip;
                            }
                        }
                        info.Row.Cells[2].Value = status;

                        //
                        info.ID = rec.RecordId;

                    }
                }

                var notexists = infos.Keys.Where(i => !exist.Contains(i)).ToArray();
                if (notexists.Count() > 0)
                {
                    foreach (var k in notexists)
                    {
                        var i = infos[k];
                        if (i.Row != null)
                        {
                            grid.Rows.Remove(i.Row);
                        }
                        infos.Remove(k);
                    }
                    saveSettings();
                }
                showInfo($"当前IP：{ip} 最后更新时间：{DateTime.Now}");
            }

        }

        void refreshProc()
        {
            while (true)
            {
                refresh();
                Thread.Sleep(config.Interval * 1000);
            }
        }

        private void FrmMain_TextChanged(object sender, EventArgs e)
        {
            notifyIcon.Text = this.Text;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            if (config.HideForm)
            {
                this.Hide();
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                var row = grid.Rows[e.RowIndex];
                var info = infos.Values.First(i => i.Row == row);
                info.DDns = !info.DDns;
                row.Cells[1].Value = info.DDns;
                saveSettings();
                new Thread(refresh).Start();
            }
        }


        void saveSettings()
        {
            using var file = File.OpenWrite(settingsPath);
            using var zip = new GZipStream(file, CompressionLevel.Fastest);
            var formatter = new BinaryFormatter();
            formatter.Serialize(zip, new object[] { infos, config });
        }

        void loadSettings()
        {
            try
            {
                using var file = File.OpenRead(settingsPath);
                using var zip = new GZipStream(file, CompressionMode.Decompress);

                var formatter = new BinaryFormatter();
                var settings = formatter.Deserialize(zip) as object[];
                if (settings == null)
                {
                    return;
                }
                infos = (Dictionary<string, DomainInfo>)settings[0];
                config = (Config)settings[1];
            }
            catch
            {
                infos = new Dictionary<string, DomainInfo>();
                config = new Config();
            }
            remakeAli();

        }

        void remakeAli()
        {
            ali = new AliDDnsUtils
            {
                AccessKeyID = config.AccessKeyID,
                AccessKeySecret = config.AccessKeySecret,
                TTL = config.TTL,
                ErrorHandler = errorHandler
            };
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {

            grid.Location = new Point(0, toolStrip.Bottom);
            grid.Size = new Size(
                ClientSize.Width,
                ClientSize.Height - statusStrip.Height - toolStrip.Height
            );

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new DlgAdd();
            var domains = from i in ali.GetDomains()
                          select i.DomainName;

            while (true)
            {
                if (dlg.Show(domains.ToArray()) != DialogResult.OK)
                {
                    return;
                }

                var key = makeSubdomain(dlg.RR, dlg.Domain);
                if (!infos.ContainsKey(key))
                {
                    ali.SetupRecord(dlg.Domain, dlg.RR, dlg.DDns ? ServerIP.Get(errorHandler) : "0.0.0.0");
                    var info = new DomainInfo
                    {
                        DDns = dlg.DDns,
                        Subdomain = key,
                        Row = null
                    };
                    infos.Add(key, info);
                    refresh();
                    return;
                }

                MessageBox.Show("域名已经存在，请直接在列表中操作。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            lock (infos)
            {
                foreach (DataGridViewRow row in grid.SelectedRows)
                {
                    var info = infos.Values.First(i => i.Row == row);
                    ali.DeleteRecord(info.ID);
                    infos.Remove(info.Subdomain);
                    grid.Rows.Remove(row);
                }
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            using var dlg = new DlgConfig();
            if (dlg.Show(config) == DialogResult.OK)
            {
                config = dlg.Config;
                saveSettings();
                remakeAli();
                refresh();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{this.Text}\r\n\r\n联系作者：newdraw@hotmail.com", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !exit;
            this.Hide();
        }

        bool exit = false;
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "确认要退出吗？\r\n\r\n* 退出后域名不再动态解析。",
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                exit = true;
                this.Close();
            }
        }
    }


}
