using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanController : ControllerBase
    {
        HumanContext db;
        public HumanController(HumanContext context)
        {
            db = context;
            if (!db.Humans.Any())
            {
                db.Humans.Add(new Human { Surname = "Иванов", Name = "Иван", SecondName = "Иванович", DateBorn = new DateTime(1995, 09, 20) });
                db.Humans.Add(new Human { Surname = "Петров", Name = "Петр", SecondName = "Петрович", DateBorn = new DateTime(1994, 10, 15) });
                db.Humans.Add(new Human { Surname = "Ковалев", Name = "Денис", SecondName = "Игоревич", DateBorn = new DateTime(1993, 03, 03) });
                db.SaveChanges();
            }
        }
        // GET: api/<HumanController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Human>>> Get()
        {
            return await db.Humans.ToListAsync();
        }

        // GET api/<HumanController>/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Human>> Get(string name)
        {
            Human human = await db.Humans.FirstOrDefaultAsync(x => x.Name == name);
            if (human == null)
                return NotFound();
            return new ObjectResult(human);
        }
        // POST api/<HumanController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Human>>> Post(Human human)
        {
            if (human == null)
            {
                return BadRequest();
            }

            db.Humans.Add(human);
            await db.SaveChangesAsync();
            return await db.Humans.ToListAsync();
        }


        // DELETE api/<HumanController>/5
        [HttpDelete("{Surname}/{Name}/{SecondName}")]
        public async Task<ActionResult<Human>> Delete(string Surname, string Name, string SecondName)
        {
            Human user = db.Humans.FirstOrDefault(x => x.Surname == Surname);
            user = db.Humans.FirstOrDefault(x => x.Name == Name);
            user = db.Humans.FirstOrDefault(x => x.SecondName == SecondName);
            if (user == null)
            {
                return NotFound();
            }
            db.Humans.Remove(user);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
