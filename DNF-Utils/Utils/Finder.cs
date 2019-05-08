using Kxnrl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DNF_Utils.Utils
{
    class Finder
    {
        private static List<string> whitelist = new List<string>()
        {
            "$windows",
            "$recycle.bin",
            "windows kits",
            "microsoft",
            "blizzard",
            "steamapps",
            "appdata",
            @"c:\windows",
            @"c:\programdata",
            @"c:\temp",
            @"c:\user"
        };

        public static string Find()
        {
            var ret = string.Empty;

            if (Reg(ref ret))
            {
                // find
                return ret;
            }

            if (Default(ref ret))
            {
                // find 
                return ret;
            }

            // Temp disabled Scanner
            //if (Search(ref ret))
            //{
            //    // find
            //    return ret;
            //}

            return string.Empty;
        }

        private static bool Reg(ref string path)
        {
            try
            {
                var reg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).CreateSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\地下城与勇士", RegistryKeyPermissionCheck.ReadWriteSubTree);
                var val = reg.GetValue("InstallSource", string.Empty, RegistryValueOptions.DoNotExpandEnvironmentNames).ToString();

                if (!string.IsNullOrEmpty(val))
                {
                    path = val;
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Logger.LogError("FinderError [{0}] Exception: {1}", "RegistryKey", e.Message);
            }

            return false;
        }

        private static bool Default(ref string path)
        {
            var list = new string[]
            {
                "地下城与勇士",
                Path.Combine("Program Files (x86)", "地下城与勇士"),
                Path.Combine("Program Files", "地下城与勇士"),
                Path.Combine("Program Files (x86)", "腾讯游戏", "地下城与勇士"),
                Path.Combine("网络游戏", "地下城与勇士"),
            };

            foreach (DriveInfo driver in DriveInfo.GetDrives().Where(x => x.IsReady == true))
            {
                try
                {
                    foreach (var c in list)
                    {
                        var p = Path.Combine(driver.RootDirectory.FullName, c);

                        if (!Directory.Exists(p))
                        {
                            //exists?
                            continue;
                        }

                        if (Directory.GetFiles(p, "DNF.exe", SearchOption.TopDirectoryOnly).Length > 0)
                        {
                            // find
                            path = p;
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError("FinderError [{0}] Exception: {1}", driver.RootDirectory.FullName, e.Message);
                }
            }

            return false;
        }

        private static bool Search(ref string path)
        {
            foreach (DriveInfo driver in DriveInfo.GetDrives().Where(x => x.IsReady == true))
            {
                try
                {
                    Foreach(driver.RootDirectory.FullName, ref path);

                    if (!string.IsNullOrEmpty(path))
                    {
                        // done
                        return true;
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception e)
                {
                    Logger.LogError("FinderError [{0}] Exception: {1}", driver.RootDirectory.FullName, e.Message);
                }
            }

            return false;
        }

        private static void Foreach(string path, ref string find)
        {
            var temp = path.ToLower();
            if (whitelist.Contains(temp))
                return;

            var c = 0;
            var d = Directory.GetDirectories(path);

            foreach (var dir in d)
            {
                if (dir.Contains("\\ImagePacks2") || dir.Contains("\\SoundPacks"))
                {
                    // 资源文件夹

                    if (++c == 2)
                    {
                        if (Directory.GetFiles(path, "DNF.exe", SearchOption.TopDirectoryOnly).Length > 0)
                        {
                            find = path;
                            return;
                        }
                    }
                }
            }

            foreach (var dir in d)
            {
                try
                {
                    Foreach(dir, ref find);
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception e)
                {
                    Logger.LogError("FinderError [{0}] Exception: {1}", path, e.Message);
                }
            }
        }
    }
}
