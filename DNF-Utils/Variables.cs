using System;

namespace DNF_Utils
{
    class Variables
    {
        public static string BaseFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Kxnrl", "DNF");
        public static string GameFolder = @"D:\地下城与勇士";

        public static bool IsMetroWindows = false;

        public enum PatchType
        {
            Character,
            Optimization,
            Miscellaneous,
            Theme
        }

        public struct VersionInfo
        {
            public string Date;
            public string Commit;
            public string Version;
            public string Description;
            public string Author;
            public string Website;
            public string UpdateURL;
        }

        public static readonly VersionInfo Version = new VersionInfo()
        {
            Date = "20190402",
            Commit = "4",
            Version = "1.0.190402",
            Description = "DNF实用工具集",
            Author = "Kyle \"Kxnrl\" Frankiss",
            Website = "https://www.kxnrl.com",
            UpdateURL = "https://dnf.kxnrl.com/app/DNF-Utils.exe"
        };

        public static PatchType PatchMode = PatchType.Character;
    }
}
