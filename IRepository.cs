using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary
{
    public interface IRepository
    {
        void Add(Movie movie);
        List<Movie> GetAll();
    }
}