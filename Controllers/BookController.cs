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
        private readonly IBookRepository _bookRepository;
        private readonly DataContext _context;

        public BookController(DataContext dataContext)
        {
            _context = dataContext;
            _bookRepository = new BookRepository(_context);
        }

        [Route("book/post")]
        [HttpPost]
        public ActionResult Insert([FromBody]Book book)
        {
            var result = _bookRepository.Insert(book);
            return Ok(result);
        }

        [Route("book/delete/{Id}")]
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = _bookRepository.DeleteBook(Id);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest("Книга у пользователя");
        }

        [Route("book/byAuthor/{FirstName}/{LastName}/{MiddleName}")]
        [HttpGet]
        public ActionResult GetByAuthor(string FirstName, string LastName, string MiddleName)
        {
            var result = _bookRepository.GetByAuthor(FirstName, LastName, MiddleName);
            return Ok(result);
        }

        [Route("book/byGenre/{Genre}")]
        [HttpGet]
        public ActionResult GetByGenre(string Genre)
        {
            var result = _bookRepository.GetByGenre(Genre);
            return Ok(result);
        }

        [Route("book/put/{Id}")]
        [HttpPut]
        public ActionResult InsertGenreByBook(int Id, List<int> genres)
        {
            var result = _bookRepository.InsertGenreByBook(Id, genres);
            return Ok(result);
        }


    }
}
