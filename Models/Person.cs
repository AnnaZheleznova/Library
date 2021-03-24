using System;
using System.Collections.Generic;

namespace Library.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDay { get; set; }
        public List<LibraryCard> LibraryCards { get; set; }
        public Person()
        {
            LibraryCards = new List<LibraryCard>();
        }
    }
}
