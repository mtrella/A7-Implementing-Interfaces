using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary
{
    public class ShowFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // public property
        public string filePath { get; set; }
        public List<Show> Shows { get; set; }

        public ShowFile(string path)
        {
            Shows = new List<Show>();
            filePath = path;
            // read data from file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Show show = new Show();
                    string line = sr.ReadLine();
                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in movie title
                        // movie details are separated with ,
                        string[] showDetails = line.Split(',');
                        show.Id = UInt64.Parse(showDetails[0]);
                        show.title = showDetails[1];
                        show.season = UInt64.Parse(showDetails[2]);
                        show.episode = UInt64.Parse(showDetails[3]);
                        show.writers = showDetails[4].Split('|').ToList();
                    }
                    else
                    {
                        // quote = comma in show title
                        // extract the showId
                        show.Id = UInt64.Parse(line.Substring(0, idx - 1));
                        // remove Id and first quote from string
                        line = line.Substring(idx + 1);
                        // find the next quote
                        idx = line.IndexOf('"');
                        // extract showTitle
                        show.title = line.Substring(0, idx);
                        // remove title and last comma from the string
                        line = line.Substring(idx + 2);
                        // replace the "|" with ", "
                        show.writers = line.Split('|').ToList();
                    }
                    Shows.Add(show);
                }
                // close file when finished
                sr.Close();
                logger.Info("Movies in file {Count}", Shows.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // public method
        public bool isUniqueTitle(string title)
        {
            if (Shows.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate show title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddShow(Show show)
        {
            try
            {
                // first generate movie id
                show.Id = Shows.Max(m => m.Id) + 1;
                // if title contains a comma, wrap it in quotes
                string title = show.title.IndexOf(',') != -1 ? $"\"{show.title}\"" : show.title;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{show.Id},{title},{string.Join("|", show.writers)}");
                sw.Close();
                // add movie details to Lists
                Shows.Add(show);
                // log transaction
                logger.Info("Show id {Id} added", show.Id);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
