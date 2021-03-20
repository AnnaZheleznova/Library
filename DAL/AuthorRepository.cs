using Dapper;
using Library.Context;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DAL
{
    public class AuthorRepository : IAuthorRepository
    {
        private DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
            BaseDate(_context.Authors);
        }
        private void BaseDate(DbSet<Author> datas)
        {
            if (!datas.Any())
            {
                Author authors1 = new Author { FirstName = "Лев", LastName = "Толстой", MiddleName = "Николаевич" };
                Author authors2 = new Author { FirstName = "Фёдор", LastName = "Достоевский", MiddleName = "Михайлович" };
                Author authors3 = new Author { FirstName = "Иван", LastName = "Тургенев", MiddleName = "Сергеевич" };
                Author authors4 = new Author { FirstName = "Михаил", LastName = "Булгаков", MiddleName = "Афанасьевич" };
                Author authors5 = new Author { FirstName = "Иван", LastName = "Бунин", MiddleName = "Алексеевич" };
                Author authors6 = new Author { FirstName = "Артур", LastName = "Дойл", MiddleName = "Конан" };
                Author authors7 = new Author { FirstName = "Фрэнсис", LastName = "Фицджеральд", MiddleName = "Скотт" };

                _context.Authors.AddRange(authors1, authors2, authors3, authors4, authors5, authors6, authors7);

                Book book1 = new Book { Name = "Война и мир", Author = authors1 };
                Book book2 = new Book { Name = "Преступление и наказание", Author = authors2 };
                Book book3 = new Book { Name = "Отцы и дети", Author = authors3 };
                Book book4 = new Book { Name = "Морфий", Author = authors4 };
                Book book5 = new Book { Name = "Темные аллеи", Author = authors5 };
                Book book6 = new Book { Name = "Приключения Шерлока Холмса", Author = authors6 };
                Book book7 = new Book { Name = "Ночь нежна", Author = authors7 };

                _context.Books.AddRange(book1, book2, book3, book4, book5, book6, book7);

                Genre genre1 = new Genre { GenreName = "Роман" };
                Genre genre2 = new Genre { GenreName = "Детектив" };
                Genre genre3 = new Genre { GenreName = "Рассказ" };
                Genre genre4 = new Genre { GenreName = "Сборник рассказов" };
                _context.Genres.AddRange(genre1, genre2, genre3, genre4);

                book1.BookGenres.Add(new BookGenre { Book = book1, Genre = genre1 });
                book2.BookGenres.Add(new BookGenre { Book = book2, Genre = genre1 });
                book3.BookGenres.Add(new BookGenre { Book = book3, Genre = genre1 });
                book4.BookGenres.Add(new BookGenre { Book = book4, Genre = genre3 });
                book5.BookGenres.Add(new BookGenre { Book = book5, Genre = genre4 });
                book6.BookGenres.Add(new BookGenre { Book = book6, Genre = genre2 });
                book6.BookGenres.Add(new BookGenre { Book = book6, Genre = genre4 });
                book7.BookGenres.Add(new BookGenre { Book = book7, Genre = genre1 });

                Person person1 = new Person { FirstName = "Светлана", LastName = "Волкова", MiddleName = "Петровна", BirthDay = new System.DateTime(1998, 03, 04) };
                Person person2 = new Person { FirstName = "Петр", LastName = "Калужин", MiddleName = "Денисович", BirthDay = new System.DateTime(1995, 10, 20) };
                Person person3 = new Person { FirstName = "Василий", LastName = "Иванов", MiddleName = "Викторович", BirthDay = new System.DateTime(1996, 07, 13) };
                Person person4 = new Person { FirstName = "Мария", LastName = "Кузнецова", MiddleName = "Александровна", BirthDay = new System.DateTime(1997, 06, 14) };
                _context.People.AddRange(person1, person2, person3, person4);

                person1.LibraryCards.Add(new LibraryCard { Book = book3, Person = person1 });
                person2.LibraryCards.Add(new LibraryCard { Book = book5, Person = person2 });
                person3.LibraryCards.Add(new LibraryCard { Book = book2, Person = person3 });
                person4.LibraryCards.Add(new LibraryCard { Book = book4, Person = person4 });
                person2.LibraryCards.Add(new LibraryCard { Book = book7, Person = person2 });
                _context.SaveChanges();
            }
        }

        public Author AddAuthor(Author author)
        {
            var authors = new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName,
            };

            _context.Authors.Add(authors);
            _context.SaveChanges();

            string result = "";
            if (author.Books != null)
            {
                foreach (var item in author.Books)
                {
                    result = item.Name;
                }
                if (result != null)
                {
                    var books = new Book
                    {
                        Name = result,
                        Author = authors
                    };
                    _context.Books.Add(books);
                    _context.SaveChanges();
                }
            }
            return authors;
        }

        public bool DeleteAuthor(Author author)
        {
            var Id = _context.Authors.FirstOrDefault(p => p.FirstName == author.FirstName && p.LastName == author.LastName && p.MiddleName == author.MiddleName);
            var book = _context.Books.FirstOrDefault(p => p.AuthorId == Id.Id);
            if (Id != null && book == null)
            {
                _context.Authors.Remove(Id);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<string> GetAllAuthor()
        {
            var authors = _context.Authors.Select(u => u.FirstName + " " + u.LastName + " " + u.MiddleName).ToList();
            return (authors);
        }

        public List<object> GetBook(int Id)
        {
            List<object> result = new List<object>();
            var authors = _context.Authors.Include(u => u.Books).ThenInclude(u => u.BookGenres).ThenInclude(u => u.Genre).Where(i => i.Id == Id).ToList();
            foreach (var a in authors)
            {
                var newAuthor = new { Author = a.FirstName + " " + a.LastName + " " + a.MiddleName };
                result.Add(newAuthor);
                foreach (var b in a.Books)
                {
                    var newBook = new { Book = b.Name };
                    result.Add(newBook);
                    foreach (var g in b.BookGenres)
                    {
                        var newGenre = new { Genre = g.Genre.GenreName };
                        result.Add(newGenre);
                    }
                }
            }
            return (result);
        }
    }
}
