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
        List<Author> GetAllBookByAuthor();
        bool AddAuthor();
        bool DeleteAuthor();
    }
}
