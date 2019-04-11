using Kxnrl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DNF_Utils
{
    public partial class ThemeForm : Form
    {
        class FilesData
        {
            public string fName;
            public string fHash;
            public string fGUID;
            public string fSque;

            public FilesData(string file, string hash, string guid, string sque)
            {
                fName = file;
                fHash = hash;
                fGUID = guid;
                fSque = sque;
            }
        }

        class ThemeData
        {
            public string tName;
            public string tGUID;
            public string tPath;
            public List<FilesData> tFile;

            public ThemeData(string name)
            {
                tName = name;
                tGUID = null;
                tPath = null;
                tFile = null;
            }

            public ThemeData(string name, string guid, string path, List<FilesData> file)
            {
                tName = name;
                tGUID = guid;
                tPath = path;
                tFile = file;
            }

            public override string ToString() { return tName; }
        }

        int defaultIndex = -1;
        int currentInstalled = -1;

        static bool CheckValidation(List<FilesData> files, out int validations)
        {
            validations = 0;

            foreach (var file in files)
            {
                if (CheckInstalled(file))
                {
                    // not installed
                    validations++;
                }
            }

            return validations > 0;
        }

        static string BuildPath(FilesData fd)
        {
            return Path.Combine(Variables.GameFolder, "ImagePacks2", ".theme." + fd.fSque + "." + fd.fGUID + ".NPK");
        }

        static bool CheckInstalled(FilesData fd)
        {
            var file = BuildPath(fd);

            if (File.Exists(file))
            {
                if (MD5.Match(file, fd.fHash))
                {
                    return true;
                }
                File.Delete(file);
            }
            return false;
        }

        public ThemeForm()
        {
            InitializeComponent();
        }

        private void ThemeForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icon;

            Utils.NPKScanner.Scan();

            new Thread(() =>
            {
                Invoke(new Action(() =>
                {
                    ActionLabel.Text = "正在连接服务器...";
                    Application.DoEvents();
                }));

                Thread.Sleep(500);

                Invoke(new Action(() =>
                {
                    try
                    {
                        var file = Path.Combine(Variables.BaseFolder, "Theme.list");

                        try
                        {
                            using (var http = new WebClient())
                            {
                                http.DownloadFile("https://dnf.kxnrl.com/" + "Theme.list", file);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError("LoadListError [{0}] Exception: {1}", "Theme", ex.Message);
                            MessageBox.Show("连接到远程服务器失败:" + Environment.NewLine + ex.Message + Environment.NewLine +
                                            "将尝试读取本地文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (!File.Exists(file))
                        {
                            ActionLabel.Text = "补丁列表文件已损坏...";
                            throw new Exception("补丁列表文件已损坏");
                        }

                        var text = File.ReadAllText(file).Split('\n');
                        if (text.Length == 0)
                        {
                            // null
                            ActionLabel.Text = "补丁列表为空";
                            throw new Exception("补丁列表为空");
                        }

                        defaultIndex = ThemeSelector.Items.Add(new ThemeData("国服DNF原版界面"));
                        ThemeSelector.SelectedIndex = defaultIndex;
                        currentInstalled = defaultIndex;
                        foreach (var theme in LoadThemes(text))
                        {
                            var index = ThemeSelector.Items.Add(theme);
                            CheckValidation(theme.tFile, out int installed);
                            if (installed > 0)
                            {
                                // installed
                                currentInstalled = index;
                                ThemeSelector.SelectedIndex = index;
                            }
                        }
                        ThemeSelector.DropDownStyle = ComboBoxStyle.DropDownList;
                        ThemeSelector.Enabled = true;

                        Progressbar.Value = Progressbar.Maximum;
                        ActionLabel.Text = "初始化完成...";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("初始化补丁列表失败:" + Environment.NewLine + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogError("DownloadData [{0}] Exception: {1} {2} {3}", "ThreadInvoker", ex.Message, Environment.NewLine, ex.StackTrace);
                        Invoke(new Action(() =>
                        {
                            ((MainForm)Owner).Activate();
                            Close();
                            Dispose();
                        }));
                    }
                }));
            }).Start();
        }

        List<ThemeData> LoadThemes(string[] data)
        {
            var themes = new List<ThemeData>();

            foreach (var theme in data)
            {
                var split = theme.Split('|');

                if (split.Length != 3)
                {
                    // except
                    continue;
                }

                var name = split[0];
                var path = split[1];
                var guid = split[2];

                try
                {
                    using (var http = new WebClient())
                    {
                        var files = new List<FilesData>();
                        var bytes = http.DownloadData("https://dnf.kxnrl.com/themes/" + path + ".list");
                        foreach (var result in Encoding.UTF8.GetString(bytes).Split('\n'))
                        {
                            var info = result.Split('|');
                            if (info.Length != 4)
                            {
                                // except
                                continue;
                            }
                            files.Add(new FilesData(info[0], info[2], info[3], info[1]));
                        }
                        if (files.Count == 0)
                        {
                            // wtf?
                            throw new Exception("NullDataSet");
                        }
                        themes.Add(new ThemeData(name, guid, path, files));
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError("LoadTheme [{0}] Exception: {1}{2}", name, e.Message, e.StackTrace);
                }
            }

            return themes;
        }

        private void Label_Kxnrl_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("如果你喜欢这个软件, 请到GitHub点个Star吧.", "嘤嘤嘤",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://github.com/Kxnrl/DNF-Utils");
            }
            else
            {
                System.Diagnostics.Process.Start(Variables.Version.Website);
            }
        }

        private void ThemeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultButton();

            var index = ThemeSelector.SelectedIndex;
            if (index <= defaultIndex)
            {
                Button_Action.Enabled = false;
                Button_Verify.Enabled = false;
                return;
            }

            var theme = (ThemeData)ThemeSelector.Items[index];
            var total = theme.tFile.Count;
            var check = CheckValidation(theme.tFile, out int installed);

            Button_Action.Enabled = true;

            Button_Action.Text = "安装";

            if (installed == total)
            {
                Button_Action.Text = "卸载";
            }
            else if (installed > 0)
            {
                Button_Action.Enabled = false;
                Button_Verify.Enabled = true;
            }

            Label_Verify.Text = installed + " / " + total;
        }

        void SetDefaultButton()
        {
            Button_Action.Enabled = false;
            Button_Verify.Enabled = false;

            Button_Action.Text = "安装";
            Button_Verify.Text = "验证";
        }

        void SetContorlState(bool enabled)
        {
            ThemeSelector.Enabled = enabled;
            Button_Action.Enabled = enabled;
            Button_Verify.Enabled = enabled;
        }

        private void Button_Action_Click(object sender, EventArgs e)
        {
            var theme = (ThemeData)ThemeSelector.Items[ThemeSelector.SelectedIndex];

            if (Button_Action.Text.Equals("卸载"))
            {
                CleanTheme();
                Button_Action.Text = "安装";
                Button_Verify.Enabled = false;
                ActionLabel.Text = "卸载 [" + theme.tName + "] 完成!";
                VerifyLabel(ThemeSelector.SelectedIndex);
                currentInstalled = defaultIndex;
                return;
            }

            if (currentInstalled != ThemeSelector.SelectedIndex && currentInstalled != defaultIndex)
            {
                if (MessageBox.Show("继续安装此主题将会卸载其他主题!", "是否继续?"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                {
                    // abort
                    return;
                }
                CleanTheme(ThemeSelector.SelectedIndex);
            }

            SetContorlState(false);
            var file = DownloadTheme(theme, out uint total, out ulong totalBytes);
            var unit = Unit.Byte(totalBytes, out int lvl);
            var lvls = Math.Pow(1024, lvl);
            var curl = totalBytes / lvls;
            MessageBox.Show("安装成功." + Environment.NewLine +
                            "================" + Environment.NewLine +
                            "新增 " + file + " 个文件" + Environment.NewLine +
                            "大小 " + curl.ToString("f2") + " " + unit,
                            "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetContorlState(true);
            if (total == file)
            {
                Button_Verify.Enabled = false;
                Button_Action.Enabled = true;
                Button_Action.Text = "卸载";
            }
            ActionLabel.Text = "安装 [" + theme.tName + "] 完成!";
            VerifyLabel(ThemeSelector.SelectedIndex);
            currentInstalled = ThemeSelector.SelectedIndex;
        }

        private void Button_Verify_Click(object sender, EventArgs e)
        {
            if (currentInstalled != ThemeSelector.SelectedIndex && currentInstalled != defaultIndex)
            {
                if (MessageBox.Show("继续安装此主题将会卸载其他主题!", "是否继续?"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                {
                    // abort
                    return;
                }
                CleanTheme(ThemeSelector.SelectedIndex);
            }

            SetContorlState(false);
            var theme = (ThemeData)ThemeSelector.Items[ThemeSelector.SelectedIndex];
            var files = DownloadTheme(theme, out uint total, out ulong size);
            MessageBox.Show("验证成功." + Environment.NewLine + 
                            "================" + Environment.NewLine +
                            "损坏/缺失了 " + total + " 个文件" + Environment.NewLine +
                            "修复了 " + files + " 个文件",
                            "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetContorlState(true);
            if (total == files)
            {
                Button_Verify.Enabled = false;
                Button_Action.Enabled = true;
                Button_Action.Text = "卸载";
            }
            ActionLabel.Text = "验证 [" + theme.tName + "] 成功!";
            VerifyLabel(ThemeSelector.SelectedIndex);
            currentInstalled = ThemeSelector.SelectedIndex;
        }

        void VerifyLabel(int themeIndex)
        {
            var theme = (ThemeData)ThemeSelector.Items[themeIndex];
            CheckValidation(theme.tFile, out int install);
            Label_Verify.Text = install + " / " + theme.tFile.Count;
            Application.DoEvents();
        }

        void CleanTheme(int selected = -1)
        {
            for (int index = 0; index < ThemeSelector.Items.Count; ++index)
            {
                var theme = (ThemeData)ThemeSelector.Items[index];

                if (theme.tFile == null || index == selected)
                {
                    // byDefault;
                    continue;
                }

                foreach (var file in theme.tFile)
                {
                    var path = BuildPath(file);
                    try
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.LogError("CleanTheme [{0}] Exception: {1}", theme.tName, e.Message);
                    }
                }
            }

            if (selected != -1)
            {
                currentInstalled = selected;
            }
        }

        int DownloadTheme(ThemeData theme, out uint toinstall, out ulong totalSize)
        {
            var success = 0;
            toinstall = 0;
            totalSize = 0;

            foreach (var file in theme.tFile)
            {
                var path = BuildPath(file);

                if (CheckInstalled(file))
                {
                    // installed
                    continue;
                }

                toinstall++;

                var data = DownloadFile("https://dnf.kxnrl.com/themes/" + file.fHash + ".NPK", path, file.fGUID, out string error);
                if (data > 0 && SetFileAttribute(path, ref error) && File.Exists(path))
                {
                    var size = (ulong)new FileInfo(path).Length;
                    PackageManager.SaveNpkData(path, new PackageManager.NpkData(file.fName, file.fSque, file.fHash, file.fGUID, size, PackageManager.NpkType.Theme));
                    success++;
                    totalSize += data;
                }
                else
                {
                    Logger.LogError("DownloadTheme [{0}] Exception: {1}", file.fName, error);
                }
            }

            return success;
        }

        #region 下载文件
        private ulong DownloadFile(string url, string file, string text, out string error)
        {
            var temp = Path.Combine(Variables.BaseFolder, "tempfile.tmp");

            if (File.Exists(temp))
            {
                File.Delete(temp);
            }

            var totalDownloadedByte = 0L;

            try
            {
                HttpWebRequest web = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)web.GetResponse();

                ActionLabel.Text = "正在下载 [" + text + "] ...";

                using (Stream stream = response.GetResponseStream())
                using (FileStream fs = new FileStream(temp, FileMode.Create))
                {
                    SetProgressBar(0, (int)response.ContentLength);

                    var bufferSize = response.ContentLength > 2048 ? (response.ContentLength > 4086 ? (response.ContentLength > 8192 ? 2048 : 1024) : 512) : 256;
                    byte[] bytes = new byte[bufferSize];
                    int osize = stream.Read(bytes, 0, bytes.Length);

                    while (osize > 0)
                    {
                        totalDownloadedByte = osize + totalDownloadedByte;
                        fs.Write(bytes, 0, osize);
                        osize = stream.Read(bytes, 0, bytes.Length);

                        ActionLabel.Text = "[" + SetProgressBar((int)totalDownloadedByte).ToString("f1") + "%]" +
                                            " " + "正在下载" + " " + text + " ..." + "     " +
                                            " " + totalDownloadedByte / 1024 + "KB" +
                                            " " + "/" +
                                            " " + response.ContentLength / 1024 + "KB";
                        Application.DoEvents();
                    }

                    ActionLabel.Text = "下载 [" + text + "] 完成!";

                    error = string.Empty;

                    fs.Close();

                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    File.Move(temp, file);

                    return (ulong)new FileInfo(file).Length;
                }
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            return 0;
        }

        private double SetProgressBar(int val, int max = -1)
        {
            if (max >= 0)
            {
                Progressbar.Maximum = max;
            }

            Progressbar.Value = val;

            // perc
            var perc = (double)Progressbar.Value * 100 / Progressbar.Maximum;
            Progressbar.CreateGraphics().DrawString(perc.ToString("f0") + "%", new Font("微软雅黑 Light", 15, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Magenta, new PointF(Progressbar.Width / 2 - 15, Progressbar.Height / 2 - 10));
            return perc;
        }

        private bool SetFileAttribute(string file, ref string error)
        {
            if (!File.Exists(file))
            {
                // wtf?
                return false;
            }

            try
            {
                File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.Hidden | FileAttributes.System);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError("SetAttributesError [{0}] Exception: {1}", file, e.Message);
                error = e.Message;
            }

            return false;
        }
        #endregion
    }
}
