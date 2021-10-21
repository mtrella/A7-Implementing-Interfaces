using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class Movie : MovieType
    {
        // public properties
        public List<string> genres { get; set; }

        // constructor
        public Movie()
        {
            genres = new List<string>();
        }

        // public method
        public override string Display()
        {
            return $"ID: {Id}\nTitle: {title}\nGenres: {string.Join(", ", genres)}\n";
        }


    }
}
