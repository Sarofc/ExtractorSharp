using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using ExtractorSharp.Core.Coder;
using ExtractorSharp.Core.Composition;
using ExtractorSharp.Core.Lib;
using ExtractorSharp.Core.Model;

namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x0200000B RID: 11
	[Export(typeof(IFileSupport))]
	[ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
	internal class SpkSupport : IFileSupport
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000037BC File Offset: 0x000019BC
		public string Extension
		{
			get
			{
				return "spk";
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000037C3 File Offset: 0x000019C3
		public void Encode(string file, List<Album> album)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000037C8 File Offset: 0x000019C8
		public List<Album> Decode(string filename)
		{
			FileStream fileStream = File.OpenRead(filename);
			byte[] buffer = SpkDecoder.Decompress(fileStream);
			fileStream.Close();
			List<Album> result;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				result = NpkCoder.ReadNpk(memoryStream, filename.RemoveSuffix(".spk"));
			}
			return result;
		}
	}
}
