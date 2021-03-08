using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    internal interface IPersonRepository
    {
        List<Person> GetPerson();
        bool InsertPerson(Person person);
        bool UpdatePerson(Person person);
        bool DeletePersonId(int Id);
        bool DeletePersonFIO(string fio);
        bool GetBookPerson(int bookId, int personId);
        bool PutBookPerson(int bookId, int personId);
    }
}
