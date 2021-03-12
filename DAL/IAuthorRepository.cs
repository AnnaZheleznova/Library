using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IAuthorRepository
    {
        List<Author> GetAllAuthor();
        List<LibraryCard> GetAllBookByAuthor(Author author);
        List<Author> AddAuthor(Author author);
        bool DeleteAuthor(Author author);
    }
}
