using Kxnrl;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DNF_Utils
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Mutex
            var self = new Mutex(true, "com.kxnrl.dnf.utils", out bool allow);
            if (!allow)
            {
                MessageBox.Show("已有一个实例在运行了...", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            // Exception Handler
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandler_CurrentThread);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler_AppDomain);

            // Visual
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // .NET framework
                CheckDotNetFramework();

                // detect windows version
                if (Environment.OSVersion.Version.Major == 10 ||
                   (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2))
                {
                    // Win8 Win8.1 Win10
                    Variables.IsMetroWindows = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + Environment.NewLine, "致命错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            try
            {
                // create Folders
                CreateFolders();

                // extract All data
                ExtraFiles();

                // handle old file
                ClearCache();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                new Thread(() =>
                {
                    MessageBox.Show("初始化中..." + Environment.NewLine + "================" + Environment.NewLine + "需要一点时间...", ":: DNF-Utils ::", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }).Start();

                // check new version
                Utils.Updater.CheckVersion(out Variables.VersionInfo version);

                // check game
                CheckGame();

                // CloseWB
                CloseTips();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                var dnf = Process.GetProcessesByName("DNF");

                if (dnf.Length > 0)
                {
                    if (MessageBox.Show("侦测到DNF正在运行或正在启动..." + Environment.NewLine + "点击确定将会强制关闭DNF..", ":: DNF-Utils ::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        foreach (var exe in dnf)
                        {
                            exe.Kill();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new MainForm());
        }

        internal static void CheckDotNetFramework()
        {
            var installed = true;

            try
            {
                using (var ndp = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\", false))
                {
                    if (ndp != null && ndp.GetValue("Release") != null)
                    {
                        var version = Convert.ToInt32(ndp.GetValue("Release"));
                        //https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
                        if (version < 461808) //461814
                        {
                            installed = false;
                        }
                    }
                    else
                    {
                        installed = false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("检查.NET运行时发生异常: " + e.Message);
            }

            if (!installed)
            {
                try
                {
                    Process.Start("https://dotnet.microsoft.com/download/thank-you/net472-offline");
                }
                catch { }
                MessageBox.Show("请在弹出的网页中下载并安装.NET Framework 4.7.2", "运行时版本错误");
                throw new Exception("您的电脑尚未安装.NET Framework 4.7.2或更新的版本");
            }
        }

        internal static void CreateFolders()
        {
            // base folder
            Directory.CreateDirectory(Variables.BaseFolder);

            // child
            foreach (var dir in new string[] { "cache", "themes", "patches", "sounds", "packages" })
            {
                Directory.CreateDirectory(Path.Combine(Variables.BaseFolder, dir));
            }
        }

        internal static void ExtraFiles()
        {
            File.WriteAllBytes(Path.Combine(Variables.BaseFolder, "DNF-Redirect.exe"), Properties.Resources.DNF_Redirect);
        }

        internal static void ClearCache()
        {
            foreach (var file in Directory.GetFiles(Path.Combine(Variables.BaseFolder, "cache")))
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }
        }

        internal static void CheckGame()
        {
            Variables.GameFolder = Settings.lastGamePath;
                
            if (string.IsNullOrEmpty(Variables.GameFolder))
            {
                Variables.GameFolder = Utils.Finder.Find();
                //Console.WriteLine("Find [{0}]", Variables.GameFolder);
            }

            if (string.IsNullOrEmpty(Variables.GameFolder))
            {
                CloseTips();

                using (var fileBrowser = new OpenFileDialog())
                {
                    fileBrowser.Multiselect = false;
                    fileBrowser.DereferenceLinks = true;
                    fileBrowser.Title = "无法扫描到您的DNF安装目录, 请手动选择";
                    fileBrowser.Filter = "地下城与勇士|DNF.exe";

                    if (fileBrowser.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("程序已退出...", ":: DNF-Utils ::", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Environment.Exit(-2);
                    }

                    Variables.GameFolder = Path.GetDirectoryName(fileBrowser.FileName);
                    //Console.WriteLine("Set [{0}]", Variables.GameFolder);
                }
            }
        }

        internal static void CloseTips()
        {
            IntPtr mbWnd = Win32Api.FindWindow("#32770", ":: DNF-Utils ::"); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
            {
                Win32Api.SendMessage(mbWnd, Win32Api.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private static void ExceptionHandler_AppDomain(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Logger.LogError("ExceptionHandler_AppDomain: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }

        private static void ExceptionHandler_CurrentThread(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            Logger.LogError("ExceptionHandler_CurrentThread: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }
    }
}
