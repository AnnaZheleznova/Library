using System.Collections.Generic;

namespace Library.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public IEnumerable<Book> books { get; set; }
    }
}
