using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }

        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<LibraryCard> LibraryCards { get; set; }

        public DataContext(DbContextOptions<DataContext> dbContext) : base(dbContext)
        {
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Author>().HasKey(p => p.Id);
            modelBuilder.Entity<Book>().HasKey(p => p.Id);
            modelBuilder.Entity<Genre>().HasKey(p => p.Id);
            modelBuilder.Entity<Person>().HasKey(p => p.Id);
            modelBuilder.Entity<BookGenre>().HasKey(p => new { p.BookId, p.GenreId });
            modelBuilder.Entity<LibraryCard>().HasKey(p => new { p.BookId, p.PersonId });
            modelBuilder.Entity<BookGenre>().HasOne(sc => sc.Book).WithMany(s => s.BookGenres).HasForeignKey(sc => sc.BookId);
            modelBuilder.Entity<BookGenre>().HasOne(sc => sc.Genre).WithMany(s => s.BookGenres).HasForeignKey(sc => sc.GenreId);

            modelBuilder.Entity<LibraryCard>().HasOne(sc => sc.Book).WithMany(s => s.LibraryCards).HasForeignKey(sc => sc.BookId);
            modelBuilder.Entity<LibraryCard>().HasOne(sc => sc.Person).WithMany(s => s.LibraryCards).HasForeignKey(sc => sc.PersonId);
            modelBuilder.Entity<Book>().HasOne(sc => sc.Author).WithMany(s => s.Books).HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<Author>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<Author>().Property(p => p.LastName).IsRequired();

            modelBuilder.Entity<Book>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Genre>().Property(p => p.GenreName).IsRequired();

            modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired();

            base.OnModelCreating(modelBuilder);
        }



    }
}
