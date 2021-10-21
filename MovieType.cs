using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary
{
    public abstract class MovieType
    {
        public UInt64 Id { get; set; }
        public string title { get; set; }
        

        public abstract string Display();
        
    }
}