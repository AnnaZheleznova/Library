using Library.DAL;
using Library.Models;
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
    public class PersonController : ControllerBase
    {
        private PersonRepository _ourPersonRepository;

        public PersonController()
        {
            _ourPersonRepository = new PersonRepository();
        }

        // GET: api/<HumanController>
        [Route("person")]
        [HttpGet]
        public List<Person> Get()
        {
            return _ourPersonRepository.GetPerson();
        }

        [HttpPost]
        public bool Post([FromBody] Person ourPerson)
        {
            //return true;
            return _ourPersonRepository.InsertPerson(ourPerson);
        }

        [HttpPut]
        public bool Put([FromBody] Person ourPerson)
        {
            return _ourPersonRepository.UpdatePerson(ourPerson);
        }

        [Route("Customers/{fio}")]
        [HttpDelete]
        public bool Delete(string fio)
        {
            return _ourPersonRepository.DeletePersonFIO(fio);
        }



        [Route("Customers/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return _ourPersonRepository.DeletePersonId(id);
        }
    }
}
