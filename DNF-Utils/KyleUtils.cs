namespace Kxnrl
{
    class IFEOException : System.Exception
    {
        private string _Section;
        private string _Execute;
        private string _Message;
        private bool   _ByBlock;

        public IFEOException() : base() { }
        public IFEOException(string message) : base(message) { _Message = message; }
        public IFEOException(string format, params object[] args) : base(string.Format(format, args)) { _Message = string.Format(format, args); }
        public IFEOException(string message, System.Exception innerException) : base(message, innerException) { _Message = message; }

        // custom
        public IFEOException(string section, string application, bool block, string message) : base (message)
        {
            _Section = section;
            _Execute = application;
            _Message = message;
            _ByBlock = block;
        }

        public override string Message
        {
            get => _ByBlock ?
                    string.Format("节点: {0}{1}程序: {2}{3}异常: {4}{5}{6}{7}{8}{9}{10}",
                        _Section, System.Environment.NewLine,
                        _Execute, System.Environment.NewLine,
                        _Message, System.Environment.NewLine,
                        "发生此类异常一般为程序权限被360/电脑管家等杀毒软件拦截", System.Environment.NewLine,
                        "若您的杀软已退出", System.Environment.NewLine,
                        "请重启程序并关闭杀软的自我保护功能") : 
                    string.Format("节点: {0}{1}程序: {2}{3}异常: {4}",
                        _Section, System.Environment.NewLine,
                        _Execute, System.Environment.NewLine,
                        _Message);

        }
    }

    class FileException : System.Exception
    {
        private string _File;
        private string _Message;

        public FileException() : base() { }
        public FileException(string message) : base(message) { _Message = message; }
        public FileException(string format, params object[] args) : base(string.Format(format, args)) { _Message = string.Format(format, args); }
        public FileException(string message, System.Exception innerException) : base(message, innerException) { _Message = message; }

        // custom
        public FileException(string file, string message) : base(message)
        {
            _File = file;
            _Message = message;
        }

        public override string Message
        {
            get => string.Format("路径: {0}{1}异常: {2}", _File, System.Environment.NewLine, _Message);
        }
    }

    class Logger
    {
        public static void LogError(string format, params object[] obj)
        {
            using (var sr = new System.IO.StreamWriter(System.IO.Path.Combine(DNF_Utils.Variables.BaseFolder, "errlog.log"), true))
            {
                sr.WriteLine(string.Format("[{0}]  {1}", System.DateTime.Now.ToString(), string.Format(format, obj)));
            }
        }
    }

    class Shell
    {
        public static void Exec(string file, string args)
        {
            using (var proc = new System.Diagnostics.Process())
            {
                proc.StartInfo.Arguments = args;
                proc.StartInfo.FileName = file;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                proc.WaitForExit(2333);
                proc.Close();
            }
        }
    }

    class Unit
    {
        public static string Byte(ulong bytes, out int lvl)
        {
            var ret = "Bytes";

            if (bytes >= 1099511627776)
            {
                ret = "TB";
                lvl = 4;
            }
            else if (bytes >= 1073741824)
            {
                ret = "GB";
                lvl = 3;
            }
            else if (bytes >= 1048576)
            {
                ret = "MB";
                lvl = 2;
            }
            else if (bytes >= 1024)
            {
                ret = "KB";
                lvl = 1;
            }
            else
            {
                ret = "Bytes";
                lvl = 0;
            }

            return ret;
        }
    }

    class MD5
    {
        public static string Calc(string file)
        {
            try
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                using (var stream = System.IO.File.OpenRead(file))
                {
                    var hash = md5.ComputeHash(stream);
                    return System.BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(); //ToLowerInvariant //ToUpperInvariant
                }
            }
            catch (System.Exception e)
            {
                Logger.LogError("CalcMD5Error [{0}] Exception: {1}", file, e.Message);
            }
            return string.Empty;
        }

        public static bool Match(string file, string MD5)
        {
            return MD5.Equals(Calc(file));
        }
    }

    class Win32Api
    {
        [System.Runtime.InteropServices.DllImport("kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filepath);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retval, int size, string filePath);

        public const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        internal static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        internal static extern System.IntPtr SendMessage(System.IntPtr hWnd, uint Msg, System.IntPtr wParam, System.IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(System.IntPtr hWnd);

        public static bool IniSet(string file, string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, file);
        }

        public static string IniGet(string file, string section, string key)
        {
            var sb = new System.Text.StringBuilder(1024);
            GetPrivateProfileString(section, key, null, sb, 1024, file);
            return sb.ToString();
        }
    }

    class ExWebClient : System.Net.WebClient
    {
        public uint Timeout { get; set; }

        public ExWebClient() : this(10000u) { }

        public ExWebClient(uint timeout)
        {
            Timeout = timeout;
        }

        protected override System.Net.WebRequest GetWebRequest(System.Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = (int)Timeout;
            }
            return request;
        }
    }
}
