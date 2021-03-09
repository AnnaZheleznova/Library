using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    interface IBookRepository
    {
        List<LibraryCard> InsertBook(Book book, Author author,Genre genre);
        bool DeleteBook(int id);
        bool NewGenre(Book book);
        List<LibraryCard> AllBookByAuthor(Author author);
        List<LibraryCard> AllBookByGenre(Genre genre);

    }
}
