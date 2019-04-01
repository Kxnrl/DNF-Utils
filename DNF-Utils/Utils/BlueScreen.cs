using Kxnrl;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace DNF_Utils.Utils
{
    class BlueScreen
    {
        const string RegKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\Maintenance";

        public static bool Check()
        {
            try
            {
                using (var reg = Registry.LocalMachine.CreateSubKey(RegKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    foreach (var key in reg.GetValueNames())
                    {
                        if (key.Equals("MaintenanceDisabled"))
                        {
                            if (int.Parse(reg.GetValue("MaintenanceDisabled").ToString()) == 1)
                            {
                                return true;
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError("CheckRegError in [{0}] Exception: {1}", RegKey, e.Message);
            }

            return false;
        }

        public static bool SetFixed(bool enabled = false)
        {
            try
            {
                using (var reg = Registry.LocalMachine.CreateSubKey(RegKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    reg.SetValue("MaintenanceDisabled", enabled ? 0 : 1, RegistryValueKind.DWord);
                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.LogError("SetFixedError in [{0}] Exception: {1}", RegKey, e.Message);
                MessageBox.Show("执行失败!" + Environment.NewLine + e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
    }
}
