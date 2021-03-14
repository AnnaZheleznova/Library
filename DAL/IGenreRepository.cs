using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IGenreRepository
    {
        List<Genre> GetGenres();
        bool AddGenres(Genre genre);
        int Statistic(Genre genre);

    }
}
