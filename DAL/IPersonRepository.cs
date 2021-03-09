using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    internal interface IPersonRepository
    {
        List<LibraryCard > GetPerson(int Id);
        List<Person> InsertPerson(Person person);
        List<Person> UpdatePerson(Person person);
        bool DeletePersonId(int Id);
        bool DeletePersonFIO(Person person);
        List<LibraryCard> GetBookPerson(int bookId, int personId);
        List<LibraryCard> PutBookPerson(int bookId, int personId);
    }
}
