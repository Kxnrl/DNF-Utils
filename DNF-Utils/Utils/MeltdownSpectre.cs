using Kxnrl;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace DNF_Utils.Utils
{
    class MeltdownSpectre
    {
        const string RegKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management";

        public static bool Check()
        {
            try
            {
                using (var reg = Registry.LocalMachine.CreateSubKey(RegKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    var a = reg.GetValue("FeatureSettingsOverride")?.ToString();
                    var b = reg.GetValue("FeatureSettingsOverrideMask")?.ToString();

                    if (a == null || b == null)
                    {
                        return false;
                    }

                    if (!int.TryParse(a, out int c) || !int.TryParse(b, out int d) || c != 3 || d != 3)
                    {
                        return false;
                    }

                    return true;
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
                    if (enabled)
                    {
                        reg.SetValue("FeatureSettingsOverride", 0, RegistryValueKind.DWord);
                        reg.SetValue("FeatureSettingsOverrideMask", 3, RegistryValueKind.DWord);
                    }
                    else
                    {
                        reg.SetValue("FeatureSettingsOverride", 3, RegistryValueKind.DWord);
                        reg.SetValue("FeatureSettingsOverrideMask", 3, RegistryValueKind.DWord);
                    }

                    reg.Flush();
                    reg.Close();

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
