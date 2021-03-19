using Library.Context;
using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context=context;
        }

        [Route("book/post")]
        [HttpPost]
        public ActionResult Post([FromBody]Book book)
        {
            var author = _context.Authors.Find(book.AuthorId);
            var genre = " ";
            if (author == null)
            {
                author = new Author { FirstName = book.Author.FirstName, LastName = book.Author.LastName, MiddleName = book.Author.MiddleName };
                _context.Authors.Add(author);
            }

            foreach (var b in book.BookGenres)
            {
                genre = b.Genre.GenreName;
            }

            var genreFind = _context.Genres.Include(u => u.BookGenres).ThenInclude(u => u.Genre).FirstOrDefault(u=>u.GenreName==genre);
            if(genreFind == null)
            {
                genreFind = new Genre { GenreName = genre };
                _context.Genres.Add(genreFind);
            }

            Book newbook = new Book { Name = book.Name,AuthorId=author.Id };
            _context.Books.Add(newbook);

            BookGenre bookGenre = new BookGenre { BookId = newbook.Id, GenreId = genreFind.Id };
            newbook.BookGenres.Add(bookGenre);

            _context.SaveChanges();
            return Ok();
        }

        [Route("book/delete/{Id}")]
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var library = _context.Books.Include(u => u.LibraryCards);
            foreach (var b in library)
            {
                var card= b.LibraryCards;
                foreach (var c in card)
                { 
                    if (c.BookId != Id)
                    {
                        var book = _context.Books.Find(Id);
                        _context.Books.Remove(book);
                        _context.SaveChanges();
                        return Ok();
                    }
                }
            }
            return BadRequest("Книга у пользователя");
        }

        [Route("book/byAuthor/{FirstName}/{LastName}/{MiddleName}")]
        [HttpGet]
        public ActionResult GetByAuthor(string FirstName, string LastName, string MiddleName)
        {
            List<object> result = new List<object>();
            var authors = _context.Authors.Include(u => u.Books).ThenInclude(u => u.BookGenres).ThenInclude(u => u.Genre).Where(p => p.FirstName==FirstName && p.LastName==LastName && p.MiddleName == MiddleName).ToList();
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
            return Ok(result);
        }

        [Route("book/byGenre/{Genre}")]
        [HttpGet]
        public ActionResult GetByGenre(string Genre)
        {
            List<object> result = new List<object>();
            var genres = _context.Genres.Include(u => u.BookGenres).ThenInclude(u => u.Book).ThenInclude(u => u.Author).Where(u => u.GenreName == Genre).ToList();
            foreach (var a in genres)
            {
                var newGenre = new { Genre = a.GenreName };
                result.Add(newGenre);

                foreach (var b in a.BookGenres)
                {
                    var newBook = new { Book = b.Book.Name };
                    result.Add(newBook);
                    var authors = _context.Authors.Include(u => u.Books).ThenInclude(u => u.BookGenres).ThenInclude(u => u.Genre).Where(u=>u.Id==b.Book.AuthorId).ToList();

                    foreach (var g in authors)
                    {
                        var newAuthor = new { Author = g.FirstName+" "+g.LastName+" "+g.MiddleName };
                        result.Add(newAuthor);
                    }
                }
            }
            return Ok(result);
        }

        [Route("book/put/{Id}")]
        [HttpPut]
        public ActionResult PutGenre(int Id, List<int> genres)
        {
            List<int> ourGenre = new List<int>();
            List<int> newGenre = genres;
            List<int> compare = new List<int>();

            var books = _context.Books.Include(u => u.BookGenres).ThenInclude(u => u.Genre).FirstOrDefault(u=>u.Id==Id);
            foreach(var genred in books.BookGenres)
            {
                    ourGenre.Add(genred.Genre.Id);
            }

            if (ourGenre.Count == newGenre.Count)
            {
                compare = ourGenre.Except(newGenre).ToList();
                foreach (var item in compare)
                {
                    var oldInformation = books.BookGenres.FirstOrDefault(u => u.GenreId == item);
                    books.BookGenres.Remove(oldInformation);
                    _context.SaveChanges();

                }
                compare = newGenre.Except(ourGenre).ToList();
                foreach (var item in compare)
                {
                    var newInformation = _context.Genres.Find(item);
                    books.BookGenres.Add(new BookGenre { Book = books, Genre = newInformation });
                    _context.SaveChanges();

                }
            }
            if (ourGenre.Count > newGenre.Count)
            {
                compare = ourGenre.Except(newGenre).ToList();
                foreach (var item in compare) {
                    var oldInformation = books.BookGenres.FirstOrDefault(u => u.GenreId == item);
                    books.BookGenres.Remove(oldInformation);
                    _context.SaveChanges();

                }
            }
            if (newGenre.Count > ourGenre.Count)
            {
                compare = newGenre.Except(ourGenre).ToList();
                foreach (var item in compare)
                {
                    var newInformation = _context.Genres.Find(item);
                    books.BookGenres.Add(new BookGenre { Book=books,Genre=newInformation});
                    _context.SaveChanges();

                }
            }

            List<object> result = new List<object>();
            var bookss = _context.Books.Include(u => u.BookGenres).ThenInclude(u => u.Genre).FirstOrDefault(u => u.Id == Id);
            var ourBook = new { bookss.Name };
            result.Add(ourBook);

            foreach (var a in bookss.BookGenres)
            {
                var ourGenree = new { Genre = a.Genre.GenreName };
                result.Add(ourGenree);
            }
            var ourAuthor = _context.Books.Include(u => u.Author).Where(u => u.Id == Id).ToList();
            foreach (var d in ourAuthor)
            {
                var sgdfg = new { Author = d.Author.FirstName + " " + d.Author.LastName + " " + d.Author.MiddleName };
                result.Add(sgdfg);
            }

            return Ok(result);
        }


    }
}
