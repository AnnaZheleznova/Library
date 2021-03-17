namespace Library.Models
{
    public class LibraryCard
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
