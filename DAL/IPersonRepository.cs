using Library.Models;
using System.Collections.Generic;

namespace Library.DAL
{
    internal interface IPersonRepository
    {
        List<object> Get(int Id);
        Person InsertPerson(Person person);
        Person UpdatePerson(Person person);
        bool DeletePersonId(int Id);
        bool DeletePersonFIO(Person person);
        List<object> InsertLibraryCard(int bookId, int personId);
        List<object> DeleteLibraryCard(int bookId, int personId);
    }
}
