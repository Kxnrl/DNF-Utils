using Kxnrl;
using System;
using System.IO;
using System.Windows.Forms;

namespace DNF_Utils.Utils
{
    class BlackScreen
    {
        private static string configDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "DNF");

        public static bool Check()
        {
            if (!Directory.Exists(configDir))
            {
                // ??
                return false;
            }

            try
            {
                return Directory.GetFiles(configDir).Length > 0;
            }
            catch (Exception e)
            {
                Logger.LogError("BlackScreen.Check Exception: {0}", e.Message);
            }

            // err it
            return false;
        }

        public static bool Fix()
        {
            if (ProcKiller.KillAll(new string[] { "DNF", "GameLoader" }))
            {
                // kill?
                return false;
            }

            try
            {
                foreach (var file in Directory.GetFiles(configDir))
                {
                    File.Delete(file);
                }
            }
            catch (Exception e)
            {
                Logger.LogError("BlackScreen.Fix Exception: {0}", e.Message);
                MessageBox.Show("发生异常错误: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
