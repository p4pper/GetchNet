using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xNet;
using HtmlAgilityPack;

namespace ArcNet.Modules
{
	//TODO; Investigate and fix bugs
	// TODO; Fix Proxy bug

	class BingSearcher
	{
		
		public static int errors;
		public static int filUrlcount;
		public static string resFolder;
		public static string dorksFilePath;
		public static int getThreads;
		public static int getPages;
		private static readonly List<string> proxies = new List<string>();
		private static readonly List<string> dorks = new List<string>();
		private static readonly List<string> urls = new List<string>();
		public static void MainMenu()
		{
			errors = 0;
			filUrlcount = 0;
			resFolder = null;
			dorksFilePath = null;
			getThreads = 0;
			getPages = 0;
			resFolder = @"Results\BingParser\" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
			Directory.CreateDirectory(resFolder);
			
			Config.Logo();
			Config.PrintCyan("Loading config...");
			Config.LoadConfig();
			Thread.Sleep(700);
			Config.Logo();
			getThreads = Config.threads;
			getPages = Config.pages; 
			Config.PrintBlue("         Pages: "); Console.WriteLine(Config.pages.ToString());
			Config.PrintBlue("         Threads: "); Console.WriteLine(Config.threads.ToString());
			Config.PrintBlue("         Proxy type: "); Console.WriteLine(Config.proxytype.ToUpper());
			Config.PrintBlue("         Is this your correct config? ( Y/N )"); Console.WriteLine("\n         Option: ");
			Config.PrintRed(""); string configChoice = Console.ReadLine();
			Config.PrintCyan("");
			configChoice = configChoice.ToLower();
			switch (configChoice)
			{
				case "y":
					StartSearcher();
					break;

				case "n":
					Config.PrintCyan("Please edit config.ini for your needs, then save it and click enter here");
					Console.ReadKey();
					MainMenu();
					break;

				case null:
					MainMenu();
					break;
			}
			Console.ReadKey();
		}

		public static void StartSearcher()
		{
			Config.Logo();
			LoadDorks();
			BingParserProxyless();
		}

		public static void BingParserProxyless()
		{
			Config.Logo();
			Parallel.For(0, dorks.Count, new ParallelOptions
			{
				MaxDegreeOfParallelism = getThreads
			}, delegate (int x)
			{
                HttpRequest httpRequest = new HttpRequest
                {
                    UserAgent = "Mozilla/5.0 (Linux; Android 7.0; SM-G892A Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.1805 Mobile Safari/537.36",
                    ConnectTimeout = 5000,
                    KeepAlive = false
                };

                string dork = dorks[x];

				for (int i = 1; i < getPages; i++)
				{

					int num = 0;

					int managedThreadId = Thread.CurrentThread.ManagedThreadId;
					while (num != 1)
					{
						try
						{
							bool flag2 = managedThreadId == 1;
							if (flag2)
							{
								WriteStatus();
							}


							string text2, address;
							int page;
							page = i + 11;

							// Will Generate URL, Get Response
							address = GenerateBingUrl(dork, page);
							var getResponse = httpRequest.Get(address);
							text2 = getResponse.ToString();

							int num3 = Convert.ToInt32((float)Encoding.ASCII.GetByteCount(text2) / 1024f);
							bool flag6 = num3 < 100;


							if (flag6)
							{
								throw new Exception();
							}
							HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
							htmlDocument.LoadHtml(text2);
							foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//a[@href]")))
							{
								string HREF_LINK = htmlNode.GetAttributeValue("href", string.Empty);
								bool flag = URL_VERIFICATION_STRINGS.All((string kw) => HREF_LINK.Contains(kw));
								bool flag26 = URL_BAD_STRINGS.Any(new Func<string, bool>(HREF_LINK.Contains));
								bool flag3 = flag && !flag26;
								if (flag3)
								{
									urls.Add(HREF_LINK);

								}
							}

							num = 1;
						}
						catch (Exception)
						{
							errors++;

						}
					}
				}


			});

			Console.WriteLine("\n\n\n\n\nFinish!");
			Console.ReadKey();


		}

		private static void WriteStatus()
		{

			var result = urls
		   .Select(url => new Uri(url))
		   .ToLookup(uri => uri.Host, uri => uri)
		   .Select(v => v.First());

			filUrlcount = result.ToList().Count();

			new Thread(() => {
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(resFolder, "urls.txt")))
				{
					foreach (var line in result.ToList())
						outputFile.WriteLine(line.ToString());
				}
			}).Start();
			
			Config.Logo();
			Config.PrintBlue("         | "); Console.WriteLine("$ Parsing $");
			Config.PrintBlue("         | "); Console.Write("Total URLs: "); Config.PrintBlue(urls.Count<string>().ToString()); Console.WriteLine("");
			Config.PrintBlue("         | "); Console.Write("Filtered URLs: "); Config.PrintBlue(filUrlcount.ToString()); Console.WriteLine("");
			Config.PrintBlue("         | "); Console.Write("Errors: "); Config.PrintBlue(errors.ToString()); Console.WriteLine("");
			Config.Title($"Valid URLs : {filUrlcount} | Errors : {errors} | Total Dorks : {dorks.Count}");
			 
		}

		
		private static string GenerateBingUrl(string dork, int Page)
		{
			return string.Format("http://www.bing.com/search?q={0}&go=Submit&first={1}&count=50", dork, Page);

		}

		private static void LoadDorks()
		{
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose dorks...",
                Filter = "Text file (*.txt)|*.txt"
            };
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            dorksFilePath = filePath;
			try
			{

				string[] array = File.ReadAllLines(dorksFilePath);
				foreach (string item in array)
				{
					dorks.Add(item);
				}
				Config.Logo();

				Console.WriteLine("         " + "Dorks loaded: " + dorks.Count<string>().ToString());
			}
			catch
			{
				Config.Logo();

				Console.WriteLine("         No dork file found");

				Console.ReadKey();
				LoadDorks();
			}
		}


		private static void LoadProxies()
		{
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose Proxies...",
                Filter = "Text file (*.txt)|*.txt"
            };
            openFileDialog.ShowDialog();
			
			string[] array = File.ReadAllLines(openFileDialog.FileName);
			foreach (string item in array)
			{
				proxies.Add(item);
			}

			Config.Logo();

			Console.WriteLine("         Proxies loaded: " + proxies.Count<string>().ToString());
		}


		private static readonly string[] URL_VERIFICATION_STRINGS = new string[]
		{
			"http",
			".",
			"=",
			"/",
			"php",
			"&",
			"asp",
			"?"
		};

		private static readonly string[] URL_BAD_STRINGS = new string[]
		{
			"microsoft",
			"google",
			"youtube",
			"facebook",
			"stackoverflow",
			"bing",
			"yahoo",
			"amazon",
			"wikipedia",
			"yandex",
			"instagram"
		};


		private class MyWebClient : WebClient
		{
			protected override WebRequest GetWebRequest(Uri uri)
			{
				WebRequest webRequest = base.GetWebRequest(uri);
				webRequest.Timeout = 5000;
				return webRequest;
			}
		}
	}
}

