using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace ArcNet.Modules
{
    class DorkFilter
    {
        public static string resFolder = null;
        public static string filePath = null;
        public static int foundCounter = 0;
        public static int dorksCounter = 0;
        public static int notFoundCounter = 0;

        public static void MainMenu()
        {
            string[] countres =
            {
                "afghanistan",
"albania",
"algeria",
"andorra",
"angola",
"argentina",
"armenia",
"australia",
"austria",
"azerbaijan",
"bahamas",
"bahrain",
"bangladesh",
"barbados",
"belarus",
"belgium",
"belize",
"benin",
"bhutan",
"bolivia",
"bosnia herzegovina",
"botswana",
"brazil",
"brunei",
"bulgaria",
"burkina",
"burundi",
"cambodia",
"cameroon",
"canada",
"cape verde",
"central african rep",
"chad",
"chile",
"china",
"colombia",
"comoros",
"congo",
"congo {democratic rep}",
"costa rica",
"croatia",
"cuba",
"cyprus",
"czech republic",
"denmark",
"djibouti",
"dominica",
"dominican republic",
"east timor",
"ecuador",
"egypt",
"el salvador",
"equatorial guinea",
"eritrea",
"estonia",
"ethiopia",
"fiji",
"finland",
"france",
"gabon",
"gambia",
"georgia",
"germany",
"ghana",
"greece",
"grenada",
"guatemala",
"guinea",
"guinea-bissau",
"guyana",
"haiti",
"honduras",
"hungary",
"iceland",
"india",
"indonesia",
"iran",
"iraq",
"ireland {republic}",
"israel",
"italy",
"ivory coast",
"jamaica",
"japan",
"jordan",
"kazakhstan",
"kenya",
"kiribati",
"korea north",
"korea south",
"kosovo",
"kuwait",
"kyrgyzstan",
"laos",
"latvia",
"lebanon",
"lesotho",
"liberia",
"libya",
"liechtenstein",
"lithuania",
"luxembourg",
"macedonia",
"madagascar",
"malawi",
"malaysia",
"maldives",
"mali",
"malta",
"marshall islands",
"mauritania",
"mauritius",
"mexico",
"micronesia",
"moldova",
"monaco",
"mongolia",
"montenegro",
"morocco",
"mozambique",
"myanmar, {burma}",
"namibia",
"nauru",
"nepal",
"netherlands",
"new zealand",
"nicaragua",
"niger",
"nigeria",
"norway",
"oman",
"pakistan",
"palau",
"panama",
"papua new guinea",
"paraguay",
"peru",
"philippines",
"poland",
"portugal",
"qatar",
"romania",
"russian federation",
"rwanda",
"st lucia",
"samoa",
"san marino",
"saudi arabia",
"senegal",
"serbia",
"seychelles",
"sierra leone",
"singapore",
"slovakia",
"slovenia",
"solomon islands",
"somalia",
"south africa",
"south sudan",
"spain",
"sri lanka",
"sudan",
"suriname",
"swaziland",
"sweden",
"switzerland",
"syria",
"taiwan",
"tajikistan",
"tanzania",
"thailand",
"togo",
"tonga",
"tunisia",
"turkey",
"turkmenistan",
"tuvalu",
"uganda",
"ukraine",
"united arab emirates",
"united kingdom",
"united states",
"uruguay",
"uzbekistan",
"vanuatu",
"vatican city",
"venezuela",
"vietnam",
"yemen",
"zambia",
"zimbabwe"
            };
            List<string> dorksList = new List<string>();
            Config.Title("         Dorks Filter");
            Config.Logo();
            Config.PrintBlue("         Choose File\n");
     
            resFolder = @"Results\Filter" + DateTime.Now.Year + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
            string filePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose Dorks to filter...";
            openFileDialog.Filter = "Text file (*.txt)|*.txt";
            openFileDialog.ShowDialog();
            filePath = openFileDialog.FileName;
            Directory.CreateDirectory(resFolder);
            Console.Write("         [ "); Config.PrintBlue("1"); Console.Write(" ] "); Console.Write(@"∙USA Filter "); Config.PrintBlue(" ↓\n");
            Console.Write("         [ "); Config.PrintBlue("2"); Console.Write(" ] "); Console.Write(@"∙No Country Filter "); Config.PrintBlue(" ↓\n");
            Console.Write("         [ "); Config.PrintBlue("3"); Console.Write(" ] "); Console.Write(@"∙Any Country Filter "); Config.PrintBlue(" ↓\n");
            Console.Write("         [ "); Config.PrintBlue("4"); Console.Write(" ] "); Console.Write(@"∙Target Filter "); Config.PrintBlue(" ↓\n");
            Config.PrintRed(""); int getChoice = Convert.ToInt32(Console.ReadLine());
            if (getChoice == 1)
            {
                string[] getDorks = File.ReadAllLines(filePath);
                dorksCounter = getDorks.Count();
                foreach (string item in getDorks)
                {
                    if (item.Contains(".us") || item.Contains(".com") || item.Contains(".net") || item.Contains("united states"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        dorksList.Add(item);
                        Console.WriteLine("         Found =>" + item);
                        File.WriteAllLines(resFolder + @"\found.txt", dorksList);
                        foundCounter++;
                        Config.Title("Dork Filter | Config:USA Only | Found: " + foundCounter.ToString() + "/" + dorksCounter.ToString());
                    }
                }

                Console.WriteLine("        " + "Finsih!");
                Console.ReadKey();
                Program.MainMenu();

            }
            else if (getChoice == 2)
            {
                string[] getDorks = File.ReadAllLines(filePath);
                dorksCounter = getDorks.Count();

                foreach (string co in countres)
                   {
                    foreach (string item in getDorks)
                    {
                        if (item.Contains(co) == false && item.Contains("site") == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            dorksList.Add(item);
                            Console.WriteLine("Found =>" + item);
                            File.WriteAllLines(resFolder + @"\found.txt", dorksList);
                            foundCounter++;
                            Config.Title("Dork Filter | Config:No Country | Found: " + foundCounter.ToString() + "/" + dorksCounter.ToString() + " | Not Found:" + notFoundCounter.ToString() + "/" + dorksCounter.ToString());
                        } else
                        {
                            notFoundCounter++;
                            Config.Title("Dork Filter | Config:No Country | Found: " + foundCounter.ToString() + "/" + dorksCounter.ToString() + " | Not Found:" + notFoundCounter.ToString() + "/" + dorksCounter.ToString());
                        }
                    }
                    
                }
                Console.WriteLine("        " + "Finsih!");
                Console.ReadKey();
                Program.MainMenu();
            }
            else if (getChoice == 3)
            {
                string[] getDorks = File.ReadAllLines(filePath);
                dorksCounter = getDorks.Count();

                foreach (string co in countres)
                {
                    foreach (string item in getDorks)
                    {
                        if (item.Contains(co) && item.Contains("site"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            dorksList.Add(item);
                            Console.WriteLine("Found =>" + item);
                            File.WriteAllLines(resFolder + @"\found.txt", dorksList);
                            foundCounter++;
                            Config.Title("Dork Filter | Config:Has Country | Found: " + foundCounter.ToString() + "/" + dorksCounter.ToString() + " | Not Found:" + notFoundCounter.ToString() + "/" + dorksCounter.ToString());
                        }
                        else
                        {
                            notFoundCounter++;
                            Config.Title("Dork Filter | Config:Has Country | Found: " + foundCounter.ToString() + "/" + dorksCounter.ToString() + " | Not Found:" + notFoundCounter.ToString() + "/" + dorksCounter.ToString());
                        }
                    }

                }
                Console.WriteLine("        " + "Finsih!");
                Console.ReadKey();
                Program.MainMenu();
                
            } else if (getChoice == 4)
            {
                Config.Logo();
                Config.PrintCyan("~"); Console.WriteLine("Please enter target name, e.g netflix");
                Config.PrintRed("");  string getTarget = Console.ReadLine();
                string[] getDorks = File.ReadAllLines(filePath);
                dorksCounter = getDorks.Count();

                foreach (string co in countres)
                {
                    foreach (string item in getDorks)
                    {
                        if (item.Contains(getTarget))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            dorksList.Add(item);
                            Console.WriteLine("Found =>" + item);
                            File.WriteAllLines(resFolder + @"\found.txt", dorksList);
                            foundCounter++;
                            Config.Title("Dork Filter | Config:Target | Found: " + foundCounter.ToString() + "/" + dorksCounter.ToString() + " | Not Found:" + notFoundCounter.ToString() + "/" + dorksCounter.ToString());
                        }
                        else
                        {
                            notFoundCounter++;
                            Config.Title("Dork Filter | Config:Target | Found: " + foundCounter.ToString() + "/" + dorksCounter.ToString() + " | Not Found:" + notFoundCounter.ToString() + "/" + dorksCounter.ToString());
                        }
                    }

                }
                Console.WriteLine("        " + "Finsih!");
                Console.ReadKey();
                Program.MainMenu();

            }
        }
    }
}
