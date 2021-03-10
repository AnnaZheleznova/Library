using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Library.DAL;

namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private BookRepository _ourBookRepository;

        public BookController()
        {
            _ourBookRepository = new BookRepository();
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

    }
}
