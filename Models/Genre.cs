using System.Collections.Generic;

namespace Library.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public List<BookGenre> BookGenres { get; set; }
        
        public Genre()
        {
            BookGenres = new List<BookGenre>();
        }
    }
}
