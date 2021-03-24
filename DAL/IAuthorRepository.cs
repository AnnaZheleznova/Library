using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IAuthorRepository
    {
        public List<string> Get();
        public List<object> GetById(int Id);
        public Author AddAuthor(Author author);
        public bool Delete(Author author);
    }
}
