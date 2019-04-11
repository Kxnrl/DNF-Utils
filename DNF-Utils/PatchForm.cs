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
    public partial class PatchForm : Form
    {
        const string url = "https://dnf.kxnrl.com/patches/";

        string PatchTypeName;

        public class Patches
        {
            public string pName;
            public string pDesc;
            public string pHash;
            public string pGUID;
            public bool   pDone;

            public Patches(string name, string desc, string hash, string guid)
            {
                pName = name;
                pDesc = desc;
                pHash = hash;
                pGUID = guid;
                pDone = CheckInstalled(guid, hash);
            }
        }

        List<Patches> patches = new List<Patches>();

        static string BuildPath(string guid)
        {
            return Path.Combine(Variables.GameFolder, "ImagePacks2", ".patch." + guid + ".NPK");
        }

        public PatchForm()
        {
            InitializeComponent();

            PatchList.DefaultCellStyle.SelectionBackColor = PatchList.DefaultCellStyle.BackColor;
            PatchList.DefaultCellStyle.SelectionForeColor = PatchList.DefaultCellStyle.ForeColor;

            Progressbar.Value = 100;
            Progressbar.Maximum = 100;
        }

        private void PatchForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.icon;

            Utils.NPKScanner.Scan();

            switch (Variables.PatchMode)
            {
                case Variables.PatchType.Character:
                    Text = "角色补丁";
                    PatchTypeName = "Character";
                    break;
                case Variables.PatchType.Optimization:
                    Text = "优化补丁";
                    PatchTypeName = "Optimization";
                    break;
                case Variables.PatchType.Miscellaneous:
                    Text = "其他补丁";
                    PatchTypeName = "Miscellaneous";
                    break;
                //case Variables.PatchType.Theme:
                //    Text = "自定义主题";
                //    PatchTypeName = "Theme";
                //    break;
                default:
                    MessageBox.Show("发生未知错误!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ((MainForm)Owner).Activate();
                    Close();
                    Dispose();
                    return;
            }

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
                        var file = Path.Combine(Variables.BaseFolder, PatchTypeName + ".list");

                        try
                        {
                            using (var http = new WebClient())
                            {
                                http.DownloadFile("https://dnf.kxnrl.com/" + PatchTypeName + ".list", file);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError("LoadListError [{0}] Exception: {1}", PatchTypeName, ex.Message);
                            MessageBox.Show("连接到远程服务器失败:" + Environment.NewLine + ex.Message + Environment.NewLine +
                                            "将尝试读取本地文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (!File.Exists(file))
                        {
                            ActionLabel.Text = "补丁列表文件已损坏...";
                            throw new Exception ("补丁列表文件已损坏");
                        }

                        var data = File.ReadAllText(file).Split('\n');
                        if (data.Length == 0)
                        {
                            // null
                            ActionLabel.Text = "补丁列表为空";
                            throw new Exception("补丁列表为空");
                        }

                        patches.Clear();

                        foreach (var patch in data)
                        {
                            var split = patch.Split('|');
                            if (split.Length != 4)
                            {
                                // ???
                                continue;
                            }

                            patches.Add(new Patches(split[0], split[1], split[2].ToLowerInvariant(), split[3]));
                        }

                        Progressbar.Value = Progressbar.Maximum;
                        ActionLabel.Text = "初始化完成...";

                        foreach (var p in patches)
                        {
                            PatchList.Rows.Add(p.pGUID, p.pName, p.pDesc, p.pDone ? "卸载" : "安装");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("初始化补丁列表失败:" + Environment.NewLine + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogError("DownloadData [{0}] Exception: {1}", "ThreadInvoker", ex.Message);
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

        #region 下载文件
        private long DownloadFile(string url, string file, string text, out string error)
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
                                            " " + "正在下载" + " " + text + " ..." + "       " +
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

                    return new FileInfo(file).Length;
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

        private static bool CheckInstalled(string guid, string hash)
        {
            var file = BuildPath(guid);

            if (File.Exists(file))
            {
                if (MD5.Match(file, hash))
                {
                    return true;
                }
                File.Delete(file);
            }
            return false;
        }

        private void PatchList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var p = patches[e.RowIndex];

            if (e.ColumnIndex != 3)
            {
                MessageBox.Show("名称: " + p.pName + Environment.NewLine +
                            "介绍: " + p.pDesc + Environment.NewLine +
                            "哈希: " + p.pHash + Environment.NewLine +
                            "标识: " + p.pGUID, 
                            PatchList.Rows[e.RowIndex].Cells["pName"].Value.ToString());
            }
            else
            {
                SetEnabled(false);

                var file = BuildPath(p.pGUID);

                if (PatchList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "安装" && !p.pDone && !File.Exists(file))
                {
                    var size = DownloadFile(url + p.pHash + ".NPK", file, p.pName, out string error);
                    if (size > 0 && SetFileAttribute(file, ref error) && File.Exists(file))
                    {
                        var npk = Utils.NPKHelper.ReadNPK(file, out Utils.NPKHelper.FileMode fm);
                        var crt = new StringBuilder(1024);
                        var ret = DialogResult.Yes;
                        var nct = 0;

                        foreach (var img in npk)
                        {
                            if (PackageManager.ListIMG.Contains(img))
                            {
                                foreach (var text in PackageManager.DictIMG[img].Split('|'))
                                {
                                    if (crt.ToString().Contains(text))
                                    {
                                        continue;
                                    }
                                    crt.AppendFormat("{0}{1}", text, '\n');
                                }
                                nct++;
                            }
                        }

                        if (nct > 0)
                        {
                            var complex = crt.ToString();
                            foreach (var text in complex.Split('\n'))
                            {
                                var name = text.Trim();
                                if (name.Length < 36)
                                {
                                    continue;
                                }

                                var data = PackageManager.GetNpkData(name);
                                if (text.Equals(data.GUID))
                                {
                                    complex = complex.Replace(name, data.Name);
                                }
                            }

                            ret = MessageBox.Show("检查到该补丁与之前安装的主题或其他补丁冲突: " + Environment.NewLine +
                                                  "================" + Environment.NewLine +
                                                  complex, 
                                                  "检测到冲突", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button2);
                        }

                        if (ret == DialogResult.Yes)
                        {
                            patches[e.RowIndex].pDone = true;
                            PatchList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "卸载";
                            ActionLabel.Text = "安装 [" + p.pName + "] 成功!";
                            try
                            {
                                PackageManager.AppendNPK(npk, file);
                                PackageManager.SaveNpkData(file, new PackageManager.NpkData(p.pName, p.pDesc, p.pHash, p.pGUID, (ulong)new FileInfo(file).Length, PackageManager.NpkType.Patch));
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch { }
                            ActionLabel.Text = "已取消安装 [" + p.pName + "] !";
                        }
                        /*
                        MessageBox.Show("安装 [" + p.pName + "] 成功!" + Environment.NewLine +
                                "=========================" + Environment.NewLine +
                                "大小: " + data / 1024 + " KB" + Environment.NewLine +
                                "哈希: " + MD5.Calc(file) + Environment.NewLine +
                                "匹配: " + p.pHash,
                                p.pName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                       */
                    }
                    else
                    {
                        MessageBox.Show("下载 [" + p.pName + "] 失败:" + Environment.NewLine + error, p.pName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                        patches[e.RowIndex].pDone = false;
                        PatchList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "安装";
                        ActionLabel.Text = "卸载 [" + p.pName + "] 成功!";
                    }
                    catch { }
                }

                SetEnabled(true);
            }
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

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要清空所有第三方补丁吗?", "嘤嘤嘤",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                != DialogResult.Yes)
            {
                // abr
                return;
            }

            try
            {
                var NPKFileDir = Path.Combine(Variables.GameFolder, "ImagePacks2");
                var totalBytes = 0ul;
                foreach (var file in Directory.GetFiles(NPKFileDir, "*.NPK", SearchOption.TopDirectoryOnly))
                {
                    if (file.StartsWith(Path.Combine(NPKFileDir, "sprite")))
                    {
                        // ignore offical
                        continue;
                    }

                    totalBytes += (ulong)new FileInfo(file).Length;
                    File.Delete(file);
                }

                var unit = Unit.Byte(totalBytes, out int lvl);
                var lvls = Math.Pow(1024, lvl);
                var curl = totalBytes / lvls;
                MessageBox.Show("清除所有第三方补丁成功!" + Environment.NewLine +
                                "================" + Environment.NewLine +
                                "已释放 [" + curl.ToString("f2") + unit + "] 空间",
                                "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ((MainForm)Owner).Activate();
                Close();
                Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogError("ClearPatchError Exception: {0}", ex.Message);
                MessageBox.Show("清除补丁失败!" + Environment.NewLine +
                                "================" + Environment.NewLine + ex.Message,
                                "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SetEnabled(bool enabled)
        {
            PatchList.Enabled = enabled;
            Button_Clear.Enabled = enabled;
            Label_Kxnrl.Enabled = enabled;
        }
    }
}
