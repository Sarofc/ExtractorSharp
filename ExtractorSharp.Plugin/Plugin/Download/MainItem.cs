using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using ExtractorSharp.Core.Composition;

namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x02000007 RID: 7
	[ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
	[Export(typeof(IMenuItem))]
	internal class MainItem : IMenuItem
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003164 File Offset: 0x00001364
		public string Name
		{
			get
			{
				return "FileDownload";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000316B File Offset: 0x0000136B
		public string Command
		{
			get
			{
				return "fileDownload";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003172 File Offset: 0x00001372
		public MenuItemType Parent
		{
			get
			{
				return MenuItemType.MODEL;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003175 File Offset: 0x00001375
		public List<IMenuItem> Children { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000023F2 File Offset: 0x000005F2
		public ClickType Click
		{
			get
			{
				return ClickType.View;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000317D File Offset: 0x0000137D
		public Image Image
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000023FD File Offset: 0x000005FD
		public Keys ShortcutKeys
		{
			get
			{
				return Keys.None;
			}
		}
	}
}
