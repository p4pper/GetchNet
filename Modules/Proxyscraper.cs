using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ArcNet.Modules
{
    class Proxyscraper
    {
        public static List<string> proxies = new List<string>();
        public static int scrapedProxies = 0;
        static string resFolder = @"Results\ProxyScraper\" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;

        private static bool isFinish = false;

        static Thread http = new Thread(HTTPScraper);
        static Thread socks4 = new Thread(SOCKS4Scraper);
        static Thread socks5 = new Thread(SOCKS5Scraper);

        public static void MainMenu()
        {
            Config.Logo();
            Config.Title("Proxy Scraper ");
            Config.PrintCyan("What do you like to scrape today?\n");
            Config.PrintCyan("1"); Console.Write(" ] "); Console.Write(@"∙HTTP "); Config.PrintBlue(" ↓\n");
            Config.PrintCyan("2"); Console.Write(" ] "); Console.Write(@"∙SOCKS4 "); Config.PrintBlue(" ↓\n");
            Config.PrintCyan("3"); Console.Write(" ] "); Console.Write(@"∙SOCKS5 "); Config.PrintBlue(" ↓\n");
            Config.PrintCyan(": ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Directory.CreateDirectory(resFolder);

            Thread statusThread = new Thread(WriteStatus);
            switch (choice)
            {
                case 1:
                    statusThread.Start();
                    http.Start();
                    break;
                case 2:
                    statusThread.Start();
                    socks4.Start();
                    break;
                case 3:
                    statusThread.Start();
                    socks5.Start();
                    break;
                default:
                    MainMenu();
                    break;
            }
        }

        static void WriteStatus()
        {
            while(true)
            {
                Config.Logo();
                Config.PrintCyan("| "); Console.Write("Scraped Proxies "); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(scrapedProxies.ToString());
                Config.PrintCyan("| "); Console.Write("API: "); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("[ Proxyscrape ]");
                Thread.Sleep(25);

                if(isFinish)
                {
                    Config.Logo();
                    http.Abort();
                    socks4.Abort();
                    socks5.Abort();
                    Config.PrintCyan("Scraping done...at > " + resFolder + @"\scraped.txt" + " < \n");
                    Config.PrintCyan("Press ENTER to go main menu, or 'Escape' key to re-scrape");
                    ConsoleKeyInfo input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Escape)
                        MainMenu();
                    break;
                }
              
            }
            Console.ReadKey();
            Program.MainMenu();

        }

        static void HTTPScraper()
        {
            WebClient webClient = new WebClient();
            foreach (Match match in Regex.Matches(webClient.DownloadString("https://api.proxyscrape.com/?request=displayproxies&proxytype=http"), "\\b(\\d{1,3}\\.){3}\\d{1,3}\\:\\d{1,8}\\b", RegexOptions.Singleline))
            {
                proxies.Add(match.Groups[0].Value);
                File.WriteAllLines(resFolder + @"\scraped.txt", proxies);
                scrapedProxies++;
            }
            isFinish = true;
        }

        static void SOCKS4Scraper()
        {
            WebClient webClient = new WebClient();
            foreach (Match match in Regex.Matches(webClient.DownloadString("https://api.proxyscrape.com/?request=displayproxies&proxytype=socks4"), "\\b(\\d{1,3}\\.){3}\\d{1,3}\\:\\d{1,8}\\b", RegexOptions.Singleline))
            {
                proxies.Add(match.Groups[0].Value);
                File.WriteAllLines(resFolder + @"\scraped.txt", proxies);
                scrapedProxies++;
            }
            isFinish = true;

        }

        static void SOCKS5Scraper()
        {
            WebClient webClient = new WebClient();
            foreach (Match match in Regex.Matches(webClient.DownloadString("https://api.proxyscrape.com/?request=displayproxies&proxytype=socks5"), "\\b(\\d{1,3}\\.){3}\\d{1,3}\\:\\d{1,8}\\b", RegexOptions.Singleline))
            {
                proxies.Add(match.Groups[0].Value);
                File.WriteAllLines(resFolder + @"\scraped.txt", proxies);
                scrapedProxies++;
            }
            isFinish = true;
        }

  
    }
}
