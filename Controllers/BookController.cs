using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public static IEnumerable<Book> books = new List<Book>
        {
            new Book { Id = 1, Name = "Война и мир", Author = "Толстой", Genre = "Роман" },
            new Book { Id = 2, Name = "Преступление и наказание", Author = "Достоевский", Genre = "Роман" },
            new Book { Id = 3, Name = "Прекрасные и проклятые", Author = "Фицджеральд", Genre = "Роман" }
        };

        // GET: api/book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return  books;
        }

        // GET api/book/2
        [HttpGet("{author}")]
        public ActionResult<Book> Get(string author)
        {
            Book book = books.FirstOrDefault(x=> x.Author == author);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        // POST api/book
        [HttpPost]
        public IEnumerable<Book> Post(Book book)
        {
            if (book == null)
            {
                return (IEnumerable<Book>)BadRequest();
            }

            books.ToList().Add(book);
            //var b = books.First();
            //b.Author = null;
            //return b.serialize();
            return books;
        }

        // DELETE api/<HumanController>/5
        [HttpDelete("{author}/{name}")]
        public ActionResult<Book> Delete(string Author, string Name)
        {
            Book book = books.FirstOrDefault(x => x.Author == Author);
            book = books.FirstOrDefault(x => x.Name == Name);
            if (book == null)
            {
                return NotFound();
            }
            books.ToList().Remove(book);
            return Ok();
        }
    }
}
