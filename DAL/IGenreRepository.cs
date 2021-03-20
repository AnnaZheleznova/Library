using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IGenreRepository
    {
        List<string> GetGenres();
        List<string> AddGenres(Genre genre);
        List<object> Statistic();

    }
}
