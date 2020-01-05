using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExtractorSharp.Core.Lib;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;

namespace ExtractorSharp.Core.Coder
{
	// Token: 0x02000002 RID: 2
	public static class SpkDecoder
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static byte[] Decompress(byte[] bs)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(bs))
			{
				result = SpkDecoder.Decompress(memoryStream);
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		public static void Main(string[] args)
		{
			if (args.Length != 0)
			{
				FileStream fileStream = File.OpenRead(args[0]);
				byte[] bytes = SpkDecoder.Decompress(fileStream);
				fileStream.Close();
				File.WriteAllBytes(args[0].RemoveSuffix(".spk"), bytes);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static byte[] DecompressZip(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream(data);
			data = SpkDecoder.UnZip(memoryStream);
			memoryStream.Close();
			return data;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D8 File Offset: 0x000002D8
		public static byte[] UnZip(MemoryStream ms)
		{
			ZipInputStream zipInputStream = new ZipInputStream(ms);
			bool nextEntry = zipInputStream.GetNextEntry() != null;
			byte[] result = new byte[0];
			if (nextEntry)
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] array = new byte[1024];
				int num;
				do
				{
					num = zipInputStream.Read(array, 0, array.Length);
					memoryStream.Write(array, 0, num);
				}
				while (num > 0);
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			zipInputStream.Close();
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002140 File Offset: 0x00000340
		public static byte[] Decompress(Stream stream)
		{
			stream.Seek(272L);
			byte[] data;
			stream.ReadToEnd(out data);
			byte[][] array = data.Split(SpkDecoder.HEADER);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				for (int i = 1; i < array.Length; i++)
				{
					byte[][] array2 = array[i].Split(SpkDecoder.MARK);
					using (MemoryStream memoryStream2 = new MemoryStream(SpkDecoder.HEADER.Concat(array2[0])))
					{
						BZip2.Decompress(memoryStream2, memoryStream, false);
					}
					if (array2.Length > 1)
					{
						for (int j = 1; j < array2.Length - 1; j++)
						{
							memoryStream.Write(array2[j].Sub(32));
						}
						byte[] array3 = array2.Last<byte[]>();
						int num = array3.LastIndexof(SpkDecoder.TAIL);
						num = ((num < 0) ? (num + array3.Length) : num);
						memoryStream.Write(array3.Sub(32, num));
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000225C File Offset: 0x0000045C
		public static int LastIndexof(this byte[] data, byte[] pattern)
		{
			for (int i = data.Length - 1; i > 0; i--)
			{
				int num = i;
				while (data[num] == pattern[num - i])
				{
					num++;
					if (num - i == pattern.Length)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002296 File Offset: 0x00000496
		public static byte[] Sub(this byte[] array, int start)
		{
			return array.Sub(start, array.Length);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022A4 File Offset: 0x000004A4
		public static byte[] Sub(this byte[] array, int start, int end)
		{
			byte[] array2 = new byte[end - start];
			Buffer.BlockCopy(array, start, array2, 0, end - start);
			return array2;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022C8 File Offset: 0x000004C8
		public static byte[][] Split(this byte[] data, byte[] pattern)
		{
			int i = 0;
			List<byte[]> list = new List<byte[]>();
			int start = 0;
			while (i < data.Length)
			{
				i = data.Indexof(pattern, start);
				i = ((i == -1) ? data.Length : i);
				list.Add(data.Sub(start, i));
				if (i < data.Length)
				{
					i = (start = i + pattern.Length);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002320 File Offset: 0x00000520
		public static int Indexof(this byte[] data, byte[] pattern, int start)
		{
			for (int i = start; i < data.Length; i++)
			{
				int num = i;
				while (num < data.Length && data[num] == pattern[num - i])
				{
					num++;
					if (num - i == pattern.Length)
					{
						return num - pattern.Length;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002362 File Offset: 0x00000562
		// Note: this type is marked as 'beforefieldinit'.
		static SpkDecoder()
		{
			byte[] array = new byte[4];
			array[0] = 1;
			SpkDecoder.TAIL = array;
		}

		// Token: 0x04000001 RID: 1
		private static byte[] HEADER = new byte[]
		{
			66,
			90,
			104,
			57,
			49,
			65,
			89,
			38,
			83,
			89
		};

		// Token: 0x04000002 RID: 2
		private static byte[] MARK = new byte[]
		{
			0,
			0,
			0,
			0,
			0,
			16,
			14,
			0,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			239,
			241,
			byte.MaxValue
		};

		// Token: 0x04000003 RID: 3
		private static byte[] TAIL;
	}
}
