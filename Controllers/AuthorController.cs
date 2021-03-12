using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private AuthorRepository _ourAuthorRepository;

        public AuthorController()
        {
            _ourAuthorRepository = new AuthorRepository();
        }

        [Route("author/{action}")]
        [HttpGet]
        public List<Author> Get()
        {
            List<Author> authors = _ourAuthorRepository.GetAllAuthor();
            return authors;
        }

        [Route("author/bookbyauthor")]
        [HttpGet]
        public List<LibraryCard> GetBook(Author author)
        {
            List<LibraryCard> authors = _ourAuthorRepository.GetAllBookByAuthor(author);
            return authors;
        }


        [Route("author/{action}")]
        [HttpDelete]
        public ActionResult<Author> Delete([FromBody] Author author)
        {
            bool authors = _ourAuthorRepository.DeleteAuthor(author);
            if (authors == true)
            {
                return Ok();
            }
            return BadRequest("Нельзя удалить автора пока есть книги");
        }

        [Route("author/addauthor")]
        [HttpPost]
        public List<Author> AddAuthor([FromBody] Author author)
        {
            List<Author> authors = _ourAuthorRepository.AddAuthor(author);
            return authors;
        }

    }
}
