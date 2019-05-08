using Kxnrl;
using System;
using System.IO;

namespace DNF_Utils
{
    class Settings
    {
        private static readonly string configFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Kxnrl", "DNF", "dnf.conf");

        public static string lastRunningVersion
        {
            get { return Win32Api.IniGet(configFile, "Settings", "lastRunning"); }
            set { Win32Api.IniSet(configFile, "Settings", "lastRunning", value); }
        }

        public static string lastRunningDate
        {
            get { return Win32Api.IniGet(configFile, "Settings", "lastDate"); }
            set { Win32Api.IniSet(configFile, "Settings", "lastDate", value); }
        }

        public static string lastGamePath
        {
            get { return Win32Api.IniGet(configFile, "Settings", "lastGamePath"); }
            set { Win32Api.IniSet(configFile, "Settings", "lastGamePath", value); }
        }

    }
}
