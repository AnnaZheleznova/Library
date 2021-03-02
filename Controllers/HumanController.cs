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
        public static IEnumerable<Human> humans = new List<Human>
        {
            new Human { Surname = "Иванов", Name = "Иван", SecondName = "Иванович", DateBorn = new DateTime(1995, 09, 20) },
            new Human { Surname = "Петров", Name = "Петр", SecondName = "Петрович", DateBorn = new DateTime(1994, 10, 15)},
            new Human { Surname = "Ковалев", Name = "Денис", SecondName = "Игоревич", DateBorn = new DateTime(1993, 03, 03) }
        };

        // GET: api/<HumanController>
        [HttpGet]
        public IEnumerable<Human> Get()
        {
            return humans;
        }

        // GET api/<HumanController>/5
        [HttpGet("{name}")]
        public ActionResult<Human> Get(string name)
        {
            Human human = humans.FirstOrDefault(x => x.Name == name);
            if (human == null)
                return NotFound();
            return new ObjectResult(human);
        }
        // POST api/<HumanController>
        [HttpPost]
        public IEnumerable<Human> Post(Human human)
        {
            if (human == null)
            {
                return (IEnumerable<Human>)BadRequest();
            }

            humans.ToList().Add(human);
            return humans;
        }


        // DELETE api/<HumanController>/5
        [HttpDelete("{Surname}/{Name}/{SecondName}")]
        public ActionResult<Human> Delete(string Surname, string Name, string SecondName)
        {
            Human user = humans.FirstOrDefault(x => x.Surname == Surname);
            user = humans.FirstOrDefault(x => x.Name == Name);
            user = humans.FirstOrDefault(x => x.SecondName == SecondName);
            if (user == null)
            {
                return NotFound();
            }
            humans.ToList().Remove(user);
            return Ok();
        }
    }
}
