﻿using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace Library.Controllers
{
    [Route("api")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private GenreRepository _ourGenreRepository;

        public GenreController()
        {
            _ourGenreRepository = new GenreRepository();
        }


        [Route("genre/{action}")]
        [HttpGet]
        public List<Genre> Get()
        {
            List<Genre> genres = _ourGenreRepository.GetGenres();
            return genres;
        }

        [Route("genre/statistic")]
        [HttpGet]
        public int Get(Genre genre)
        {
            int genres = _ourGenreRepository.Statistic(genre);
            return genres;
        }

        [Route("genre/{action}")]
        [HttpPost]
        public ActionResult<Genre> Post([FromBody] Genre genre)
        {
            bool genres = _ourGenreRepository.AddGenres(genre);
            if(genres==true)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
