using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DNF_Utils.Utils
{
    class ProcKiller
    {
        public static bool KillAll(string[] procs)
        {
            var list = new List<Process>();
            foreach (var exe in procs)
            {
                foreach (var p in Process.GetProcessesByName(exe))
                {
                    list.Add(p);
                }
            }

            if (list.Count > 0)
            {
                var sb = new StringBuilder("侦测到以下程序", 10240);
                sb.AppendLine("点击[是]将会强制关闭所有进程,");
                sb.AppendLine("点击[否]将会终止当前操作任务.");
                sb.AppendLine("===========================");

                foreach (var p in list)
                {
                    sb.AppendLine((p.ProcessName + ".exe").PadRight(16)  + " " + "[" + p.Id + "]");
                }

                if (MessageBox.Show(sb.ToString(), "侦测到目标进程正在运行", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return false;
                }

                foreach (var p in list)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("结束[" + p.ProcessName + "]失败." + Environment.NewLine + "异常: " + e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
