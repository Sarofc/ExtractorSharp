using System;

namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x02000004 RID: 4
	public class DirectoryInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023AA File Offset: 0x000005AA
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000023A1 File Offset: 0x000005A1
		public string Name { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000023B2 File Offset: 0x000005B2
		public string Prefix { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000023C3 File Offset: 0x000005C3
		public string Suffix { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x000023D4 File Offset: 0x000005D4
		public override string ToString()
		{
			return this.Name;
		}
	}
}
