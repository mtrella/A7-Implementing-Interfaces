using System;
using System.Collections.Generic;
using NLog;
using System.Linq;

namespace MovieLibrary
{
    class Program
    {
        //class  logger
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string jfile = "../../movies.json";
            string mfile = "../../movies.csv";
            string sFile = "../../shows.csv";
            string vFile = "../../videos.csv";
            logger.Info("Program started");

            MovieFile movieFile = new MovieFile(mfile);
            ShowFile showFile = new ShowFile(sFile);
            VideoFile videoFile = new VideoFile(vFile);
            JsonRepository jsonrepo = new JsonRepository(jfile);
            
            string choice = "";
            do
            {
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
                    System.Console.WriteLine("Enter a movie, show, or video to search for: ");
                    string searchEntry = "";
                    searchEntry = Console.ReadLine();
                    logger.Info("Search Entry: {Search}", searchEntry);

                    //Search for a movie/show/video

                    
                    var movies = from m in movieFile.Movies
                    select m;
                    var shows = from s in showFile.Shows
                    select s;
                    var videos = from v in videoFile.Videos
                    select v;

                    if (!String.IsNullOrEmpty(movies))
                    {
                        movies = movies.Where(s => s.title.Contains(searchEntry));
                    }
                    return;

                    if (!String.IsNullOrEmpty(shows))
                    {
                        movies = movies.Where(s => s.title.Contains(searchEntry));
                    }
                    return;

                    if (!String.IsNullOrEmpty(videos))
                    {
                        movies = movies.Where(s => s.title.Contains(searchEntry));
                    }
                    return;
                
                }
            } while (choice == "1" || choice == "2");

            logger.Info("Program has closed");
        }

        public static string[] searchRecord(string searchEntry, string filepath, int positionOfEntry)
        {
            positionOfEntry--;
            string[] recordNotFound = { "Record not found" };

            try
            {
                string[] lines = System.IO.File.ReadAllLines(@filepath);

            }
            catch(Exception ex)
            {
                return recordNotFound;
                throw new ApplicationException("",ex);
            }
        }

    
    }
}

