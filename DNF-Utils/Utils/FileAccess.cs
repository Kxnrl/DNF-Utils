using Kxnrl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DNF_Utils.Utils
{
    class FileAccess
    {
        public static bool Set(bool deny)
        {
            ForeachDir(Variables.GameFolder, deny);
            return true;
        }

        static void ForeachDir(string dir, bool deny)
        {
            string[] dirs;

            try
            {
                dirs = Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException)
            {
                if (!ACLHelper.DirectoryAccess.GetPermission(dir, FileSystemRights.Read) || 
                    !ACLHelper.DirectoryAccess.GetPermission(dir, FileSystemRights.Write)||
                    !ACLHelper.DirectoryAccess.GetPermission(dir, FileSystemRights.ExecuteFile))
                {
                    ACLHelper.DirectoryAccess.SetPermission(dir, false);
                    ForeachDir(dir, deny);
                }
                return;
            }

            foreach (var d in dirs)
            {
                ForeachDir(d, deny);
            }

            ForeachFile(dir, deny);
        }

        static void ForeachFile(string dir, bool deny)
        {
            foreach (var file in Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    if (!ACLHelper.FileAccess.GetPermission(file, FileSystemRights.Read) ||
                    !ACLHelper.FileAccess.GetPermission(file, FileSystemRights.Write) ||
                    !ACLHelper.FileAccess.GetPermission(file, FileSystemRights.ExecuteFile))
                    {
                        ACLHelper.FileAccess.SetPermission(file, false);
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError("FileAccess.ForeachFile [{0}] Exception: {1}", file, e.Message);
                }
            }
        }
    }
}
