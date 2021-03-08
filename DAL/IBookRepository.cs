using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    interface IBookRepository
    {
        bool InsertBook(Book book, Author author,Genre genre);
        bool DeleteBook(int id);
        bool NewGenre();

    }
}
