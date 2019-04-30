using Kxnrl;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
            var self = new Mutex(true, "com.kxnrl.dnf", out bool allow);
            if (!allow)
            {
                MessageBox.Show("已有一个实例在运行了...", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            try
            {
                // Load Library
                //AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
                //{
                //    var filename = new AssemblyName(e.Name).Name;
                //    return Assembly.LoadFrom(Path.Combine(Variables.BaseFolder, filename)); ;
                //};

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
                File.WriteAllBytes(Path.Combine(Variables.BaseFolder, "DNF-Redirect.exe"), Properties.Resources.DNF_Redirect);

                // check game
                Variables.GameFolder = Utils.Finder.Find();
                if (string.IsNullOrEmpty(Variables.GameFolder))
                {
                    throw new Exception("您的电脑尚未安装DNF");
                }

                // check new version
                Utils.Updater.CheckVersion(out Variables.VersionInfo version);

                // handle old file

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandler_CurrentThread);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler_AppDomain);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
