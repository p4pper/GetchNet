using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;
namespace ArcNet.Modules
{
    public class MultiSearcher
    {

        public static int errors = 0;
        public static int filUrlcount = 0;
        public static string resFolder = "";
        public static string dorksFilePath;
        public static int getThreads = 100;
        public static int getPages = 10;
        private static List<string> proxies = new List<string>();
        private static List<string> dorks = new List<string>();
        private static List<string> urls = new List<string>();


        public static void MainMenu()
        {

            resFolder = @"Results\MutliParser" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
            Directory.CreateDirectory(resFolder);
            Config.Title("Multiy Searcher | URLs : 0 | Filtered : 0 | Dorks: 0");
            Config.Logo();
            Config.PrintCyan("Loading config...");
            Config.LoadConfig();
            Thread.Sleep(700);
            Config.Logo();
            getThreads = Config.threads;
            getPages = Config.pages;
            Config.PrintBlue("         Pages: "); Console.WriteLine(Config.pages.ToString());
            Config.PrintBlue("         Threads: "); Console.WriteLine(Config.threads.ToString());
            Config.PrintBlue("         Proxytype: "); Console.WriteLine(Config.proxytype.ToUpper());
            Config.PrintBlue("         Is this your correct config? ( Y/N )"); Console.WriteLine("\n         Option: ");
            Config.PrintRed(""); string configChoice = Console.ReadLine();
            Config.PrintCyan("");
            configChoice = configChoice.ToLower();
            switch (configChoice)
            {
                case "y":
                    StartTheProccess();
                    break;

                case "n":
                    Console.WriteLine("         Please edit config.ini for your needs, then save it and click enter here");
                    Console.ReadKey();
                    MainMenu();
                    break;
            }
            Console.ReadKey();
        }

