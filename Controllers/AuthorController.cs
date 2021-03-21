using Library.Context;
using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly DataContext _context;
        public AuthorController(DataContext data)
        {
            _context = data;
            _authorRepository = new AuthorRepository(_context);
        }

        [Route("author/get")]
        [HttpGet]
        public ActionResult Get()
        {
            var authors = _authorRepository.Get();
            return Ok(authors);
        }

        [Route("author/bookbyauthor/{Id}")]
        [HttpGet]
        public IActionResult GetById(int Id)
        {
            var result = _authorRepository.GetById(Id);
            return Ok(result);
        }


        [Route("author/delete")]
        [HttpDelete]
        public IActionResult Delete([FromBody] Author author)
        {
            var result = _authorRepository.Delete(author);
            if(result==true)
            {
                return Ok();
            }
            return BadRequest("Нельзя удалить автора пока есть книги");
        }

        [Route("author/addauthor")]
        [HttpPost]
        public IActionResult AddAuthor([FromBody] Author author)
        {
            var result = _authorRepository.AddAuthor(author);
            return Ok(result);
        }

    }
}
