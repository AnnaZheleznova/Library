using Library.Context;
using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly DataContext _context;

        public GenreController(DataContext dataContext)
        {
            _context = dataContext;
            _genreRepository = new GenreRepository(_context);
        }


        [Route("genre/get")]
        [HttpGet]
        public ActionResult Get()
        {
            var result = _genreRepository.GetGenres();
            return Ok(result);
        }

        [Route("genre/statistic")]
        [HttpGet]
        public ActionResult Getstatics()
        {
            var result = _genreRepository.Statistic();
            return Ok(result);
        }

        [Route("genre/post")]
        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            var result = _genreRepository.AddGenres(genre);
            return Ok(result);
        }
    }
}
