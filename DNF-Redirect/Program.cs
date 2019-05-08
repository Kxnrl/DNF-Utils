using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace DNF_Redirect
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Kxnrl", "DNF", "launched.log");

            var parent = ParentProcessUtilities.GetParentProcess();
            var p_name = parent == null ? "unknow" : parent.ProcessName + ".exe";

            using (var sr = new System.IO.StreamWriter(path, true, Encoding.UTF8, 1024))
            {
                var sb = new StringBuilder(1024);
                for (int index = 0; index < args.Length; ++index)
                {
                    if (index > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(args[index]);
                }

                sr.WriteLine(string.Format("[{0}]  Launched {1} by [{2}]", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), sb.ToString(), p_name));
            }
        }

        //https://stackoverflow.com/questions/394816/how-to-get-parent-process-in-net-in-managed-way
        [StructLayout(LayoutKind.Sequential)]
        public struct ParentProcessUtilities
        {
            internal IntPtr Reserved1;
            internal IntPtr PebBaseAddress;
            internal IntPtr Reserved2_0;
            internal IntPtr Reserved2_1;
            internal IntPtr UniqueProcessId;
            internal IntPtr InheritedFromUniqueProcessId;

            [DllImport("ntdll.dll")]
            static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);

            public static Process GetParentProcess()
            {
                return GetParentProcess(Process.GetCurrentProcess().Handle);
            }

            //public static Process GetParentProcess(int id)
            //{
            //    Process process = Process.GetProcessById(id);
            //    return GetParentProcess(process.Handle);
            //}

            static Process GetParentProcess(IntPtr handle)
            {
                ParentProcessUtilities pbi = new ParentProcessUtilities();
                int returnLength;
                int status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out returnLength);
                if (status != 0)
                    throw new Win32Exception(status);

                try
                {
                    return Process.GetProcessById(pbi.InheritedFromUniqueProcessId.ToInt32());
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
        }
    }
}
