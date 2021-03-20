using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    interface IBookRepository
    {
        public bool InsertBook(Book book);
        public bool DeleteBook(int Id);
        public List<object> NewGenre(int Id, List<int> genres);
        public List<object> AllBookByAuthor(string FirstName, string LastName, string MiddleName);
        public List<object> AllBookByGenre(string Genre);

    }
}
