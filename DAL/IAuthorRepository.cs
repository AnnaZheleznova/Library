using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    interface IAuthorRepository
    {
        List<Author> GetAllAuthor();
        List<LibraryCard> GetAllBookByAuthor(Author author);
        bool AddAuthor();
        bool DeleteAuthor(Author author);
    }
}
