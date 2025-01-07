using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Console = Colorful.Console;


namespace ArcNet.Modules
{
    public class CostumDorkGen
    {

         static List<string> keywords, pageformats, pagetypes, searchFunctions, domainextension = new List<string>();
         private static List<string> geneneratedDorks = new List<string>();
         private static string resFolder = null;
         static string path = null;
         private static int dorkCounter = 0;
         private static int formatCounter = 0;
        
        public static void MainMenu()
        {
            Config.Title("Costum Dork Gen | Formats Found: 0 | Dorks Generated: 0 | Progress: 0%");
            Config.Logo();
            Config.PrintCyan("How Many formats ( MAX 10 )\n         ");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input > 10)
            { 
                MainMenu();
            }
            
            Config.PrintCyan($"Great we have now {input.ToString()} Formats\nHit enter to load your formats from text file");
            Console.ReadKey();
            path = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Load Dork Type...";
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            openFileDialog.ShowDialog();
            path = openFileDialog.FileName;
            bool has_domain_extension = File.ReadAllLines(path).Contains("de");
            resFolder = @"Results\CostumDorkGen\" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
            Directory.CreateDirectory(resFolder);
            if (has_domain_extension)
            {
                OpenFileDialog loadKeyword = new OpenFileDialog();
                loadKeyword.Title = "Load Keyword...";
                loadKeyword.Filter = "Text file (*.txt)|*.txt";
                loadKeyword.ShowDialog();
                keywords = File.ReadAllLines(loadKeyword.FileName).ToList();
                
                OpenFileDialog loadPageFormats = new OpenFileDialog();
                loadPageFormats.Title = "Load Pageformats...";
                loadPageFormats.Filter = "Text file (*.txt)|*.txt";
                loadPageFormats.ShowDialog();
                pageformats = File.ReadAllLines(loadPageFormats.FileName).ToList();
                
                OpenFileDialog loadPageTypes = new OpenFileDialog();
                loadPageTypes.Title = "Load Pagetype...";
                loadPageTypes.Filter = "Text file (*.txt)|*.txt";
                loadPageTypes.ShowDialog();
                pagetypes = File.ReadAllLines(loadPageTypes.FileName).ToList();
                
                OpenFileDialog loadSearchFunctions = new OpenFileDialog();
                loadSearchFunctions.Title = "Load Search functions...";
                loadSearchFunctions.Filter = "Text file (*.txt)|*.txt";
                loadSearchFunctions.ShowDialog();
                searchFunctions = File.ReadAllLines(loadSearchFunctions.FileName).ToList();
                
                OpenFileDialog loadDomainExt = new OpenFileDialog();
                loadDomainExt.Title = "Load Domain extensions...";
                loadDomainExt.Filter = "Text file (*.txt)|*.txt";
                loadDomainExt.ShowDialog();
                domainextension = File.ReadAllLines(loadDomainExt.FileName).ToList();
            }
            else
            {
                OpenFileDialog loadKeyword = new OpenFileDialog();
                loadKeyword.Title = "Load Keyword...";
                loadKeyword.Filter = "Text file (*.txt)|*.txt";
                loadKeyword.ShowDialog();
                keywords = File.ReadAllLines(loadKeyword.FileName).ToList();
                
                OpenFileDialog loadPageFormats = new OpenFileDialog();
                loadPageFormats.Title = "Load Pageformats...";
                loadPageFormats.Filter = "Text file (*.txt)|*.txt";
                loadPageFormats.ShowDialog();
                pageformats = File.ReadAllLines(loadPageFormats.FileName).ToList();
                
                OpenFileDialog loadPageTypes = new OpenFileDialog();
                loadPageTypes.Title = "Load Pagetype...";
                loadPageTypes.Filter = "Text file (*.txt)|*.txt";
                loadPageTypes.ShowDialog();
                pagetypes = File.ReadAllLines(loadPageTypes.FileName).ToList();
                
                OpenFileDialog loadSearchFunctions = new OpenFileDialog();
                loadSearchFunctions.Title = "Load Search functions...";
                loadSearchFunctions.Filter = "Text file (*.txt)|*.txt";
                loadSearchFunctions.ShowDialog();
                searchFunctions = File.ReadAllLines(loadSearchFunctions.FileName).ToList();
                foreach (var line in File.ReadAllLines(path).ToArray())
                {
                    formatCounter++;
                }
                Config.Title($"Costum Dork Gen | Formats Found: {formatCounter.ToString()} | Dorks Generated: 0 | Progress: 0%");
                GenWithoutDomain();
                
            }
        }

   static void GenWithoutDomain()
   {

       foreach (var typeLine in File.ReadAllLines(path).ToArray())
       {
           foreach (var key in keywords)
           {
               foreach (var pt in pagetypes)
               {
                   foreach (var pf in pageformats)
                   {
                       foreach (var sf in searchFunctions)
                       {
                          
                          string newLine = typeLine;
                          newLine = newLine.Replace("pt", pt);
                          newLine =newLine.Replace("pf", pf);
                          newLine = newLine.Replace("kw", key);
                          newLine = newLine.Replace("sf", sf);
                                newLine = newLine + Environment.NewLine;
                         
                           Console.WriteLine(newLine);
                           dorkCounter++;
                           int sum = dorkCounter / 100;
                           Config.Title($"Costum Dork Gen | Formats Found: {formatCounter.ToString()} | Dorks Generated: {dorkCounter.ToString()} | Progress: {sum.ToString()}%");

                                File.AppendAllText(resFolder + @"\results.txt", newLine);


                       }
                       
                       
                   }
               }
           }
       }
      ;
   }

   static void GenWithDomain()
   {
       
   }
    }
}