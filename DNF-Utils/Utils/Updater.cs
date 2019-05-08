using Kxnrl;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DNF_Utils.Utils
{
    class Updater
    {
        public static uint Major
        {
            get { return currentVersion(0); }
        }

        public static uint Minor
        {
            get { return currentVersion(1); }
        }

        public static uint Builld
        {
            get { return currentVersion(2); }
        }

        public static uint currentVersion (int index)
        {
            try
            {
                var s = Variables.Version.Version.Split('.');
                return uint.Parse(s[index]);
            }
            catch { return 0; }
        }

        public static void CheckVersion(out Variables.VersionInfo versionInfo)
        {
            versionInfo = new Variables.VersionInfo();
            var file = Path.Combine(Variables.BaseFolder, "cache", "version.db");

            try
            {
                if (File.Exists(file))
                {
                    // deleted
                    File.Delete(file);
                }

                using (var http = new ExWebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    http.Encoding = Encoding.UTF8;
                    http.DownloadFile("https://dnf.kxnrl.com/version.ini", file);

                    versionInfo.Version = GetIniString(file, "Version", "version");

                    string[] latest = versionInfo.Version.Split('.');

                    if (latest.Length != 3 || 
                        !uint.TryParse(latest[2], out uint latestBuild) ||
                        !uint.TryParse(latest[1], out uint latestMinor) ||
                        !uint.TryParse(latest[0], out uint latestMajor)
                        )
                    {
                        throw new Exception("版本号检查错误 -> 本地版本[" + Variables.Version.Version + "] 远程版本[" + versionInfo.Version + "].");
                    }

                    if (latestMajor  > Major ||
                       (latestMajor == Major && latestMinor  > Minor) ||
                       (latestMajor == Major && latestMinor == Minor && latestBuild > Builld))
                    {
                        versionInfo.Author = GetIniString(file, "Description", "author");
                        versionInfo.Commit = GetIniString(file, "Version", "commit");
                        versionInfo.Date = GetIniString(file, "Version", "date");
                        versionInfo.UpdateURL = GetIniString(file, "Version", "updateUrl");
                        versionInfo.Description = GetIniString(file, "Description", "description");
                        versionInfo.Website = GetIniString(file, "Description", "website");
                        
                        if (MessageBox.Show("发现新版本 [" + versionInfo.Version + "]" + Environment.NewLine + "是否立即更新? ", "发现新版本",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                            == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(versionInfo.UpdateURL);
                            Environment.Exit(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("CheckVersionError Exception: {0}", e.Message);
                MessageBox.Show("检查新版本失败...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
 
        private static string GetIniString(string file, string section, string key)
        {
            var sb = new StringBuilder(128);
            GetPrivateProfileString(section, key, null, sb, 128, file);
            return sb?.ToString();
        }
    }
}