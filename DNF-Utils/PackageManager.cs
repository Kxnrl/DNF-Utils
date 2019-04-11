using Kxnrl;
using System;
using System.Collections.Generic;
using System.IO;

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
            var file = Path.GetFileNameWithoutExtension(path.Replace(".theme.", "").Replace(".patch.", ""));

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

        public static string GetNpkType(NpkType npkType, bool prefix = true)
        {
            var pref = prefix ? "Npk_" : "";

            switch (npkType)
            {
                case NpkType.Patch: return pref + "patch";
                case NpkType.Theme: return pref + "theme";
                case NpkType.Sound: return pref + "sound";
            }

            return pref + "unknow";
        }

        public static NpkType GetNpkType(string npkType)
        {
            var type = npkType.Replace("Npk_", "");

            switch (npkType)
            {
                case "patch": return NpkType.Patch;
                case "theme": return NpkType.Theme;
                case "sound": return NpkType.Sound;
            }

            return NpkType.Match;
        }

        public static NpkData GetNpkData(string file)
        {
            var name = Path.GetFileNameWithoutExtension(file).Replace(".theme.", "").Replace(".patch.", "").Replace(".sound.", "");
            var path = Path.Combine(Variables.BaseFolder, "packages.db");

            if (string.IsNullOrEmpty(Win32Api.IniGet(path, name, "Name")))
            {
                return new NpkData("unknow", "unknow", "unknow", "unknow", 0ul, NpkType.Match);
            }

            return new NpkData(
                                 Win32Api.IniGet(path, name, "Name"),
                                 Win32Api.IniGet(path, name, "Desc"),
                                 Win32Api.IniGet(path, name, "Hash"),
                                 Win32Api.IniGet(path, name, "GUID"),
                Convert.ToUInt64(Win32Api.IniGet(path, name, "Size")),
                      GetNpkType(Win32Api.IniGet(path, name, "Type")));
        }

        public static void SaveNpkData(string file, NpkData data)
        {
            var path = Path.Combine(Variables.BaseFolder, "packages.db");
            var name = Path.GetFileNameWithoutExtension(file).Replace(".theme.", "").Replace(".patch.", "").Replace(".sound.", "");

            Win32Api.IniSet(path, name, "Name", data.Name);
            Win32Api.IniSet(path, name, "Desc", data.Desc);
            Win32Api.IniSet(path, name, "Type", GetNpkType(data.Type));
            Win32Api.IniSet(path, name, "Hash", data.Hash);
            Win32Api.IniSet(path, name, "GUID", data.GUID);
            Win32Api.IniSet(path, name, "Size", data.Size.ToString());
        }
    }
}
