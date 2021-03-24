using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IGenreRepository
    {
        List<string> GetGenres();
        List<string> InsertGenre(Genre genre);
        List<object> Statistic();

    }
}
