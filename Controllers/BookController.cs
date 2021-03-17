using Library.Context;
using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        private BookRepository _ourBookRepository;

        public BookController(DataContext context)
        {
            _context=context;
        }

        [Route("book/{action}")]
        [HttpPost]
        public List<LibraryCard> Post(LibraryCard libraryCard)
        {
            List<LibraryCard> books = _ourBookRepository.InsertBook(libraryCard);
            return books;
        }

        [Route("book/{action}/{Id}")]
        [HttpDelete]
        public ActionResult<Book> Delete(int Id)
        {
            bool result = _ourBookRepository.DeleteBook(Id);
            if (result == true) { return Ok(); }
            return BadRequest();
        }

        [Route("book/byAuthor")]
        [HttpGet]
        public List<Book> GetByAuthor([FromBody] Author author)
        {
            List<Book> result = _ourBookRepository.AllBookByAuthor(author);
            return  result;
        }

        [Route("book/byGenre")]
        [HttpGet]
        public List<Book> GetByGenre([FromBody] Genre genre)
        {
            List<Book> result = _ourBookRepository.AllBookByGenre(genre);
            return result;
        }

        [Route("book/{action}")]
        [HttpPut]
        public List<LibraryCard> PutGenre(Book book)
        {
            List<LibraryCard> result = _ourBookRepository.NewGenre(book);
            return result;
        }


    }
}
