using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using xNet;


namespace ArcNet.Modules
{
    class Proxychecker
    {
        public static int good, bad, totalProixes = 0;
        public static List<string> proxies,goodProxies = new List<string>();
        static readonly string resFolder = @"Results\Proxychecker\" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
        
        public static void MainMenu()
        {
            Directory.CreateDirectory(resFolder);
            Config.Logo();
            Config.Title("Proxy Checker ");
            Config.PrintBlue("");
            Console.Write("         [ "); Config.PrintBlue("1"); Console.Write(" ] "); Console.Write(@"∙HTTP "); Config.PrintBlue(" ↓\n");
            Console.Write("         [ "); Config.PrintBlue("2"); Console.Write(" ] "); Console.Write(@"∙SOCKS4 "); Config.PrintBlue(" ↓\n");
            Console.Write("         [ "); Config.PrintBlue("3"); Console.Write(" ] "); Console.Write(@"∙SOCKS5 "); Config.PrintBlue(" ↓\n");
            Config.PrintBlue("\t: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    CheckHTTPProxies();
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                default:
                    MainMenu();
                    break;
            }
            
            

            // Multi-Threads Checking...
        }

        static void WriteStatus(int currentProxyindex)
        {
            Config.Logo();
            Config.PrintBlue("| "); Console.Write("Total Proxies: "); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(totalProixes.ToString());
            Config.PrintBlue("| "); Console.Write("Proxies Checked: "); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(currentProxyindex + "/" + totalProixes.ToString());
            Config.PrintBlue("| "); Console.Write("Good Proxies: "); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(good.ToString());
            Config.PrintBlue("| "); Console.Write("Bad Proxies: "); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(bad.ToString());
            Config.PrintBlue("| "); Console.Write("API: "); Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("[ DynDNS ]");
            
        }

        static void CheckHTTPProxies()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose Proxies...",
                Filter = "Text file (*.txt)|*.txt"
            };
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            string[] arrayproxies = File.ReadAllLines(filePath);
            foreach (var item in arrayproxies)
            {
                proxies.Add(item);
            }
            for (int i = 0; i < proxies.Count; i++)
            {
                HttpRequest httpRequest = new HttpRequest
                {
                    KeepAlive = false,
                    Proxy = HttpProxyClient.Parse(proxies[i]),
                    ConnectTimeout = 5000,
                    UserAgent = Http.FirefoxUserAgent()
                };

                try
                {
                    HttpResponse resposnse = httpRequest.Get("https://azenv.net");
                    good++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\tGOOD =>" + proxies[i]);
                    File.WriteAllText(resFolder + @"\good.txt", proxies[i]);
                    Config.Title($"Proxy Checker | Good:{good} | Bad{bad}  ");
                }
                catch (Exception)
                {
                    bad++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tBAD =>" + proxies[i]);
                    Config.Title($"Proxy Checker | Good:{good} | Bad{bad}  ");
                }
            }
        }

        static void CheckSocks4Proxies()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose Proxies...",
                Filter = "Text file (*.txt)|*.txt"
            };
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            string[] arrayproxies = File.ReadAllLines(filePath);
            foreach (var item in arrayproxies)
            {
                proxies.Add(item);
            }
            for (int i = 0; i < proxies.Count; i++)
            {
                HttpRequest httpRequest = new HttpRequest
                {
                    KeepAlive = false,
                    Proxy = Socks4ProxyClient.Parse(proxies[i]),
                    ConnectTimeout = 5000,
                    UserAgent = Http.FirefoxUserAgent()
                };

                try
                {
                    HttpResponse resposnse = httpRequest.Get("https://azenv.net");
                    good++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\tGOOD =>" + proxies[i]);
                    File.WriteAllText(resFolder + @"\good.txt", proxies[i]);
                    Config.Title($"Proxy Checker | Good:{good} | Bad{bad}  ");
                }
                catch (Exception)
                {
                    bad++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tBAD =>" + proxies[i]);
                    Config.Title($"Proxy Checker | Good:{good} | Bad{bad}  ");
                }
            }
        }

        static void CheckSocks5Proxies()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose Proxies...",
                Filter = "Text file (*.txt)|*.txt"
            };
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            string[] arrayproxies = File.ReadAllLines(filePath);
            foreach (var item in arrayproxies)
            {
                proxies.Add(item);
            }
            for (int i = 0; i < proxies.Count; i++)
            {
                HttpRequest httpRequest = new HttpRequest
                {
                    KeepAlive = false,
                    Proxy = Socks5ProxyClient.Parse(proxies[i]),
                    ConnectTimeout = 5000,
                    UserAgent = Http.FirefoxUserAgent()
                };

                try
                {
                    HttpResponse resposnse = httpRequest.Get("https://azenv.net");
                    good++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\tGOOD =>" + proxies[i]);
                    File.WriteAllText(resFolder + @"\good.txt", proxies[i]);
                    Config.Title($"Proxy Checker | Good:{good} | Bad{bad}  ");
                }
                catch (Exception)
                {
                    bad++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tBAD =>" + proxies[i]);
                    Config.Title($"Proxy Checker | Good:{good} | Bad{bad}  ");
                }
            }
        }
    }
}