        private static void StartTheProccess()
        {
            if (Config.proxytype.ToLower() == "http")
            {
                BingParserHTTP();
            }
            else if (Config.proxytype.ToLower() == "socks4")
            {
                BingParserSocks4();
            }
            else if (Config.proxytype.ToLower() == "proxyless")
            {
                BingParserProxyless();
            }
            else
            {
                BingParserSocks5();
            }
        }
        public static void BingParserHTTP()
        {
            Config.Logo();
            Parallel.For(0, dorks.Count, new ParallelOptions
            {
                MaxDegreeOfParallelism = getThreads
            }, delegate (int x)
            {
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.UserAgent = "Mozilla/5.0 (Linux; Android 7.0; SM-G892A Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.1805 Mobile Safari/537.36";
                httpRequest.ConnectTimeout = 5000;
                httpRequest.KeepAlive = false;

                string dork = dorks[x];

                for (int i = 1; i < getPages; i++)
                {

                    int num = 0;
                    httpRequest.Proxy = HttpProxyClient.Parse(PickRandomProxy());
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    while (num != 1)
                    {
                        try
                        {
                            bool flag2 = managedThreadId == 1;
                            if (flag2)
                            {
                                WriteStatus(getPages, dork);
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


        public static void BingParserProxyless()
        {
            Config.Logo();
            Parallel.For(0, dorks.Count, new ParallelOptions
            {
                MaxDegreeOfParallelism = getThreads
            }, delegate (int x)
            {
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.UserAgent = "Mozilla/5.0 (Linux; Android 7.0; SM-G892A Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.1805 Mobile Safari/537.36";
                httpRequest.ConnectTimeout = 5000;
                httpRequest.KeepAlive = false;

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
                                WriteStatus(getPages, dork);
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

        public static void BingParserSocks4()
        {
            Config.Logo();
            Parallel.For(0, dorks.Count, new ParallelOptions
            {
                MaxDegreeOfParallelism = getThreads
            }, delegate (int x)
            {
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.UserAgent = "Mozilla/5.0 (Linux; Android 7.0; SM-G892A Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.1805 Mobile Safari/537.36";
                httpRequest.ConnectTimeout = 5000;
                httpRequest.KeepAlive = false;

                string dork = dorks[x];

                for (int i = 1; i < getPages; i++)
                {

                    int num = 0;
                    httpRequest.Proxy = Socks4ProxyClient.Parse(PickRandomProxy());
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    while (num != 1)
                    {
                        try
                        {
                            bool flag2 = managedThreadId == 1;
                            if (flag2)
                            {
                                WriteStatus(getPages, dork);
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

        public static void BingParserSocks5()
        {
            Config.Logo();
            Parallel.For(0, dorks.Count, new ParallelOptions
            {
                MaxDegreeOfParallelism = getThreads
            }, delegate (int x)
            {
                HttpRequest httpRequest = new HttpRequest();
                httpRequest.UserAgent = "Mozilla/5.0 (Linux; Android 7.0; SM-G892A Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.1805 Mobile Safari/537.36";
                httpRequest.ConnectTimeout = 5000;
                httpRequest.KeepAlive = false;

                string dork = dorks[x];

                for (int i = 1; i < getPages; i++)
                {

                    int num = 0;
                    httpRequest.Proxy = Socks5ProxyClient.Parse(PickRandomProxy());
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    while (num != 1)
                    {
                        try
                        {
                            bool flag2 = managedThreadId == 1;
                            if (flag2)
                            {
                                WriteStatus(getPages, dork);
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


        private static string PickRandomProxy()
        {
            Random rnd = new Random();
            var pickedProxy = proxies[rnd.Next(0, proxies.Count)];
            return pickedProxy;
        }

        private static void WriteStatus(int pages_per_dork, string dork)
        {

            var result = urls
           .Select(url => new Uri(url))
           .ToLookup(uri => uri.Host, uri => uri)
           .Select(v => v.First());

            filUrlcount = result.ToList().Count();

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(resFolder, "urls.txt")))
            {
                foreach (var line in result.ToList())
                    outputFile.WriteLine(line.ToString());
            }
            Config.Logo();
            Config.PrintBlue("         | "); Console.WriteLine("$ Parsing $");
            Config.PrintBlue("         | "); Console.Write("Total URLs: "); Config.PrintBlue(urls.Count<string>().ToString()); Console.WriteLine("");
            Config.PrintBlue("         | "); Console.Write("Filtered URLs: "); Config.PrintBlue(filUrlcount.ToString()); Console.WriteLine("");
            Config.PrintBlue("         | "); Console.Write("Errors/Null: "); Config.PrintBlue(errors.ToString()); Console.WriteLine("");
            Config.PrintBlue("         | "); Console.Write("Pages Specified: "); Config.PrintBlue(pages_per_dork.ToString()); Console.WriteLine("");
            Config.PrintBlue("         | "); Console.Write("Dorks: "); Config.PrintBlue(dorks.Count.ToString()); ; Console.WriteLine("");
            Config.PrintBlue("         | "); Console.Write("Result Folder: "); Config.PrintBlue(resFolder); Console.WriteLine("");

        }


        private static string GenerateBingUrl(string dork, int Page)
        {
            return string.Format("http://www.bing.com/search?q={0}&go=Submit&first={1}&count=50", dork, Page);

        }


        private static void LoadDorks()
        {
            //Config.printBlue("         Drag drop your dorks file here..."); Console.WriteLine("Input: ");
            //Config.printRed(""); string filePath = Console.ReadLine();
            string filePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose dorks...";
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            openFileDialog.ShowDialog();
            filePath = openFileDialog.FileName;
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

                Console.WriteLine("         No dork file found. Press enter. I am giving you another chance");

                Console.ReadKey();
                LoadDorks();
            }
        }

        private static void LoadProxies()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose Proxies...";
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            openFileDialog.ShowDialog();

            string[] array = File.ReadAllLines(openFileDialog.FileName);
            foreach (string item in array)
            {
                proxies.Add(item);
            }

            Config.Logo();

            Console.WriteLine("         Proxies loaded: " + proxies.Count<string>().ToString());
            StartTheProccess();
        }


        private static string[] URL_VERIFICATION_STRINGS = new string[]
        {
            "http",
            ".",
            "=",
            "/",
            "php",
            "&"
        };

        private static string[] URL_BAD_STRINGS = new string[]
        {
            "microsoft",
            "google",
            "youtube",
            "facebook",
            "stackoverflow",
            "bing",
            "yahoo"
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