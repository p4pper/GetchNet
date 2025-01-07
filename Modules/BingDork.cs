using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace ArcNet.Modules
{
    public class BingDork
    {
        private static  string resFolder = null;
        private static  List<string> res = new List<string>();
        private static string[] keyList = { };

        private static List<string> format_dorks_list = new List< string > ();

        public static void MainMenu()
        {
           
            Config.Logo();
            Config.Title("Bing Dorker");
            string keywords = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Load Keywords...";
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            openFileDialog.ShowDialog();
            keywords = openFileDialog.FileName;
            
            keyList = File.ReadAllLines(keywords);
            //.pagetype ?pageformat= ~keyword
            
            resFolder = @"Results\BingDorkGen" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
            
            Thread dr1 = new Thread(Dork_Format1);
            dr1.Start();
            
            Console.WriteLine("done");
            Console.ReadKey();
            
            //TODO: Add main menu or re-gen


        }

        /* FORMAT 1 ~*/
        protected static void Dork_Format1()
        {
            StringBuilder bld = new StringBuilder();
            for (int i = 0; i < keyList.Length; i++)
            {
                bld.Append(".php ?query = ~");
                bld.Append(keyList[i]);
                bld.AppendLine("");
                format_dorks_list.Add(bld.ToString());
                //Console.WriteLine(dork);
            }
            Shuffle(format_dorks_list);
            format_dorks_list.ForEach(Console.WriteLine);
        }

        private static Random rng = new Random();

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


    }
}