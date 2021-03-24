using System.Collections.Generic;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGenre> BookGenres { get; set; }
        public List<LibraryCard> LibraryCards { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public Book()
        {
            BookGenres = new List<BookGenre>();
            LibraryCards = new List<LibraryCard>();
        }

    }
}
