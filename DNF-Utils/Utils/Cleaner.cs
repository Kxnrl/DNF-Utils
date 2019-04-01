using Kxnrl;
using System;
using System.Collections.Generic;
using System.IO;

namespace DNF_Utils.Utils
{
    class Cleaner
    {
        static readonly List<string> Folders = new List<string>()
        {
            "tgppatches",
            "components",
            "TGuard",
            "TP_Temp"
        };

        static List<string> Files = new List<string>();

        public static bool Check()
        {
            Files.Clear();

            try
            {
                foreach (var dir in Folders)
                {
                    var path = Path.Combine(Variables.GameFolder, dir);

                    if (Directory.Exists(path))
                    {
                        foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                        {
                            Files.Add(file);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("CleanerError [{0}] Exception: {1}", "Check", e.Message);
            }

            return Files.Count > 0;
        }

        public static long DeleteAll()
        {
            long totalBytes = 0;

            foreach (var file in Files)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        totalBytes += new FileInfo(file).Length;
                        File.Delete(file);
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError("CleanerError DeleteAll [{0}] Exception: {1}", file, e.Message);
                }
            }

            foreach (var dir in Folders)
            {
                try
                {
                    if (Directory.Exists(dir))
                    {
                        Directory.Delete(dir, true);
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError("CleanerError DeleteAll [{0}] Exception: {1}", dir, e.Message);
                }
            }

            return totalBytes;
        }
    }
}
