using System;
using System.Collections.Generic;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);
                        
                        int count = 0;
                        
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        
                        foreach (string item in results)
                        {
                            Console.Write("{0,-45}", item);
                            count++;

                            if (count % 5 == 0)
                            {
                                if (Console.BackgroundColor == ConsoleColor.White)
                                
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                else
                                    Console.BackgroundColor = ConsoleColor.White;

                                Console.WriteLine();
                            }
                        }
                        
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(
                "{0,-45}{1,-45}{2,-45}{3,-45}{4,-45}",
                "POSITION","COMPANY","LOCATION","DESCRIPTION","SKILLS"
            );

            /*Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(
                "{0,-45}{1,-45}{2,-45}{3,-45}{4,-45}", 
                "=========================================",
                "=========================================",
                "=========================================",
                "=========================================",
                "========================================="
            );*/

            Console.WriteLine();

            int count = 0;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            foreach (Dictionary<string, string> jobs in someJobs)
            {   
                foreach (KeyValuePair<string, string> job in jobs)
                {
                    Console.Write("{0,-45}", job.Value);
                    count++;

                    if (count % 5 == 0)
                    {
                        if (Console.BackgroundColor == ConsoleColor.White)
                                
                            Console.BackgroundColor = ConsoleColor.Gray;
                        else
                            Console.BackgroundColor = ConsoleColor.White;
                    }
                }
                
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}
