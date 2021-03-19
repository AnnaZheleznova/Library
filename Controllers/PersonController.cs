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
    public class PersonController : ControllerBase
    {
        private DataContext _context;
        private PersonRepository _ourPersonRepository;

        public PersonController(DataContext context)
        {
            _context = context;
        }

        [Route("person/{Id}")]
        [HttpGet]
        public ActionResult Get(int Id)
        {
            var persons = _context.People.Include(c => c.LibraryCards).ThenInclude(sc => sc.Book).Where(p => p.Id == Id).ToList();
            return Ok(persons);
        }

        [Route("person/post")]
        [HttpPost]
        public ActionResult Post([FromBody] Person ourPerson)
        {
            var persons = new Person
            {
                FirstName = ourPerson.FirstName,
                LastName = ourPerson.LastName,
                MiddleName = ourPerson.MiddleName,
                BirthDay = ourPerson.BirthDay
            };
            _context.People.Add(persons);
            _context.SaveChanges();
            return Ok(persons);
        }

        [Route("person/update")]
        [HttpPost]
        public List<Person> Update([FromBody] Person ourPerson )
        {
            List<Person> persons= _ourPersonRepository.UpdatePerson(ourPerson);
            return persons;
        }

        [Route("person/delete")]
        [HttpDelete]
        public ActionResult<Person> Delete([FromBody] Person ourPerson)
        {
            bool result = _ourPersonRepository.DeletePersonFIO(ourPerson);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("person/delete/{Id}")]
        [HttpDelete]
        public ActionResult<Person> Delete(int id)
        {
            bool result = _ourPersonRepository.DeletePersonId(id);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("person/getbookperson/{bookId}/{personId}")]
        [HttpPost]
        public ActionResult GetBookPerson(int bookId, int personId)
        {
            var persons = new { BookId=bookId,PersonId=personId };
            
            return Ok(persons);
        }

        [Route("person/PutBookPerson/{bookId}/{personId}")]
        [HttpDelete]
        public List<LibraryCard> PutBookPerson(int bookId, int personId)
        {
            List<LibraryCard> persons = _ourPersonRepository.DeleteLibraryCard(bookId,personId);
            return persons;
        }

    }
}
