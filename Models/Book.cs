using System.Collections.Generic;

namespace Library.Models
{
    public class Book
    {
        public int bookId { get; set; }
        public string name { get; set; }
        public int authorId { get; set; }

        public IEnumerable<Genre> genres { get; set; }
    }
}
