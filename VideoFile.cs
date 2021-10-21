using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary
{
    public class VideoFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // public property
        public string filePath { get; set; }
        public List<Video> Videos { get; set; }

        public VideoFile(string path)
        {
            Videos = new List<Video>();
            filePath = path;
            // read data from file
            try
            {
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Video video = new Video();
                    string line = sr.ReadLine();
                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in video title
                        // video details are separated with ,
                        string[] videoDetails = line.Split(',');
                        video.Id = UInt64.Parse(videoDetails[0]);
                        video.title = videoDetails[1];
                        video.format = videoDetails[2];
                        video.length = UInt64.Parse(videoDetails[3]);
                        video.regions = UInt64.Parse(videoDetails[4]);
                    }
                    else
                    {
                        // quote = comma in show title
                        // extract the showId
                        video.Id = UInt64.Parse(line.Substring(0, idx - 1));
                        // remove showId and first quote from string
                        line = line.Substring(idx + 1);
                        // find the next quote
                        idx = line.IndexOf('"');
                        // extract showTitle
                        video.title = line.Substring(0, idx);
                        // remove title and last comma from the string
                        line = line.Substring(idx + 2);
                        // replace the "|" with ", "
                        video.regions = UInt64.Parse(line.Substring(0, idx - 1));
                    }
                    Videos.Add(video);
                }
                // close file when finished
                sr.Close();
                logger.Info("Videos in file {Count}", Videos.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // public method
        public bool isUniqueTitle(string title)
        {
            if (Videos.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate show title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddVideo(Video video)
        {
            try
            {
                // first generate movie id
                video.Id = Videos.Max(m => m.Id) + 1;
                // if title contains a comma, wrap it in quotes
                string title = video.title.IndexOf(',') != -1 ? $"\"{video.title}\"" : video.title;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{video.Id},{title},{string.Join("|", video.regions)}");
                sw.Close();
                // add movie details to Lists
                Videos.Add(video);
                // log transaction
                logger.Info("Video id {Id} added", video.Id);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
