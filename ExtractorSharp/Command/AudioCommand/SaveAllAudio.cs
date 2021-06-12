using System;
using System.IO;
using ExtractorSharp.Core.Command;
using ExtractorSharp.Core.Composition;
using ExtractorSharp.Core.Model;

namespace ExtractorSharp.Command.ImageCommand
{
    /// <summary>
    ///     保存所有ogg
    /// </summary>
    internal class SaveAllAudio : IMutipleAciton, ICommandMessage
    {
        private Album[] Albums { set; get; }
        private int Digit { set; get; }
        private bool FullPath { set; get; }
        private int Increment { set; get; }

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
                var dir = $"{Path}/{(FullPath ? album.Path : album.Name)}";
                dir = dir.Replace('\\', '/');

                //if (File.Exists(dir))
                //{
                //    dir += "_";
                //}

                var folder = System.IO.Path.GetDirectoryName(dir);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = $"{dir}"; //文件名格式:文件路径/xxx.ogg
                File.WriteAllBytes(path, album.Data);
            }
        }

        public bool CanUndo => false;

        public bool IsChanged => false;

        public string Name => "SaveAllAudio";

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