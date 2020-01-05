using System;

namespace ExtractorSharp.Plugin.Download
{
	// Token: 0x0200000A RID: 10
	public class SeverInfo
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000037B4 File Offset: 0x000019B4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000025 RID: 37
		public string Name;

		// Token: 0x04000026 RID: 38
		public string Host;

		// Token: 0x04000027 RID: 39
		public string Extension;

		// Token: 0x04000028 RID: 40
		public string Index;
	}
}
