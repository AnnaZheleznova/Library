using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IBookRepository
    {
        List<LibraryCard> InsertBook(LibraryCard libraryCard);
        bool DeleteBook(int id);
        List<LibraryCard> NewGenre(Book book);
        List<Book> AllBookByAuthor(Author author);
        List<Book> AllBookByGenre(Genre genre);

    }
}
