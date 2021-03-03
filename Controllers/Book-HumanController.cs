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
        public static IEnumerable<Book_Human> bookhuman = new List<Book_Human> { };

        // POST api/<HumanController>
        [HttpPost]
        public IEnumerable<Book_Human> Post(Book_Human book_human)
        {
            if (book_human == null)
            {
                return (IEnumerable<Book_Human>)BadRequest();
            }

            bookhuman.ToList().Add(book_human);
            return bookhuman;
        }

    }
}
