using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    interface IBookRepository
    {
        List<LibraryCard> InsertBook(LibraryCard libraryCard);
        bool DeleteBook(int id);
        bool NewGenre(Book book);
        List<Book> AllBookByAuthor(Author author);
        List<Book> AllBookByGenre(Genre genre);

    }
}
