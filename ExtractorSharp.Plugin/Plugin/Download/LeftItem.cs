using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using ExtractorSharp.Core.Composition;

namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x02000005 RID: 5
	[ExportMetadata("Guid", "87844cf0-a062-4f34-8c3b-d8e6bda28daa")]
	[Export(typeof(IMenuItem))]
	internal class LeftItem : IMenuItem
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023E4 File Offset: 0x000005E4
		public string Name
		{
			get
			{
				return "ReplaceFromServer";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023EB File Offset: 0x000005EB
		public string Command
		{
			get
			{
				return "replaceFromServer";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023F2 File Offset: 0x000005F2
		public ClickType Click
		{
			get
			{
				return ClickType.View;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023F5 File Offset: 0x000005F5
		public Image Image { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023FD File Offset: 0x000005FD
		public Keys ShortcutKeys
		{
			get
			{
				return Keys.None;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002400 File Offset: 0x00000600
		public MenuItemType Parent
		{
			get
			{
				return MenuItemType.FILELIST;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002403 File Offset: 0x00000603
		public List<IMenuItem> Children { get; }
	}
}
