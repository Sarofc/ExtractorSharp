using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ExtractorSharp.Component;
using ExtractorSharp.Core.Coder;
using ExtractorSharp.Core.Composition;
using ExtractorSharp.Core.Lib;
using ExtractorSharp.Core.Model;
using ExtractorSharp.Json;

namespace ExtractorSharp.Plugin.Download
{
    // Token: 0x02000006 RID: 6
    [ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
    [Export(typeof(ESDialog))]
    public partial class MainDialog : ESDialog
    {
        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000020 RID: 32 RVA: 0x0000240C File Offset: 0x0000060C
        private string ServerConfigPath
        {
            get
            {
                string text = Uri.UnescapeDataString(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);
                text = Path.GetDirectoryName(text);
                return string.Format("{0}/conf/server.json", text);
            }
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00002448 File Offset: 0x00000648
        [ImportingConstructor]
        public MainDialog(IConnector Connector) : base(Connector)
        {
            this.InitializeComponent();
            this.List = new List<string>();
            this.Task = new List<MainDialog.DownloadInfo>();
            this.Decoders = new Dictionary<string, Decoder>
            {
                {
                    ".spk",
                    new Decoder(SpkDecoder.Decompress)
                },
                {
                    ".tct",
                    new Decoder(SpkDecoder.DecompressZip)
                }
            };
            this.keywordBox.TextChanged += delegate (object o, EventArgs e)
            {
                this.ListFlush();
            };
            this.serverList.SelectedIndexChanged += this.Search;
            this.directoryList.SelectedIndexChanged += this.Search;
            this.addListItem.Click += this.Download;
            this.saveAsItem.Click += this.SaveAs;
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002584 File Offset: 0x00000784
        private void LoadServerList()
        {
            try
            {
                if (this.serverList.Items.Count == 0 && File.Exists(this.ServerConfigPath))
                {
                    SeverInfo[] array = new LSBuilder().Read(this.ServerConfigPath).GetValue(typeof(SeverInfo[])) as SeverInfo[];
                    this.serverList.Items.Clear();
                    ComboBox.ObjectCollection items = this.serverList.Items;
                    object[] items2 = array;
                    items.AddRange(items2);
                    this.serverList.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                base.Connector.SendError("LoadServerError");
            }
        }

        // Token: 0x06000023 RID: 35 RVA: 0x0000262C File Offset: 0x0000082C
        private void ListFlush()
        {
            CheckedListBox.CheckedItemCollection checkedItems = this.fileList.CheckedItems;
            int num = this.fileList.SelectedIndex;
            string[] array = new string[checkedItems.Count];
            checkedItems.CopyTo(array, 0);
            this.fileList.Items.Clear();
            string[] args = this.keywordBox.Text.Trim().Split(" ");
            List<string> list = this.List.FindAll((string e) => args.Length == 0 || args.All((string arg) => e.ToLower().Contains(arg.ToLower())));
            ListBox.ObjectCollection items = this.fileList.Items;
            object[] items2 = list.ToArray();
            items.AddRange(items2);
            for (int i = 0; i < list.Count; i++)
            {
                if (array.Contains(list[i]))
                {
                    this.fileList.SetItemChecked(i, true);
                }
            }
            if (this.fileList.Items.Count > 0)
            {
                if (num < 1 || num > this.fileList.Items.Count - 1)
                {
                    num = Math.Min(num, this.fileList.Items.Count - 1);
                    num = Math.Max(num, 0);
                }
                this.fileList.SelectedIndex = num;
            }
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00002757 File Offset: 0x00000957
        private void Download(object sender, EventArgs e)
        {
            this.Download(delegate ()
            {
                List<Album> list = new List<Album>();
                foreach (MainDialog.DownloadInfo downloadInfo in this.Task)
                {
                    using (MemoryStream memoryStream = new MemoryStream(downloadInfo.Data))
                    {
                        list.AddRange(NpkCoder.ReadNpk(memoryStream, downloadInfo.Name));
                    }
                }
                base.Connector.Do("addImg", new object[]
                {
                    list.ToArray(),
                    false
                });
            });
        }

        // Token: 0x06000025 RID: 37 RVA: 0x0000276C File Offset: 0x0000096C
        private void SaveAs(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            this.Download(delegate ()
            {
                foreach (MainDialog.DownloadInfo downloadInfo in this.Task)
                {
                    using (FileStream fileStream = new FileStream(string.Format("{0}/{1}", dialog.SelectedPath, downloadInfo.Name), FileMode.Create))
                    {
                        fileStream.Write(downloadInfo.Data);
                    }
                }
            });
        }

        // Token: 0x06000026 RID: 38 RVA: 0x000027B4 File Offset: 0x000009B4
        private void Download(Action action)
        {
            string[] temp = this.fileList.SelectItems;
            SeverInfo info = this.serverList.SelectedItem as SeverInfo;
            DirectoryInfo directoryInfo = this.directoryList.SelectedItem as DirectoryInfo;
            this.Count = 0;
            try
            {
                int j;
                int i;
                for (i = 0; i < temp.Length; i = j + 1)
                {
                    if (this.Task.Find((MainDialog.DownloadInfo e) => e.Name.Equals(temp[i])) == null)
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            string uriString = string.Format("{0}/{1}/{2}{3}", new object[]
                            {
                                info.Host,
                                directoryInfo.Name,
                                temp[i],
                                info.Extension
                            });
                            string name = temp[i];
                            MainDialog.DownloadInfo down = new MainDialog.DownloadInfo
                            {
                                Name = name
                            };
                            this.Task.Add(down);
                            this.Count++;
                            webClient.DownloadDataAsync(new Uri(uriString));
                            webClient.DownloadProgressChanged += delegate (object o, DownloadProgressChangedEventArgs e)
                            {
                                this.bar.Value = e.ProgressPercentage;
                                down.State = e.ProgressPercentage;
                            };
                            webClient.DownloadDataCompleted += delegate (object o, DownloadDataCompletedEventArgs ev)
                            {
                                down.Data = this.Decoders[info.Extension](ev.Result);
                                this.Count--;
                                if (this.Count == 0)
                                {
                                    this.bar.Value = 0;
                                    Action action2 = action;
                                    if (action2 == null)
                                    {
                                        return;
                                    }
                                    action2();
                                }
                            };
                        }
                    }
                    j = i;
                }
            }
            catch (Exception)
            {
                base.Connector.SendError("NetError");
            }
        }

        // Token: 0x06000027 RID: 39 RVA: 0x000029D4 File Offset: 0x00000BD4
        private void Search(object sender, EventArgs e)
        {
            this.List.Clear();
            this.Task.Clear();
            this.fileList.Items.Clear();
            DirectoryInfo directory;
            SeverInfo severInfo;
            if ((severInfo = (this.serverList.SelectedItem as SeverInfo)) == null || (directory = (this.directoryList.SelectedItem as DirectoryInfo)) == null)
            {
                return;
            }
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    Uri address = new Uri(string.Format("{0}/{1}", severInfo.Host, severInfo.Index));
                    webClient.DownloadStringAsync(address);
                    webClient.DownloadProgressChanged += delegate (object o, DownloadProgressChangedEventArgs ex)
                    {
                        this.bar.Value = ex.ProgressPercentage;
                    };
                    webClient.DownloadStringCompleted += delegate (object o, DownloadStringCompletedEventArgs ex)
                    {
                        this.bar.Value = 0;
                        string result = ex.Result;
                        this.List = this.ParseLst(result, directory.Prefix, directory.Suffix);
                        this.ListFlush();
                    };
                }
            }
            catch (Exception)
            {
                base.Connector.SendError("NetError");
            }
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00002AD4 File Offset: 0x00000CD4
        private List<string> ParseLst(string text, string prefix, string suffix)
        {
            List<string> list = new List<string>();
            int i = 0;
            while (i < text.Length)
            {
                int num = text.IndexOf(prefix, i, StringComparison.Ordinal);
                int num2 = text.IndexOf(suffix, num + 1, StringComparison.Ordinal);
                if (num <= 0 || num2 <= 0)
                {
                    break;
                }
                i = num2 + suffix.Length;
                string item = text.Substring(num, i - num);
                list.Add(item);
            }
            return list;
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00002B30 File Offset: 0x00000D30
        public override DialogResult Show(params object[] args)
        {
            this.LoadServerList();
            return base.Show(args);
        }

        // Token: 0x04000009 RID: 9
        private List<string> List;

        // Token: 0x0400000A RID: 10
        private List<MainDialog.DownloadInfo> Task;

        // Token: 0x0400000B RID: 11
        private int Count;

        // Token: 0x0400000D RID: 13
        private Dictionary<string, Decoder> Decoders;

        // Token: 0x0200000D RID: 13
        private class DownloadInfo
        {
            // Token: 0x17000017 RID: 23
            // (get) Token: 0x0600004B RID: 75 RVA: 0x00003829 File Offset: 0x00001A29
            // (set) Token: 0x0600004A RID: 74 RVA: 0x00003820 File Offset: 0x00001A20
            public string Name { get; set; }

            // Token: 0x17000018 RID: 24
            // (get) Token: 0x0600004D RID: 77 RVA: 0x0000383A File Offset: 0x00001A3A
            // (set) Token: 0x0600004C RID: 76 RVA: 0x00003831 File Offset: 0x00001A31
            public int State { get; set; }

            // Token: 0x17000019 RID: 25
            // (get) Token: 0x0600004F RID: 79 RVA: 0x0000384B File Offset: 0x00001A4B
            // (set) Token: 0x0600004E RID: 78 RVA: 0x00003842 File Offset: 0x00001A42
            public byte[] Data { get; set; }

            // Token: 0x06000050 RID: 80 RVA: 0x00003854 File Offset: 0x00001A54
            public override string ToString()
            {
                string arg = (this.State > 100) ? Language.Default["Success"] : string.Format("{0}%", this.State);
                return string.Format("{0},{1}:{2}", this.Name, Language.Default["State"], arg);
            }
        }
    }
}
