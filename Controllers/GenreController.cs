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
    public class GenreController : ControllerBase
    {
        private readonly DataContext _context;

        public GenreController(DataContext context)
        {
            _context = context;
        }


        [Route("genre/get")]
        [HttpGet]
        public ActionResult Get()
        {
            var genre = _context.Genres.Select(u=>u.GenreName).ToList();
            return Ok(genre);
        }

        [Route("genre/statistic")]
        [HttpGet]
        public ActionResult Getstatics()
        {
            var statistic = _context.Genres.GroupBy(u=>u.GenreName).ToList();
            return Ok(statistic);
        }

        [Route("genre/post")]
        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
             return Ok();
        }
    }
}
