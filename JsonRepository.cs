using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MovieLibrary
{
    public class JsonRepository : IRepository
    {


        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public JsonRepository(string filePath) 
        {
            this.filePath = filePath;
            Movies = new List<Movie>();
            // read data from file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Movie movie = new Movie();
                    string line = sr.ReadLine();
                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in movie title
                        // movie details are separated with ,
                        string[] movieDetails = line.Split(',');
                        movie.Id = UInt64.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                    }
                    else
                    {
                        // quote = comma in movie title
                        // extract the movieId
                        movie.Id = UInt64.Parse(line.Substring(0, idx - 1));
                        // remove Id and first quote from string
                        line = line.Substring(idx + 1);
                        // find the next quote
                        idx = line.IndexOf('"');
                        // extract movieTitle
                        movie.title = line.Substring(0, idx);
                        // remove title and last comma from the string
                        line = line.Substring(idx + 2);
                        // replace the "|" with ", "
                        movie.genres = line.Split('|').ToList();
                    }
                    Movies.Add(movie);
                }
                // close file when finished
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
   
        }
        public string filePath { get; set; }
        public List<Movie> Movies { get; set; }


        public void Add(Movie movie)
        {
            try
            {
                // first generate movie id
                movie.Id = Movies.Max(m => m.Id) + 1;
                // if title contains a comma, wrap it in quotes
                string title = movie.title.IndexOf(',') != -1 ? $"\"{movie.title}\"" : movie.title;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{movie.Id},{title},{string.Join("|", movie.genres)}");
                sw.Close();
                // add movie details to Lists
                Movies.Add(movie);
                // log transaction
                logger.Info("Movie id {Id} added", movie.Id);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public List<Movie> GetAll()
        {
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>("{title: Sample }");
            return movies;
        }
    }
}