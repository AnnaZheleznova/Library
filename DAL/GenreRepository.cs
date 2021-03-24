using Library.Context;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DAL
{
    public class GenreRepository : IGenreRepository
    {
        private DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public List<string> InsertGenre(Genre genre)
        {
            Genre NewGenre = new Genre { GenreName = genre.GenreName };
            _context.Genres.Add(NewGenre);
            _context.SaveChanges();
            return GetGenres();
        }

        public List<string> GetGenres()
        {
            var Genres = _context.Genres.Select(u => u.GenreName).ToList();
            return Genres;
        }

        public List<object> Statistic()
        {
            var Statistics = _context.BookGenres.Include(u => u.Genre).GroupBy(u => u.Genre.GenreName).Select(u => new { u.Key, Count = u.Count() }).ToList();
            List<object> result = new List<object>();
            result.Add(Statistics);
            return result;
        }
    }
}
