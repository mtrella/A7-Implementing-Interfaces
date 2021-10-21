using System;
using System.Collections.Generic;
using NLog;

namespace MovieLibrary
{
    class Program
    {
        //class  logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string jfile = "movies.json";
            string mfile = "movies.csv";
            string sFile = "shows.csv";
            string vFile = "videos.csv";
            logger.Info("Program started");

            MovieFile movieFile = new MovieFile(mfile);
            ShowFile showFile = new ShowFile(sFile);
            VideoFile videoFile = new VideoFile(vFile);
            JsonRepository jsonrepo = new JsonRepository(jfile);
            
            string choice = "";

                Console.WriteLine();
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1. What media type to display");
                Console.WriteLine("2. Enter to quit");
                //input
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                if (choice == "1")
                {
                    // Ask what type to display
                    Console.WriteLine("What type would you like to display (Movie, Show, or Video)");
                    string typeChoice = "";
                    typeChoice = Console.ReadLine();

                    if (typeChoice == "Movie"){
                        
                        foreach(Movie m in jsonrepo.Movies)
                        {
                            Console.WriteLine(m.Display());
                        }
                            
                    }
                    if (typeChoice == "Show")
                    {
                        foreach(Show s in showFile.Shows)
                        {
                            Console.WriteLine(s.Display());
                        }
                    }
                    if (typeChoice == "Video")
                    {
                        foreach(Video v in videoFile.Videos)
                        {
                            Console.WriteLine(v.Display());
                        }
                    }
                } else if (choice == "2")
                {
                    //Quit the program
                    Console.WriteLine("You have exited the program");
                }
        }
    }
}

