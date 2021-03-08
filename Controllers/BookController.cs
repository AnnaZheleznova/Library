using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        
        // GET: api/book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return  Ok();
        }

        // GET api/book/2
        [HttpGet("{author}")]
        public ActionResult<Book> Get(string author)
        {
            return Ok();
        }

        // POST api/book
        [HttpPost]
        public ActionResult<IEnumerable<Book>> Post(Book book)
        {
            return Ok();
        }

        // DELETE api/<HumanController>/5
        [HttpDelete("{author}/{name}")]
        public ActionResult<Book> Delete(string Author, string Name)
        {
            return Ok();
        }
    }
}
