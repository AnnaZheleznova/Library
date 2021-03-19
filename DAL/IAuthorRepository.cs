using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IAuthorRepository
    {
        List<Author> GetAllAuthor();
        public List<Author> GetAllBookByAuthor(int Id);
        List<Author> AddAuthor(Author author);
        bool DeleteAuthor(Author author);
    }
}
