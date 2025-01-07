using System.Drawing;
using System.IO;
using IniParser;
using IniParser.Model;
using Console = Colorful.Console;

namespace ArcNet
{
    class Config
    {
        public static int threads = 0;
        public static int pages = 0;
        public static string proxytype = "";

        


        public static void LoadConfig()
        {
            /*
             * 
             * - Improved Load of Configuration - 
             *
            */
     
            if (!File.Exists("config.ini"))
            {
                File.WriteAllText("config.ini",
                   @"
                    [DorkSearcher] 
                    threads=100 
                    pages=15
                    proxytype=proxyless

                   ");
            }
            var file = new FileIniDataParser();
            IniData data = file.ReadFile("config.ini");

            // Parse INI Config
            string load_config_var1 = data["DorkSearcher"]["threads"];
            string load_config_var2 = data["DorkSearcher"]["pages"];
            string load_config_var4 = data["DorkSearcher"]["proxytype"];

            // Stores Variables
            threads = int.Parse(load_config_var1);
            pages = int.Parse(load_config_var2);
            proxytype = load_config_var4;

        }

        public static void Logo()
        {
            Console.Clear();
            Console.Write( 
                @"
                  ,▄▄▓█▓▓▓▓▓██▄▄,          
               ,▄████████████████▓▓╖       
             ╓█▓████▓▓▓▓▓▓▓▓▓▓▓██████▄     
            ▄▓███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓████▓█    
           ▓▓▓▓█▀▓█▓╙▓▓▓█▓▓▓▓▓▓▓█▌▒█▓███   
          ╒╢╣▒▒▒░▀░░░╙░░▓▓▀ÿ▓╙▓▄▀▓▓▓██▓▌  
          ╫╢▒▒▒▒░░░░░░░░░░░░░ ░░░▀█▓▓▓▌▒╣  
          ╟╢▒▒▒▒░░░░░░░░░░▄▓▄░░░░░▐▓▓▓▌╢╣  
          ╘╣▒▒▒▒░░░░░░░░░░▀▓▓▄░░░░▓▓▓▓█╢╛  
           ╟╣▒▒▒░░░░░░▓▓▄░░▄▓▓▓▄░░▓▓▓▓▓▌   
            ╚╣▒▒▒▒░░░░░░▀▓██▓▓▓▓▄█▓▓█▓▀    
             `╬▒▒▒▒▒░░░░▓▓▀▀█▓▓▓▓▓▓▓█   
                ╙╣▒▒▒▄▄██▓▓█▓▓▓▓▓█▀`       
                   ╙▀▀▓█▓▓▓▓▓▀▀▀`         
                DorkMaster FOSS
                ", Color.CornflowerBlue);
          

            Console.WriteLine("\n");
            Console.ResetColor();
        }
        
        public static void Title(string text)
        {
            Console.Title = "DorkMaster X | v6.0.0 | Reborn " + text;
        }

        public static void PrintBlue(string text)
        {
            Console.Write(text, Color.SkyBlue);
        }

        public static void PrintRed(string text)
        {
            Console.Write("         " + text, Color.MediumVioletRed);
        }

        public static void PrintCyan(string text)
        {

            Console.Write("         " + text, Color.DodgerBlue);
         
        }

        public static void PrintError(string text)
        {
            Console.Write(text, Color.Red);
        }

        public static void PrintNfo(string text)
        {
            Console.Write(text, Color.CornflowerBlue);
        }
    }
}
