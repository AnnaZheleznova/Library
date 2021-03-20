using Dapper;
using Library.Context;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DAL
{
    public class PersonRepository : IPersonRepository
    {
        private DataContext _context;

        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public List<object> DeleteLibraryCard(int bookId, int personId)
        {
            LibraryCard libraryCard = new LibraryCard { BookId = bookId, PersonId = personId };
            if (libraryCard != null)
            {
                _context.LibraryCards.Remove(libraryCard);
                _context.SaveChanges();
            }

            return Get(personId);
        }

        public bool DeletePersonFIO(Person ourPerson)
        {
            var person = _context.People.FirstOrDefault(u => u.FirstName == ourPerson.FirstName && u.LastName == ourPerson.LastName && u.MiddleName == ourPerson.MiddleName);
            if (person != null)
            {
                _context.People.Remove(person);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePersonId(int Id)
        {
            var person = _context.People.Find(Id);
            if (person != null)
            {
                _context.People.Remove(person);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<object> Get(int Id)
        {
            List<object> result = new List<object>();
            var persons = _context.People.Include(c => c.LibraryCards).FirstOrDefault(u => u.Id == Id);
            foreach (var item in persons.LibraryCards)
            {
                var books = _context.Books.Find(item.BookId);
                var authors = _context.Authors.Find(_context.Books.FirstOrDefault(u => u.Id == item.BookId).AuthorId);
                var author = new Author { FirstName = authors.FirstName, LastName = authors.LastName, MiddleName = authors.MiddleName };
                result.Add(books.Name);
                result.Add(author);
                var genres = _context.BookGenres.Where(u => u.BookId == books.Id).ToList();

                foreach (var genre in genres)
                {
                    var findgenre = _context.Genres.Find(genre.GenreId);
                    var genr = new Genre { GenreName = findgenre.GenreName };
                    result.Add(genr);
                }
            }
            return result;
        }

        public List<object> InsertLibraryCard(int bookId, int personId)
        {
            LibraryCard libraryCard = new LibraryCard { BookId = bookId, PersonId = personId };
            if (libraryCard != null)
            {
                _context.LibraryCards.Add(libraryCard);
                _context.SaveChanges();
            }
            return Get(personId);
        }

        public Person InsertPerson(Person ourPerson)
        {
            var persons = new Person
            {
                FirstName = ourPerson.FirstName,
                LastName = ourPerson.LastName,
                MiddleName = ourPerson.MiddleName,
                BirthDay = ourPerson.BirthDay
            };
            _context.People.Add(persons);
            _context.SaveChanges();
            return persons;
        }

        public Person UpdatePerson(Person ourPerson)
        {
            Person person = _context.People.FirstOrDefault(u => u.Id == ourPerson.Id);
            if (person != null)
            {
                person.BirthDay = ourPerson.BirthDay;
                person.FirstName = ourPerson.FirstName;
                person.LastName = ourPerson.LastName;
                person.MiddleName = ourPerson.MiddleName;
                _context.SaveChanges();
            }
            return person;
        }
    }
}

