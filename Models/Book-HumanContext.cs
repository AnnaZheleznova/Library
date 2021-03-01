using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book_HumanContext : DbContext
    {
        public DbSet<Book_Human> BookHuman { get; set; }
        public Book_HumanContext(DbContextOptions<Book_HumanContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
