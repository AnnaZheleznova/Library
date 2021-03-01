using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class HumanContext : DbContext
    {
        public DbSet<Human> Humans { get; set; }
        public HumanContext(DbContextOptions<HumanContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
