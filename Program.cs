using System;
/*
    Copyright (C) 2024 harethpy

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.
*/
namespace ArcNet
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //  -------------- Initialize App --------------
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Load Configurators
            Config.LoadConfig();
            Config.Title("");
            MainMenu();
        }
        public static void MainMenu()
        {
            Config.Logo();
            //TODO; Add Certain Searcher
            Console.Write("         ► "); Config.PrintBlue("1"); Console.Write(" ◄ "); Console.Write(@" Bing Searcher "); ; Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("2"); Console.Write(" ◄ "); Console.Write(@" Multi Searcher "); Config.PrintError("(DEPRECATED)"); Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("3"); Console.Write(" ◄ "); Console.Write(@" Vunlerability Scanner "); Config.PrintBlue("(REBRAND)"); Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("4"); Console.Write(" ◄ "); Console.Write(@" Dorks Filter "); Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("6"); Console.Write(" ◄ "); Console.Write(@" Proxy Checker "); Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("7"); Console.Write(" ◄ "); Console.Write(@" URL to Keyword "); Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("8"); Console.Write(" ◄ "); Console.Write(@" Bing Dork Gen "); Config.PrintError("(DEPRECATED)");  Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("9"); Console.Write(" ◄ "); Console.Write(@" Dork Generator "); Config.PrintBlue("(REBRAND)");  Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("10"); Console.Write(" ◄ "); Console.Write(@" Proxy scraper ");  Config.PrintBlue(" \n");
            Console.Write("         ► "); Config.PrintBlue("11"); Console.Write(" ◄ "); Console.Write(@" Keyword Scraper "); Config.PrintError("(DEPRECATED)"); Config.PrintBlue(" \n");

            Config.PrintCyan("Choice: ");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Modules.BingSearcher.MainMenu();
                        break;
                    case 2:
                        Modules.MultiSearcher.MainMenu();
                        break;
                    case 3:
                        Modules.InjectScanner.MainMenu();
                        break;
                    case 4:
                        Modules.DorkFilter.MainMenu();
                        break;
                    case 6:
                        Modules.Proxychecker.MainMenu();
                        break;
                    case 7:
                        Modules.URLtoKeyword.MainMenu();
                        break;
                    case 8:
                        Modules.BingDork.MainMenu();
                        break;
                    case 9:
                        Modules.CostumDorkGen.MainMenu();
                        break;
                    case 10:
                        Modules.Proxyscraper.MainMenu();
                        break;
                    case 11:
                        Modules.KeywordScraper.MainMenu();
                        break;
                    default:
                        Program.MainMenu();
                        break;

                } 
            } catch (Exception ex)
            {
                MainMenu();
            }
            
        }
    }
}
