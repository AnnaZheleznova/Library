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
        private readonly IBookRepository _bookRepository;
        private readonly DataContext _context;

        public BookController(DataContext dataContext)
        {
            _context = dataContext;
            _bookRepository = new BookRepository(_context);
        }

        [Route("book/post")]
        [HttpPost]
        public ActionResult Post([FromBody]Book book)
        {
            var result = _bookRepository.InsertBook(book);
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
            var result = _bookRepository.AllBookByAuthor(FirstName, LastName, MiddleName);
            return Ok(result);
        }

        [Route("book/byGenre/{Genre}")]
        [HttpGet]
        public ActionResult GetByGenre(string Genre)
        {
            var result = _bookRepository.AllBookByGenre(Genre);
            return Ok(result);
        }

        [Route("book/put/{Id}")]
        [HttpPut]
        public ActionResult PutGenre(int Id, List<int> genres)
        {
            var result = _bookRepository.NewGenre(Id, genres);
            return Ok(result);
        }


    }
}
