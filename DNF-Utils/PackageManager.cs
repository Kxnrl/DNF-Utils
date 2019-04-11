using Kxnrl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DNF_Utils
{
    class PackageManager
    {
        public enum NpkType
        {
            Match = 0,
            Patch = 1,
            Theme = 2,
            Sound = 3,
        }

        public class NpkData
        {
            public string  Name;
            public string  Desc;
            public string  Hash;
            public string  GUID;
            public ulong   Size;
            public NpkType Type;

            public NpkData(string name, string desc, string hash, string guid, ulong size, NpkType type)
            {
                Name = name;
                Desc = desc;
                Hash = hash;
                GUID = guid;
                Size = size;
                Type = type;
            }
        }

        public static List<string> ListIMG = new List<string>();
        public static Dictionary<string, string> DictIMG = new Dictionary<string, string>();

        public static void Reset()
        {
            ListIMG.Clear();
            DictIMG.Clear();
        }

        public static void AppendNPK(List<string> npk, string path)
        {
            var file = GetNpkGUIDName(path);

            foreach (var img in npk)
            {
                ListIMG.Add(img);

                if (DictIMG.ContainsKey(img))
                {
                    if (DictIMG[img].Contains(file))
                    {
                        continue;
                    }

                    DictIMG[img] += '|' + file;
                }
                else
                {
                    DictIMG.Add(img, file);
                }
            }
        }

        public static void EraseNPK(List<string> npk, string path)
        {
            var file = GetNpkGUIDName(path);

            foreach (var img in npk)
            {
                if (DictIMG.ContainsKey(img))
                {
                    var split = DictIMG[img].Split('|');
                    if (split.Length > 0)
                    {
                        var replace = string.Empty;
                        foreach (var text in split)
                        {
                            if (text.Contains(file))
                            {
                                continue;
                            }
                            replace += string.IsNullOrEmpty(replace) ? text : "|" + text;
                        }
                        DictIMG[img] = replace;

                        if (DictIMG[img].Length >= 36)
                        {
                            // block
                            return;
                        }
                    }

                    DictIMG.Remove(img);

                    if (ListIMG.Contains(img))
                    {
                        ListIMG.Remove(img);
                    }
                }
            }
        }

        public static string GetNpkType(NpkType npkType, bool prefix = true, bool schinese = false)
        {
            var pref = prefix ? "Npk_" : "";

            switch (npkType)
            {
                case NpkType.Patch: return pref + (!schinese ? "Patch" : "增强补丁");
                case NpkType.Theme: return pref + (!schinese ? "Theme" : "主题界面");
                case NpkType.Sound: return pref + (!schinese ? "Sound" : "魔改音效");
            }

            return pref + (!schinese ? "Unknow" : "未知文件");
        }

        public static NpkType GetNpkType(string npkType)
        {
            var type = npkType.Replace("Npk_", "");

            switch (type)
            {
                case "Patch": return NpkType.Patch;
                case "Theme": return NpkType.Theme;
                case "Sound": return NpkType.Sound;
            }

            return NpkType.Match;
        }

        public static NpkData GetNpkData(string file)
        {
            var name = GetNpkGUIDName(file);
            var path = Path.Combine(Variables.BaseFolder, "packages", name + ".db");
            var dpkg = "Package";

            if (string.IsNullOrEmpty(Win32Api.IniGet(path, dpkg, "Name")))
            {
                return new NpkData("unknow", "unknow", "unknow", "unknow", 0ul, NpkType.Match);
            }

            return new NpkData(
                                 Win32Api.IniGet(path, dpkg, "Name"),
                                 Win32Api.IniGet(path, dpkg, "Desc"),
                                 Win32Api.IniGet(path, dpkg, "Hash"),
                                 Win32Api.IniGet(path, dpkg, "GUID"),
                Convert.ToUInt64(Win32Api.IniGet(path, dpkg, "Size")),
                      GetNpkType(Win32Api.IniGet(path, dpkg, "Type")));
        }

        public static void SaveNpkData(string file, NpkData data)
        {
            var name = GetNpkGUIDName(file);
            var path = Path.Combine(Variables.BaseFolder, "packages", name + ".db");
            var dpkg = "Package";

            Win32Api.IniSet(path, dpkg, "Name", data.Name);
            Win32Api.IniSet(path, dpkg, "Desc", data.Desc);
            Win32Api.IniSet(path, dpkg, "Type", GetNpkType(data.Type));
            Win32Api.IniSet(path, dpkg, "Hash", data.Hash);
            Win32Api.IniSet(path, dpkg, "GUID", data.GUID);
            Win32Api.IniSet(path, dpkg, "Size", data.Size.ToString());
        }

        private static string GetNpkGUIDName(string name)
        {
            const string pattern = @"{?\w{8}-?\w{4}-?\w{4}-?\w{4}-?\w{12}}?";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var result = regex.Match(name);
            return result.Success ? result.Value : name;
        }
    }
}
