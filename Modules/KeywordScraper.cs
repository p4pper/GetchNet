using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using xNet;

namespace ArcNet.Modules
{
    class KeywordScraper
    {
        static public List<string> scrapedList = new List<string>();
        static int scrapedKeywords = 0;
        static string resFolder = @"Results\KeywordScraper\" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
        public static void MainMenu()
        {
            var res = "";
            var thirdOccutation = "";
            Config.Logo();
            Config.Title("Keyword Scraper | DEPRECATED ");
            Config.PrintCyan("\tWhat keyword type you'd like to scrape? e.g fortnite (no caps )\n");
            Config.PrintCyan("\t"); string target = Console.ReadLine();
            Config.Logo();
            Config.PrintCyan("Scraping Keywords...\n");
            HttpRequest http = new HttpRequest();
            http.KeepAlive = false;
            http.ConnectTimeout = 10000;
            http.UserAgent = Http.FirefoxUserAgent();
            var oneTimeRequest = http.Get("http://api.bing.com/osjson.aspx?query=" + target);
            var response = oneTimeRequest.ToString();
            response = response.Replace("[\"", "");
            response = response.Replace(",", "\n");
            response = response.Replace("\"", "");
            response = response.Replace("]]", "");
            Directory.CreateDirectory(resFolder);


  
            using (var reader = new StringReader(response))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {

                    var req = http.Get("http://api.bing.com/osjson.aspx?query=" + line);
                    res = oneTimeRequest.ToString();
                    res = res.Replace("[\"", "");
                    res = res.Replace(",", "\n");
                    res = res.Replace("\"", "");
                    res = res.Replace("]]", "");

                }


            }
            using (var reader = new StringReader(res))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    var req = http.Get("http://api.bing.com/osjson.aspx?query=" + line);
                    res = res + oneTimeRequest.ToString();
                    thirdOccutation = oneTimeRequest.ToString();
                    res = res.Replace("[\"", "");
                    res = res.Replace(",", "\n");
                    res = res.Replace("\"", "");
                    res = res.Replace("]]", "");
                    thirdOccutation = res;
                }
            }
            using (var reader = new StringReader(res))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    scrapedKeywords++;
                    Config.PrintNfo(line + "\n");
                    scrapedList.Add(line);
                }
            }

            Config.PrintCyan("Keywords Scraped : " + scrapedKeywords.ToString() + ": Target: " + target);
            File.WriteAllLines(resFolder + @"\scraped.txt", scrapedList);
            Console.ReadKey();
            Console.Clear();
            Program.MainMenu();
        }



  

    }
}
