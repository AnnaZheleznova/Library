using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private PersonRepository _ourPersonRepository;

        public PersonController()
        {
            _ourPersonRepository = new PersonRepository();
        }

        [Route("person/{Id}")]
        [HttpGet]
        public List<LibraryCard> Get(int Id)
        {
            List<LibraryCard> persons = _ourPersonRepository.GetPersonBooks(Id);
            return persons;
        }

        [Route("person/{action}")]
        [HttpPost]
        public List<Person> Post([FromBody] Person ourPerson)
        {
            List<Person> persons = _ourPersonRepository.InsertPerson(ourPerson);
            return persons;
        }

        [Route("person/{action}")]
        [HttpPost]
        public List<Person> Update([FromBody] Person ourPerson )
        {
            List<Person> persons= _ourPersonRepository.UpdatePerson(ourPerson);
            return persons;
        }

        [Route("person/{action}")]
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

        [Route("person/{action}/{Id}")]
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
        public List<LibraryCard> GetBookPerson(int bookId, int personId)
        {
            List<LibraryCard> persons = _ourPersonRepository.InsertLibraryCard(bookId,personId);
            return persons;
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
