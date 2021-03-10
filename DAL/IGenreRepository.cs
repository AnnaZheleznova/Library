using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    interface IGenreRepository
    {
        List<Genre> GetGenres();
        bool AddGenres(Genre genre);
        int Statistic(Genre genre);

    }
}
