using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IBookRepository
    {
        public bool Insert(Book book);
        public bool DeleteBook(int Id);
        public List<object> InsertGenreByBook(int Id, List<int> genres);
        public List<object> GetByAuthor(string FirstName, string LastName, string MiddleName);
        public List<object> GetByGenre(string Genre);

    }
}
