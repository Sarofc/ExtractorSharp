using System;
using System.ComponentModel.Composition;
using ExtractorSharp.Core.Composition;

namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x02000008 RID: 8
	[ExportMetadata("Name", "ExtrctorSharp Downloader")]
	[ExportMetadata("Version", "1.7.2.0")]
	[ExportMetadata("Since", "1.7.2.0")]
	[ExportMetadata("Description", "Downloader")]
	[ExportMetadata("Author", "Kritsu")]
	[ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
	[Export(typeof(IPlugin))]
	internal class MainPlugin : IPlugin
	{
	}
}
