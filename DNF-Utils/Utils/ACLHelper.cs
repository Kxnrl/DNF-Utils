using Kxnrl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DNF_Utils.Utils
{
    class ACLHelper
    {
        //https://stackoverflow.com/questions/2978612/how-to-check-if-a-windows-file-is-readable-writable

        private static readonly FileSystemAccessRule ACL_Deny  = new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Deny);
        private static readonly FileSystemAccessRule ACL_Allow = new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow);
        private readonly static WindowsIdentity _identity = WindowsIdentity.GetCurrent();

        public class DirectoryAccess
        {
            public static bool GetPermission(string path, FileSystemRights right)
            {
                try
                {
                    var ds = Directory.GetAccessControl(path);

                    foreach (FileSystemAccessRule fsar in ds.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                    {
                        if (fsar.IdentityReference == _identity.User && fsar.FileSystemRights.HasFlag(right) && fsar.AccessControlType == AccessControlType.Allow)
                        {
                            return true;
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
                catch (UnauthorizedAccessException)
                {
                    return false;
                }
                catch (Exception e)
                {
                    throw new FileException(path, e.Message);
                }

                return false;
            }

            public static void SetPermission(string path, bool deny)
            {
                var ds = Directory.GetAccessControl(path);

                if (deny)
                {
                    ds.RemoveAccessRule(ACL_Allow);
                    ds.AddAccessRule(ACL_Deny);
                }
                else
                {
                    ds.RemoveAccessRule(ACL_Deny);
                    ds.AddAccessRule(ACL_Allow);
                }

                Directory.SetAccessControl(path, ds);
            }
        }

        public class FileAccess
        {
            public static bool GetPermission(string path, FileSystemRights right)
            {
                try
                {
                    var fs = File.GetAccessControl(path);

                    foreach (FileSystemAccessRule fsar in fs.GetAccessRules(true, true, typeof(SecurityIdentifier)))
                    {
                        if (fsar.IdentityReference == _identity.User && fsar.FileSystemRights.HasFlag(right) && fsar.AccessControlType == AccessControlType.Allow)
                        {
                            return true;
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
                catch (UnauthorizedAccessException)
                {
                    return false;
                }
                catch (Exception e)
                {
                    throw new FileException(path, e.Message);
                }

                return false;
            }


            public static void SetPermission(string path, bool deny)
            {
                var fs = File.GetAccessControl(path);

                if (deny)
                {
                    fs.RemoveAccessRule(ACL_Allow);
                    fs.AddAccessRule(ACL_Deny);
                }
                else
                {
                    fs.RemoveAccessRule(ACL_Deny);
                    fs.AddAccessRule(ACL_Allow);
                }

                File.SetAccessControl(path, fs);
            }
        }
    }
}
