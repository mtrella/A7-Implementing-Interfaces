using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary
{
    public class Show : MovieType
    {
        public UInt64 season { get; set; }
        public UInt64 episode { get; set; }
        public List<string> writers { get; set; }

        public Show()
        {
            writers = new List<string>();
        }

        public override string Display()
        {
            return $"ID: {Id}\nTitle: {title}\nSeason: {season}\nEpisode: {episode}\nWriters: {string.Join(", ", writers)}\n";
        }
    }
}