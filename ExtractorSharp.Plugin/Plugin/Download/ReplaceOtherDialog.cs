using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ExtractorSharp.Component;
using ExtractorSharp.Core.Coder;
using ExtractorSharp.Core.Composition;
using ExtractorSharp.Core.Handle;
using ExtractorSharp.Core.Model;
using ExtractorSharp.Json;

namespace ExtractorSharp.Plugin.Download
{
    // Token: 0x02000009 RID: 9
    [ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
    [Export(typeof(ESDialog))]
    public partial class ReplaceOtherDialog : ESDialog
    {
        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000038 RID: 56 RVA: 0x00003189 File Offset: 0x00001389
        // (set) Token: 0x06000037 RID: 55 RVA: 0x00003180 File Offset: 0x00001380
        private Album[] Sources { get; set; }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x0600003A RID: 58 RVA: 0x0000319A File Offset: 0x0000139A
        // (set) Token: 0x06000039 RID: 57 RVA: 0x00003191 File Offset: 0x00001391
        private List<Album> Queues { get; set; } = new List<Album>();

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x0600003B RID: 59 RVA: 0x000031A4 File Offset: 0x000013A4
        private string ServerConfigPath
        {
            get
            {
                string text = Uri.UnescapeDataString(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);
                text = Path.GetDirectoryName(text);
                return string.Format("{0}/conf/server.json", text);
            }
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000031E0 File Offset: 0x000013E0
        [ImportingConstructor]
        public ReplaceOtherDialog(IConnector Connector) : base(Connector)
        {
            this.InitializeComponent();
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
            this.LoadServerList();
            this.yesButton.Click += this.ReplaceFile;
        }

        // Token: 0x0600003D RID: 61 RVA: 0x00003268 File Offset: 0x00001468
        private void ReplaceFile(object sender, EventArgs e)
        {
            Album[] checkedFiles = base.Connector.CheckedFiles;
            SeverInfo severInfo = this.serverList.SelectedItem as SeverInfo;
            this.Count = 0;
            this.Sources = checkedFiles;
            this.Queues.Clear();
            this.downloads.Clear();
            for (int i = 0; i < checkedFiles.Length; i++)
            {
                string text = checkedFiles[i].Path;
                int num = text.LastIndexOf("/");
                if (num >= 0)
                {
                    string text2 = "ImagePacks2";
                    string text3 = "NPK";
                    if (checkedFiles[i].Version == ImgVersion.Other)
                    {
                        text2 = "SoundPacks";
                        text3 = text3.ToLower();
                    }
                    text = text.Substring(0, num);
                    text = text.Replace("/", "_");
                    text = string.Format("{0}/{1}/{2}.{3}", new object[]
                    {
                        severInfo.Host,
                        text2,
                        text,
                        text3
                    });
                    if (!this.downloads.Contains(text))
                    {
                        this.downloads.Add(text);
                    }
                }
            }
            this.progress.Visible = true;
            for (int j = 0; j < this.downloads.Count; j++)
            {
                this.Download(this.downloads[j]);
            }
        }

        // Token: 0x0600003E RID: 62 RVA: 0x000033A8 File Offset: 0x000015A8
        private void Download(string path)
        {
            SeverInfo info = this.serverList.SelectedItem as SeverInfo;
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadDataAsync(new Uri(string.Format("{0}{1}", path, info.Extension)));
                webClient.DownloadProgressChanged += this.ProgressChanged;
                webClient.DownloadDataCompleted += delegate (object o, DownloadDataCompletedEventArgs e)
                {
                    MemoryStream memoryStream = new MemoryStream(this.Decoders[info.Extension](e.Result));
                    List<Album> collection = NpkCoder.ReadNpk(memoryStream, path);
                    this.Queues.AddRange(collection);
                    memoryStream.Close();
                    this.Count++;
                    if (this.Count == this.downloads.Count)
                    {
                        this.progress.Visible = false;
                        this.Completed();
                    }
                };
            }
        }

        // Token: 0x0600003F RID: 63 RVA: 0x0000344C File Offset: 0x0000164C
        private void Completed()
        {
            Album[] sources = this.Sources;
            for (int i = 0; i < sources.Length; i++)
            {
                Album source = sources[i];
                Album album = this.Queues.Find((Album e) => e != null && e.Path.Equals(source.Path));
                if (album != null)
                {
                    base.Connector.Do("replaceImg", new object[]
                    {
                        source,
                        album
                    });
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        // Token: 0x06000040 RID: 64 RVA: 0x000034C2 File Offset: 0x000016C2
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progress.Value = e.ProgressPercentage;
        }

        // Token: 0x06000041 RID: 65 RVA: 0x000034D8 File Offset: 0x000016D8
        private void LoadServerList()
        {
            if (File.Exists(this.ServerConfigPath))
            {
                SeverInfo[] array = new LSBuilder().Read(this.ServerConfigPath).GetValue(typeof(SeverInfo[])) as SeverInfo[];
                ComboBox.ObjectCollection items = this.serverList.Items;
                object[] items2 = array;
                items.AddRange(items2);
                this.serverList.SelectedIndex = 0;
            }
        }

        // Token: 0x0400001C RID: 28
        private List<string> downloads = new List<string>();

        // Token: 0x0400001E RID: 30
        private int Count;

        // Token: 0x0400001F RID: 31
        private Dictionary<string, Decoder> Decoders;
    }
}
