using Kxnrl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DNF_Utils.Utils
{
    class NPKHelper
    {
        public enum FileMode
        {
            FM_UNKNOW,
            FM_NPK,
            FM_IMG,
            FM_IMAGE,
        }

        const string NPK_HEADER   = "NeoplePack_Bill";
        const string IMG_HEADER   = "Neople Img File";
        const string IMAGE_HEADER = "Neople Image Fi"; // le";

        public static byte[] header = Encoding.ASCII.GetBytes("puchikon@neople dungeon and fighter DNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNF\0");

        public static List<string> ReadNPK(string file, out FileMode flag)
        {
            if (!File.Exists(file))
            {
                // ??
                throw new ArgumentException("文件不存在", "file");
            }

            flag = FileMode.FM_UNKNOW;

            using (var fs = File.OpenRead(file))
            {
                var buffer = new byte[256];

                using (var br = new BinaryReader(fs))
                {
                    br.Read(buffer, 0, 16);

                    var magic = Encoding.ASCII.GetString(buffer).Trim('\0');

                    if (NPK_HEADER.Equals(magic))
                    {
                        flag = FileMode.FM_NPK;
                    }
                    else if (IMG_HEADER.Equals(magic))
                    {
                        flag = FileMode.FM_IMG;
                        fs.Seek(-15, SeekOrigin.Current);
                    }
                    else if (IMAGE_HEADER.Equals(magic))
                    {
                        flag = FileMode.FM_IMAGE;
                        fs.Seek(-15, SeekOrigin.Current);
                    }
                    else
                    {
                        throw new Exception("Wrong Npk header flag: " + magic);
                    }

                    var nCount = br.ReadInt32();
                    var files = new List<string>(nCount);
                    var ctrfs = false;

                    for (int i = 0; i < nCount; i++)
                    {
                        var offset = br.ReadInt32();
                        var size_t = br.ReadInt32();
                        br.Read(buffer, 0, 256);

                        if (ctrfs || (buffer[254] == 70 && buffer[253] == 78 && buffer[252] == 68))
                        {
                            ctrfs = true;
                            EncodeName(ref buffer);
                        }

                        var img = Encoding.Default.GetString(buffer).Trim('\0');

                        //if (!img.EndsWith(".img"))
                        //    continue;

                        // index = i;
                        // ioffs = offset;

                        files.Add(img);
                    }

                    return files;
                }
            }
        }

        static void EncodeName(ref byte[] img)
        {
            var iCount = img.Length < header.Length ? img.Length : header.Length;
            var buffer = new byte[256];
            var i = 0;
            for (; i < iCount; i++)
            {
                buffer[i] = (byte)(header[i] ^ img[i]);
            }
            for (; i < 256; i++)
            {
                buffer[i] = (byte)(header[i] ^ 0);
            }
            img = buffer;
        }
    }

    class NPKScanner
    {

        const string pref_theme = ".1.theme.";
        const string pref_patch = ".2.patch.";

        public static void Scan()
        {
            PackageManager.DictIMG.Clear();
            PackageManager.ListIMG.Clear();

            // scan theme
            Scan(pref_patch);
            Scan(pref_theme);
        }

        private static void Scan(string prefix)
        {
            foreach (var file in Directory.GetFiles(Path.Combine(Variables.GameFolder, "ImagePacks2"), prefix + "*.NPK", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var npk = NPKHelper.ReadNPK(file, out NPKHelper.FileMode flag);

                    if (flag != NPKHelper.FileMode.FM_NPK)
                    {
                        // IMG/IMAGE
                        continue;
                    }

                    PackageManager.AppendNPK(npk, file);
                }
                catch (Exception e)
                {
                    Logger.LogError("ScanNPK [{0}] Exception: {1}", file, e.Message);
                }
            }
        }
    }

}
