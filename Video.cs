using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary
{
    public class Video : MovieType
    {
        public string format { get; set; }
        public UInt64 length { get; set; }
        public UInt64 regions { get; set; }

        public Video()
        {
        
        }

        public override string Display()
        {
            return $"ID: {Id}\nTitle: {title}\nFormat: {format}\nLength: {length}\nRegions: {string.Join(", ", regions)}\n";
        }
    }
}