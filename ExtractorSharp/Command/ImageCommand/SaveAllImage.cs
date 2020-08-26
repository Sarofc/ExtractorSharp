using System;
using System.IO;
using ExtractorSharp.Core.Command;
using ExtractorSharp.Core.Composition;
using ExtractorSharp.Core.Model;

namespace ExtractorSharp.Command.ImageCommand
{
    /// <summary>
    ///     保存贴图
    ///     不可撤销
    ///     可宏命令
    /// </summary>
    internal class SaveAllImage : IMutipleAciton, ICommandMessage
    {
        private Album[] Albums { set; get; }
        private int Digit { set; get; }
        private bool FullPath { set; get; }
        private int Increment { set; get; }

        private SpriteEffect OnSaving { set; get; }

        private string Path { set; get; }

        private string Prefix { set; get; } = string.Empty;

        public void Do(params object[] args)
        {
            Albums = args[0] as Album[];
            Path = args[1] as string;
            if (args.Length > 2)
            {
                Prefix = (args[2] as string)?.Replace("\\", "/");
                Increment = (int)args[3];
                Digit = (int)args[4];
                FullPath = (bool)args[5];
                OnSaving = args[6] as SpriteEffect;
            }
            Action(Albums);
        }

        public void Redo()
        {
            // Method intentionally left empty.
        }

        public void Undo()
        {
            // Method intentionally left empty.
        }

        public void Action(params Album[] albums)
        {
            foreach (var album in albums)
            {
                //是否加入文件的路径
                var dir = $"{Path}/{(FullPath ? album.Path : album.Name)}/{Prefix}";
                dir = dir.Replace('\\', '/');
                var index = dir.LastIndexOf("/");
                dir = dir.Substring(0, index + 1);
                var prefix = dir.Substring(index);
                if (File.Exists(dir))
                {
                    dir += "_";
                }
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var positionList = new System.Collections.Generic.List<OffsetData>(album.List.Count);
                for (int i = 0; i < album.List.Count; i++)
                {
                    var entity = album.List[i];
                    var name = (Increment == -1 ? i : Increment + i).ToString();
                    while (name.Length < Digit)
                    {
                        name = string.Concat("0", name);
                    }
                    var path = $"{dir}{prefix}{name}.png"; //文件名格式:文件路径/贴图索引.png
                    var image = entity.Picture;
                    if (OnSaving != null)
                    {
                        foreach (SpriteEffect action in OnSaving.GetInvocationList())
                        {
                            action.Invoke(entity, ref image);
                            image = image ?? entity.Picture;
                        }
                    }
                    var parent = System.IO.Path.GetDirectoryName(path);
                    image.Save(path); //保存贴图

                    positionList.Add(new OffsetData(entity.Location));
                }

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(positionList);
                var jsonPath = $"{dir}{prefix}info.json";
                File.WriteAllText(jsonPath, json);
            }
        }

        public bool CanUndo => false;

        public bool IsChanged => false;

        public string Name => "SaveAllImage";

        [System.Serializable]
        public struct OffsetData
        {
            public int x;
            public int y;

            public OffsetData(System.Drawing.Point point)
            {
                x = point.X;
                y = point.Y;
            }
        }
    }
}