using Library.Context;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DAL
{
    public class BookRepository : IBookRepository
    {
        private DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public List<object> GetByAuthor(string FirstName, string LastName, string MiddleName)
        {
            List<object> result = new List<object>();
            var AuthorByFIO = _context.Authors.Include(u => u.Books).ThenInclude(u => u.BookGenres).ThenInclude(u => u.Genre).
                            Where(p => p.FirstName == FirstName && p.LastName == LastName && p.MiddleName == MiddleName).ToList();
            foreach (var author in AuthorByFIO)
            {
                var NewAuthor = new { Author = author.FirstName + " " + author.LastName + " " + author.MiddleName };
                result.Add(NewAuthor);

                foreach (var book in author.Books)
                {
                    var NewBook = new { Book = book.Name };
                    result.Add(NewBook);

                    foreach (var genre in book.BookGenres)
                    {
                        var NewGenre = new { Genre = genre.Genre.GenreName };
                        result.Add(NewGenre);
                    }
                }
            }
            return result;
        }

        public List<object> GetByGenre(string Genre)
        {
            List<object> result = new List<object>();
            var GenreByName = _context.Genres.Include(u => u.BookGenres).ThenInclude(u => u.Book).ThenInclude(u => u.Author).
                                            Where(u => u.GenreName == Genre).ToList();
            foreach (var genre in GenreByName)
            {
                var NewGenre = new { Genre = genre.GenreName };
                result.Add(NewGenre);

                foreach (var book in genre.BookGenres)
                {
                    var NewBook = new { Book = book.Book.Name };
                    result.Add(NewBook);
                    var AuthorById = _context.Authors.Include(u => u.Books).ThenInclude(u => u.BookGenres).ThenInclude(u => u.Genre).
                                                    Where(u => u.Id == book.Book.AuthorId).ToList();

                    foreach (var author in AuthorById)
                    {
                        var NewAuthor = new { Author = author.FirstName + " " + author.LastName + " " + author.MiddleName };
                        result.Add(NewAuthor);
                    }
                }
            }
            return result;
        }

        public bool DeleteBook(int Id)
        {
            var LibraryByBookId = _context.LibraryCards.FirstOrDefault(u => u.BookId == Id);
            if (LibraryByBookId == null)
            {
                var BookById = _context.Books.Find(Id);
                _context.Books.Remove(BookById);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Insert(Book book)
        {
            var Author = _context.Authors.Find(book.AuthorId);
            var Genre = " ";
            if (Author == null)
            {
                Author = new Author { FirstName = book.Author.FirstName, LastName = book.Author.LastName, MiddleName = book.Author.MiddleName };
                _context.Authors.Add(Author);
                _context.SaveChanges();
            }

            foreach (var genre in book.BookGenres)
            {
                Genre = genre.Genre.GenreName;
            }

            var GenreByName = _context.Genres.Include(u => u.BookGenres).ThenInclude(u => u.Genre).FirstOrDefault(u => u.GenreName == Genre);
            if (GenreByName == null)
            {
                GenreByName = new Genre { GenreName = Genre };
                _context.Genres.Add(GenreByName);
            }

            Book NewBook = new Book { Name = book.Name, AuthorId = Author.Id };
            _context.Books.Add(NewBook);

            BookGenre NewBookGenre = new BookGenre { BookId = NewBook.Id, GenreId = GenreByName.Id };
            NewBook.BookGenres.Add(NewBookGenre);

            _context.SaveChanges();
            return true;
        }

        public List<object> InsertGenreByBook(int Id, List<int> genres)
        {
            List<int> OurGenreList = new List<int>();
            List<int> NewGenreList = genres;
            List<int> Compare = new List<int>();

            var BookById = _context.Books.Include(u => u.BookGenres).ThenInclude(u => u.Genre).FirstOrDefault(u => u.Id == Id);

            foreach (var genre in BookById.BookGenres)
            {
                OurGenreList.Add(genre.Genre.Id);
            }

            if (OurGenreList.Count == NewGenreList.Count)
            {
                Compare = OurGenreList.Except(NewGenreList).ToList();
                RemoveBookGenre(Compare, BookById);
                Compare = NewGenreList.Except(OurGenreList).ToList();
                AddBookGenre(Compare, BookById);
            }

            if (OurGenreList.Count > NewGenreList.Count)
            {
                Compare = OurGenreList.Except(NewGenreList).ToList();
                RemoveBookGenre(Compare, BookById);
            }
            if (NewGenreList.Count > OurGenreList.Count)
            {
                Compare = NewGenreList.Except(OurGenreList).ToList();
                AddBookGenre(Compare, BookById);
            }

            List<object> result = new List<object>();
            var Book = _context.Books.Include(u => u.BookGenres).ThenInclude(u => u.Genre).FirstOrDefault(u => u.Id == Id);
            var OurBook = new { Book.Name };
            result.Add(OurBook);

            foreach (var genre in Book.BookGenres)
            {
                var OurGenre = new { Genre = genre.Genre.GenreName };
                result.Add(OurGenre);
            }

            var AuthorById = _context.Books.Include(u => u.Author).Where(u => u.Id == Id).ToList();
            foreach (var author in AuthorById)
            {
                var OurAuthor = new { Author = author.Author.FirstName + " " + author.Author.LastName + " " + author.Author.MiddleName };
                result.Add(OurAuthor);
            }

            return result;
        }

        private void AddBookGenre(List<int> Compare, Book BookById)
        {
            foreach (var item in Compare)
            {
                var NewInformation = _context.Genres.Find(item);
                BookById.BookGenres.Add(new BookGenre { Book = BookById, Genre = NewInformation });
                _context.SaveChanges();
            }
        }

        private void RemoveBookGenre(List<int> Compare, Book BookById)
        {
            foreach (var item in Compare)
            {
                var OldInformation = BookById.BookGenres.FirstOrDefault(u => u.GenreId == item);
                BookById.BookGenres.Remove(OldInformation);
                _context.SaveChanges();
            }
        }
    }
}
