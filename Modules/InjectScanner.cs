using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using xNet;
using System.Net;
using System.Windows.Forms;

namespace ArcNet.Modules
{
    class InjectScanner
    {
		public static string resFolder = @"Results\VulnScanner" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
		public static string filePath;
		public static void MainMenu() {
			try
			{
				Directory.CreateDirectory(resFolder);
				ServicePointManager.DefaultConnectionLimit = 20;
				Console.ForegroundColor = ConsoleColor.Yellow;
				Config.Title("Vunlerable Scanner");
				Config.Logo();
				Console.WriteLine("Press Any key to Start");
				filePath = Console.ReadLine();
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Choose URLs...";
				openFileDialog.Filter = "Text file (*.txt)|*.txt";
				openFileDialog.ShowDialog();
				filePath = openFileDialog.FileName;
				Console.WriteLine("Result will be in:" + resFolder);
				Console.ForegroundColor = ConsoleColor.Cyan;
				Class1 @class = new Class1();
				list_0.AddRange(File.ReadLines(filePath));
				int_0 = list_0.Count;
				@class.int_0 = 1;
				do
				{
					ParameterizedThreadStart start;
					if ((start = @class.parameterizedThreadStart_0) == null)
					{
						start = (@class.parameterizedThreadStart_0 = new ParameterizedThreadStart(@class.method_0));
					}
					new Thread(start).Start(@class.int_0);
					int num = @class.int_0;
					@class.int_0 = checked(num + 1);
				}
				while (@class.int_0 <= 100);
			}
			catch
			{
				Console.WriteLine("An Error Has 00x1 Contact dev :(");
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002644 File Offset: 0x00000844
		public static void smethod_11(int int_3)
		{
			while (int_3 < list_0.Count - 1)
			{
				int_1++;
				try
				{
					string text = list_0[int_3].ToString();
					list_0.RemoveAt(int_3);
					if (text.Contains("?") && text.Contains("="))
					{
						smethod_12(text);
					}
				}
				catch
				{
				}
				Console.Title = string.Concat(new object[]
				{
					"ArcNet | Inject Scanner",
					Convert.ToString(int_1),
					"/",
					int_0,
					" | Vulnerable: ",
					scanned,
					" | ",
					" Non-vulnerable: ",
					bad.Count.ToString()
				});
			}
			int_2++;
			if (int_2 >= 100)
			{
				
				Console.WriteLine("Finished!");
				Console.ReadKey();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000274C File Offset: 0x0000094C
		public static bool smethod_12(string string_2)
		{
			try
			{
				using (HttpRequest httpRequest = new HttpRequest())
				{
					httpRequest.UserAgent = PickRandomAgent();
					httpRequest.Cookies = new CookieDictionary(false);
					httpRequest.ConnectTimeout = 10000;
					httpRequest.ReadWriteTimeout = 10000;
					httpRequest.KeepAlive = true;
					httpRequest.IgnoreProtocolErrors = true;
					string @object = httpRequest.Get(string_2 + "'", null).ToString();
					if (string_1.Any(new Func<string, bool>(@object.Contains)))
					{
						Console.ForegroundColor = ConsoleColor.Green;
						scanned++;
						list_1.Add(string_2);
						Console.Write("         [ "); Config.PrintBlue("+"); Console.WriteLine(" ] VULNERABLE | " + string_2);
						File.WriteAllLines(resFolder + @"\vulnerable.txt", list_1);
					}
					else
					{
						
					}
				}
			}
			catch
			{
			}
			return true;
		}


		// Token: 0x04000001 RID: 1
		private static List<string> list_0 = new List<string>();

		// Token: 0x04000002 RID: 2
		public static List<string> list_1 = new List<string>();

		// Token: 0x04000003 RID: 3
		private static int int_0;

		// Token: 0x04000004 RID: 4
		private static int int_1;

		// Token: 0x04000005 RID: 5
		private static string string_0 = string.Empty;

		// Token: 0x04000006 RID: 6
		private static int int_2;

		public static List<string> bad = new List<string>();

		// Token: 0x04000007 RID: 7
		private static string[] string_1 = new string[]
		{
			"Warning:",
			"<b>Warning</b>:",
			"mysql_fetch_array()",
			"syntax",
			"failure",
			"invalid",
			"sql",
			"mysql",
			"SQL",
			"MYSQL",
			"FAILURE",
			"You have an error in your SQL syntax",
			"WARNING"

		};

		// Token: 0x04000008 RID: 8
		private static int scanned;

		public static string[] userAgents =
		{
			"Mozilla/5.0 (Windows NT 10.0; WOW64; rv:52.0) Gecko/20100101 Firefox/52.0",
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134",
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/18.17763",
			"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36",
			"Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:49.0) Gecko/20100101 Firefox/49.0"
		};

		public static string PickRandomAgent()
		{
			Random rnd = new Random();
			return userAgents[rnd.Next(0, userAgents.Count())];
		}
		// Token: 0x02000003 RID: 3
		private sealed class Class1
		{
			// Token: 0x06000012 RID: 18 RVA: 0x00002093 File Offset: 0x00000293
			internal void method_0(object object_0)
			{
				smethod_11(this.int_0);
			}

			// Token: 0x0400000A RID: 10
			public int int_0;

			// Token: 0x0400000B RID: 11
			public ParameterizedThreadStart parameterizedThreadStart_0;
		}
	}
}

