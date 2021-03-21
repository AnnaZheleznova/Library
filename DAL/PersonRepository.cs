using Library.Context;
using Library.Models;
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
            LibraryCard LibraryCard = new LibraryCard { BookId = bookId, PersonId = personId };
            if (LibraryCard != null)
            {
                _context.LibraryCards.Remove(LibraryCard);
                _context.SaveChanges();
            }

            return Get(personId);
        }

        public bool DeletePersonFIO(Person ourPerson)
        {
            var PersonByFIO = _context.People.FirstOrDefault(u => u.FirstName == ourPerson.FirstName && u.LastName == ourPerson.LastName && u.MiddleName == ourPerson.MiddleName);
            if (PersonByFIO != null)
            {
                _context.People.Remove(PersonByFIO);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePersonId(int Id)
        {
            var PersonById = _context.People.Find(Id);
            if (PersonById != null)
            {
                _context.People.Remove(PersonById);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<object> Get(int Id)
        {
            List<object> result = new List<object>();
            var PersonById = _context.People.Include(c => c.LibraryCards).FirstOrDefault(u => u.Id == Id);

            foreach (var item in PersonById.LibraryCards)
            {
                var Book = _context.Books.Find(item.BookId);
                var Author = _context.Authors.Find(_context.Books.FirstOrDefault(u => u.Id == item.BookId).AuthorId);
                var AuthorById = new Author { FirstName = Author.FirstName, LastName = Author.LastName, MiddleName = Author.MiddleName };
                result.Add(Book.Name);
                result.Add(AuthorById);
                var GenreByBookId = _context.BookGenres.Where(u => u.BookId == Book.Id).ToList();

                foreach (var genre in GenreByBookId)
                {
                    var GenreById = _context.Genres.Find(genre.GenreId);
                    var Genre = new Genre { GenreName = GenreById.GenreName };
                    result.Add(Genre);
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
            var NewPerson = new Person
            {
                FirstName = ourPerson.FirstName,
                LastName = ourPerson.LastName,
                MiddleName = ourPerson.MiddleName,
                BirthDay = ourPerson.BirthDay
            };
            _context.People.Add(NewPerson);
            _context.SaveChanges();
            return NewPerson;
        }

        public Person UpdatePerson(Person ourPerson)
        {
            Person PersonById = _context.People.FirstOrDefault(u => u.Id == ourPerson.Id);
            if (PersonById != null)
            {
                PersonById.BirthDay = ourPerson.BirthDay;
                PersonById.FirstName = ourPerson.FirstName;
                PersonById.LastName = ourPerson.LastName;
                PersonById.MiddleName = ourPerson.MiddleName;
                _context.SaveChanges();
            }
            return PersonById;
        }
    }
}

