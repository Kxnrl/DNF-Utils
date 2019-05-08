using Kxnrl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
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
                    using (var registry = Registry.LocalMachine.CreateSubKey(IFEOKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        foreach (var app in IFEOList)
                        {
                            var sub = registry.CreateSubKey(app, RegistryKeyPermissionCheck.ReadWriteSubTree);
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

                MessageBox.Show("若安全管家/助手提示IFEO劫持(系统映像劫持)请选择信任本程序." + Environment.NewLine + "本程序利用Windows IFEO功能对全家桶进行拦截." + Environment.NewLine + "详情可百度IFEO", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (var app in IFEOList)
                {
                    try
                    {
                        using (var reg = Registry.LocalMachine.CreateSubKey(IFEOKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                        using (var sub = reg.CreateSubKey(app, RegistryKeyPermissionCheck.ReadWriteSubTree))
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
                            var r = GetPermission(path, FileSystemRights.Read);
                            var w = GetPermission(path, FileSystemRights.Write);
                            var e = GetPermission(path, FileSystemRights.ExecuteFile);

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

            //https://stackoverflow.com/questions/2978612/how-to-check-if-a-windows-file-is-readable-writable
            private readonly static WindowsIdentity _identity = WindowsIdentity.GetCurrent();
            protected static bool GetPermission(string path, FileSystemRights right)
            {
                try
                {
                    var fs = File.GetAccessControl(path);

                    foreach (FileSystemAccessRule fsar in fs.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                    {
                        if (fsar.IdentityReference == _identity.User && fsar.FileSystemRights.HasFlag(right) && fsar.AccessControlType == AccessControlType.Allow)
                        {
                            return true;
                        }
                        //else if (_identity.Groups.Contains(fsar.IdentityReference) && fsar.FileSystemRights.HasFlag(right) && fsar.AccessControlType == AccessControlType.Allow)
                        //{
                        //    return true;
                        //}
                    }
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
                catch (UnauthorizedAccessException)
                {
                    return false;
                }
                catch (Exception e)
                {
                    throw new FileException(path, e.Message);
                }

                return false;
            }

            static readonly FileSystemAccessRule ACL_Deny  = new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Deny);
            static readonly FileSystemAccessRule ACL_Allow = new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow);

            public static bool SetAccess(bool deny)
            {
                var count = 0;

                foreach (var file in Files)
                {
                    var path = Path.Combine(Variables.GameFolder, FilePath, file);

                    try
                    {
                        var fs = File.GetAccessControl(path);

                        if (deny)
                        {
                            fs.RemoveAccessRule(ACL_Allow);
                            fs.AddAccessRule(ACL_Deny);
                        }
                        else
                        {
                            fs.RemoveAccessRule(ACL_Deny);
                            fs.AddAccessRule(ACL_Allow);
                        }

                        File.SetAccessControl(path, fs);

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
