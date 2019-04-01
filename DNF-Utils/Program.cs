using Kxnrl;
using System;
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
                if (!Directory.Exists(Variables.BaseFolder))
                {
                    // base folder
                    Directory.CreateDirectory(Variables.BaseFolder);
                }

                // extract All data
                File.WriteAllBytes(Path.Combine(Variables.BaseFolder, "DNF-Redirect.exe"), Properties.Resources.DNF_Redirect);
                

                // detect windows version
                if (Environment.OSVersion.Version.Major == 10 ||
                   (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2))
                {
                    // Win8 Win8.1 Win10
                    Variables.IsMetroWindows = true;
                }

                // check game
                Variables.GameFolder = Utils.Finder.Find();
                if (string.IsNullOrEmpty(Variables.GameFolder))
                {
                    throw new Exception("您的电脑尚未安装DNF");
                }

                // Load Library
                AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
                {
                    var filename = new AssemblyName(e.Name).Name;
                    return Assembly.LoadFrom(Path.Combine(Variables.BaseFolder, filename)); ;
                };

                // check new version
                Utils.Updater.CheckVersion(out Variables.VersionInfo version);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandler_CurrentThread);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler_AppDomain);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void ExceptionHandler_AppDomain(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            Logger.LogError("ExceptionHandler_AppDomain: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }

        private static void ExceptionHandler_CurrentThread(object sender, ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            Logger.LogError("ExceptionHandler_CurrentThread: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }
    }
}
