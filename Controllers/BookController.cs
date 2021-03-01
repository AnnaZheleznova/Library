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
        BookContext db;
        public BookController(BookContext context)
        {
            db = context;
         
            if (!db.Books.Any())
            {
                db.Books.Add(new Book { Name = "Война и мир", Author = "Толстой", Genre = "Роман" });
                db.Books.Add(new Book { Name = "Преступление и наказание", Author = "Достоевский", Genre = "Роман" });
                db.Books.Add(new Book { Name = "Прекрасные и проклятые", Author = "Фицджеральд", Genre = "Роман" });
                db.SaveChanges();
            }
        }
        // GET: api/book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await db.Books.ToListAsync();
        }


        // GET api/book/2
        [HttpGet("{author}")]
        public async Task<ActionResult<Book>> Get(string author)
        {
            Book book = await db.Books.FirstOrDefaultAsync(x => x.Author == author);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        // POST api/book
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Book>>> Post(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();
            return await db.Books.ToListAsync();
        }

        // DELETE api/<HumanController>/5
        [HttpDelete("{author}/{name}")]
        public async Task<ActionResult<Book>> Delete(string Author, string Name)
        {
            Book book = db.Books.FirstOrDefault(x => x.Author == Author);
            book = db.Books.FirstOrDefault(x => x.Name == Name);
            if (book == null)
            {
                return NotFound();
            }
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
