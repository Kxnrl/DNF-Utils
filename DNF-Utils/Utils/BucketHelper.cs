using Kxnrl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace DNF_Utils.Utils
{
    class BucketHelper
    {
        public class IEFO
        {
            const string IFEOKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options";

            static readonly List<string> IFEOList = new List<string>()
            {
                "TPHelper.exe",
                "TPWeb.exe",
                "tgp_gamead.exe",
                "AdvertDialog.exe",
                "AdvertTips.exe",
                "BackgroundDownloader.exe",
                "qbclient.exe"
            };

            public static string BuildPath(string application)
            {
                var path = Path.Combine(Variables.BaseFolder, "DNF-Redirect.exe");
                return path + " " + "\"" + application + "\"";
            }

            public static bool CheckIFEO()
            {
                var count = 0;

                try
                {
                    using (var registry = Registry.LocalMachine.CreateSubKey(IFEOKey, true))
                    {
                        foreach (var app in IFEOList)
                        {
                            var sub = registry.CreateSubKey(app, true);
                            var val = sub.GetValue("Debugger", "Not Set", RegistryValueOptions.None)?.ToString();
                            var ter = BuildPath(app);

                            if (ter.Equals(val))
                            {
                                // match
                                count++;
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Logger.LogError("检查IFEO时发生异常." + Environment.NewLine + Ex.Message);
                    MessageBox.Show("检查全家桶组件时发生异常: " + Environment.NewLine + Ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return count == IFEOList.Count;
            }

            public static bool SetIFEO(bool deny)
            {
                var count = 0;

                if (!ProcKiller.KillAll(new string[] { "DNF", "GameLoader", "CrossProxy", "TPHelper", "tgp_gamead", "TPHelper.Installer", "CrossProxy", "TenioDL", "TQMCenter" }))
                    return false;

                MessageBox.Show("若安全管家/助手提示IFEO劫持(系统映像劫持)请选择信任本程序." + Environment.NewLine + "本程序利用Windows IFEO功能对全家桶进行拦截." + Environment.NewLine + "详情可百度IFEO", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (var app in IFEOList)
                {
                    try
                    {
                        using (var reg = Registry.LocalMachine.CreateSubKey(IFEOKey, true))
                        using (var sub = reg.CreateSubKey(app, true))
                        {
                            if (deny)
                            {
                                sub.SetValue("Debugger", BuildPath(app), RegistryValueKind.String);
                            }
                            else
                            {
                                sub.DeleteValue("Debugger", false);
                            }

                            count++;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new IFEOException(IFEOKey, app, e.Message);
                    }
                }

                return count == IFEOList.Count;
            }
        }

        public class FileAccess
        {
            const string FilePath = @"start\Cross\Apps\DNFAD";

            static readonly List<string> Files = new List<string>()
            {
                "DNFADApp.dll",
                "GameDataPlatformClient.dll",
                "res.vfs"
            };

            public static bool CheckAccess()
            {
                var count = 0;

                foreach (var file in Files)
                {
                    var path = Path.Combine(Variables.GameFolder, FilePath, file);

                    try
                    {
                        if (File.Exists(path))
                        {
                            var r = ACLHelper.FileAccess.GetPermission(path, FileSystemRights.Read);
                            var w = ACLHelper.FileAccess.GetPermission(path, FileSystemRights.Write);
                            var e = ACLHelper.FileAccess.GetPermission(path, FileSystemRights.ExecuteFile);

                            if (!r && !w && !e)
                            {
                                count++;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.LogError("FileCheckError [{0}] Exception: {1}", file, e.Message);
                    }
                }

                return count == Files.Count;
            }

            public static bool SetAccess(bool deny)
            {
                var count = 0;

                foreach (var file in Files)
                {
                    var path = Path.Combine(Variables.GameFolder, FilePath, file);

                    try
                    {
                        ACLHelper.FileAccess.SetPermission(path, deny);
                        count++;
                    }
                    catch (Exception e)
                    {
                        throw new FileException(file, e.Message);
                    }
                }

                return count == Files.Count;
            }
        }
    }
}
