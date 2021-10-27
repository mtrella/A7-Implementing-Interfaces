using System.Globalization;
using System.IO.MemoryMappedFiles;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;



namespace MovieLibrary
{
    public class FileRepository : IRepository
    {
        private readonly string _filename = Path.Combine(Environment.CurrentDirectory, "Files", movies.csv);
        public FileRepository()
        {
            if (!File.Exists(_filename)) throw new FileNotFoundException($"Unable to locate {_filename}");
        }

        public void Add(Movie movie)
        {

        }

        public List<Movie> GetAll()
        {
            IEnumerable<Movie> records;

            using (var reader = new StreamReader(_filename))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<MovieMap>();

                    records = csv.GetRecords<Movie>().ToList();
                }
            }

            return new List<Movie>(records);
        }
    }
}