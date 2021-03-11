using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    internal interface IPersonRepository
    {
        List<LibraryCard > GetPersonBooks(int Id);
        List<Person> InsertPerson(Person person);
        List<Person> UpdatePerson(Person person);
        bool DeletePersonId(int Id);
        bool DeletePersonFIO(Person person);
        List<LibraryCard> InsertLibraryCard(int bookId, int personId);
        List<LibraryCard> DeleteLibraryCard(int bookId, int personId);
    }
}
