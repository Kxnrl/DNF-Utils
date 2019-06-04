using Kxnrl;
using System;
using System.Threading;
using System.Windows.Forms;

namespace DNF_Utils
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icon;
            Text = "DNF实用工具集 v" + Variables.Version.Version + "      " + "by Kyle";

            // 检查全家桶
            Button_TXBucket.Text = ((Utils.BucketHelper.IEFO.CheckIFEO() && Utils.BucketHelper.FileAccess.CheckAccess()) ? "恢复" : "禁用") + "TX全家桶";

            // 检查蓝屏参数
            if (!Variables.IsMetroWindows)
            {
                Button_BlueScreen.Enabled = false;
                Button_BlueScreen.Text = "您的操作的系统无需修复挂机蓝屏!";
            }
            else
            {
                Button_BlueScreen.Text = (Utils.BlueScreen.Check() ? "还原" : "修复") + "Win8/8.1/10挂机蓝屏";
            }

            // CPU熔断漏洞
            Button_MeltdownSpectre.Text = (Utils.MeltdownSpectre.Check() ? "还原" : "禁用") + "熔断/幽灵补丁";

            // Active
            Activate();
            var myhandle = Handle;
            new Thread(() =>
            {
                Thread.Sleep(3000);
                Win32Api.SetForegroundWindow(myhandle);
            })
            {
                Priority = ThreadPriority.Lowest,
                IsBackground = true
            };
        }

        private void Button_TXBucket_Click(object sender, EventArgs e)
        {
            var from = (Button)sender;
            var text = from.Text;
            var hndl = text.Contains("禁用");
            var ifeo = false;
            var file = false;

            try
            {
                ifeo = Utils.BucketHelper.IEFO.SetIFEO(hndl);
            }
            catch (IFEOException ex)
            {
                MessageBox.Show(text + " " + "失败!" + Environment.NewLine + "=========================" + Environment.NewLine + ex.Message,
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                file = Utils.BucketHelper.FileAccess.SetAccess(hndl);
            }
            catch (FileException ex)
            {
                MessageBox.Show(text + " " + "失败!" + Environment.NewLine + "=========================" + Environment.NewLine + ex.Message,
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (ifeo && file)
            {
                MessageBox.Show(text.Replace("一键", "") + "成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                from.Text = hndl ? text.Replace("禁用", "恢复") : text.Replace("恢复", "禁用");
            }
        }

        private void Button_BlueScreen_Click(object sender, EventArgs e)
        {
            var from = (Button)sender;
            var text = from.Text;
            var hndl = text.Contains("还原");

            if (Utils.BlueScreen.SetFixed(hndl))
            {
                if (MessageBox.Show((hndl ? "还原" : "修复") + "成功!" + Environment.NewLine + "将在下次重启电脑后生效!" + Environment.NewLine + "是否需要立即重启?",
                    "成功", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Shell.Exec("shutdown.exe", "-r -t 0");
                }
                from.Text = hndl ? text.Replace("还原", "修复") : text.Replace("修复", "还原");
            }
        }

        private void Button_MeltdownSpectre_Click(object sender, EventArgs e)
        {
            var from = (Button)sender;
            var text = from.Text;
            var hndl = text.Contains("还原");

            if (Utils.MeltdownSpectre.SetFixed(hndl))
            {
                if (MessageBox.Show((hndl ? "还原" : "禁用") + "成功!" + Environment.NewLine + "将在下次重启电脑后生效!" + Environment.NewLine + "是否需要立即重启?",
                    "成功", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Shell.Exec("shutdown.exe", "-r -t 0");
                }
                from.Text = hndl ? text.Replace("还原", "禁用") : text.Replace("禁用", "还原");
            }
        }

        private void Button_Cleaner_Click(object sender, EventArgs e)
        {
            var pure = !Utils.Cleaner.Check();

            if (pure)
            {
                MessageBox.Show("您的电脑很干净!" + Environment.NewLine + "暂时未发现更新残留以及TX全家桶残留", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var totalBytes = Utils.Cleaner.DeleteAll();

            if (totalBytes > 0)
            {
                var unit = Unit.Byte(totalBytes, out int lvl);
                var lvls = Math.Pow(1024, lvl);
                var curl = totalBytes / lvls;
                MessageBox.Show("清理完成!" + Environment.NewLine + "已释放 [" + curl.ToString("f2") + unit + "] 空间", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button_PatchCharacter_Click(object sender, EventArgs e)
        {
            Variables.PatchMode = Variables.PatchType.Character;
            var child = new PatchForm();
            child.ShowDialog(this);
        }

        private void Button_PatchOptimized_Click(object sender, EventArgs e)
        {
            Variables.PatchMode = Variables.PatchType.Optimization;
            var child = new PatchForm();
            child.ShowDialog(this);
        }

        private void Button_PatchMisc_Click(object sender, EventArgs e)
        {
            Variables.PatchMode = Variables.PatchType.Miscellaneous;
            var child = new PatchForm();
            child.ShowDialog(this);
        }

        private void Button_PatchTheme_Click(object sender, EventArgs e)
        {
            Variables.PatchMode = Variables.PatchType.Theme;
            var child = new ThemeForm();
            child.ShowDialog(this);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.lastRunningVersion = Variables.Version.Version;
            Settings.lastRunningDate = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }
}
