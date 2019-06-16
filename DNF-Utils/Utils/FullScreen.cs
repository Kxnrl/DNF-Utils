using Kxnrl;
using System;
using System.IO;

namespace DNF_Utils.Utils
{
    class FullScreen
    {
        private static string configFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "DNF", "DNF.cfg");

        public static bool Check()
        {
            if (!File.Exists(configFile))
            {
                // ??
                return false;
            }

            var mode = Win32Api.IniGet(configFile, "DNF COMMON", "CONFIG_COMMON_SCREEN_MODE");

            if (!int.TryParse(mode, out int value))
            {
                // need to fix
                return true;
            }

            if (value == 2)
            {
                // no need fix
                return false;
            }

            // need it
            return true;
        }

        public static bool Fix()
        {
            if (ProcKiller.KillAll(new string[] { "DNF", "GameLoader" }))
            {
                // kill?
                return false;
            }

            Win32Api.IniSet(configFile, "DNF COMMON", "CONFIG_COMMON_SCREEN_MODE", "2");
            Win32Api.IniSet(configFile, "DNF COMMON", "CONFIG_TEXTURE_POST_PROCESSING", "0");
            Win32Api.IniSet(configFile, "DNF COMMON", "CONFIG_COMMON_RESIZE_WIDTH", "800");
            Win32Api.IniSet(configFile, "DNF COMMON", "CONFIG_COMMON_RESIZE_HEIGHT", "600");

            return true;
        }
    }
}
