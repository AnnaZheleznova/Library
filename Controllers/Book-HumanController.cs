using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Book_HumanController : ControllerBase
    {
        Book_HumanContext db;
        public Book_HumanController(Book_HumanContext context)
        {
            db = context;
        }

        // POST api/<HumanController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Book_Human>>> Post(Book_Human book_human)
        {
            if (book_human == null)
            {
                return BadRequest();
            }

            db.BookHuman.Add(book_human);
            await db.SaveChangesAsync();
            return await db.BookHuman.ToListAsync();
        }

    }
}
