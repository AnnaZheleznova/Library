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
        private IPersonRepository _personRepository;

        public PersonController(DataContext dataContext)
        {
            _context = dataContext;
            _personRepository = new PersonRepository(_context);
        }

        [Route("person/{Id}")]
        [HttpGet]
        public ActionResult Get(int Id)
        {
            var result = _personRepository.Get(Id);
            return Ok(result);
        }

        [Route("person/post")]
        [HttpPost]
        public ActionResult Post([FromBody] Person ourPerson)
        {
            var result = _personRepository.InsertPerson(ourPerson);
            return Ok(result);
        }

        [Route("person/update")]
        [HttpPost]
        public ActionResult Update([FromBody] Person ourPerson )
        {
            var result = _personRepository.UpdatePerson(ourPerson);
            return Ok(result);
        }

        [Route("person/delete")]
        [HttpDelete]
        public ActionResult Delete([FromBody] Person ourPerson)
        {
            var result = _personRepository.DeletePersonFIO(ourPerson);
            if(result==true)
            {
                return Ok();
            }
            return BadRequest("Возникла ошибка");
        }

        [Route("person/delete/{Id}")]
        [HttpDelete]
        public ActionResult<Person> Delete(int Id)
        {
            var result = _personRepository.DeletePersonId(Id);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest("Данного Id не существует");
        }

        [Route("person/getbookperson/{bookId}/{personId}")]
        [HttpPost]
        public ActionResult GetBookPerson(int bookId, int personId)
        {
            var result = _personRepository.InsertLibraryCard(bookId, personId);
            return Ok(result);
        }

        [Route("person/PutBookPerson/{bookId}/{personId}")]
        [HttpDelete]
        public ActionResult PutBookPerson(int bookId, int personId)
        {
            var result = _personRepository.DeleteLibraryCard(bookId, personId);

            return Ok(Get(personId));
        }

    }
}
