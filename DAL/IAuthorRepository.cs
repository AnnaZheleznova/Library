using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IAuthorRepository
    {
        public List<string> GetAllAuthor();
        public List<object> GetBook(int Id);
        public Author AddAuthor(Author author);
        public bool DeleteAuthor(Author author);
    }
}
