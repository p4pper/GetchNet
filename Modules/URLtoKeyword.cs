using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcNet.Modules
{
    class URLtoKeyword
    {
        static string resFolder = @"Results\URLtoKeyword" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
       public static void MainMenu()
        {
            
            
            List<string> words = new List<string>();
            Config.Logo();
            Console.WriteLine(@"Choose your text file here...");
            string filePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose Text...";
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            openFileDialog.ShowDialog();
            filePath = openFileDialog.FileName;
            Directory.CreateDirectory(resFolder);
            var getLines = File.ReadAllLines(filePath);
            // ['www.google'],''
            foreach(string item in getLines)
            {
                string tr;
                tr = item.Substring(item.IndexOf('/') + 1).TrimStart();
                words.Add(tr);
            }
           for (int i =0; i < words.Count; i++)
            {
                words[i] = words[i].Replace("/", " ");
                words[i] = words[i].Replace("www", ".");
                words[i] = words[i].Replace(".", " ");
            }

            Console.WriteLine("        " + words.Count.ToString() + " Words Found!");
            Console.WriteLine("        " + "URLs Extract done!");
            File.WriteAllLines(resFolder + @"\results.txt", words);
            Config.PrintBlue("Done!!, Words Extracted\nFile saved at " + filePath + "-worlds.txt");
            Console.ReadKey();
            MainMenu();
        } 
    }
}
