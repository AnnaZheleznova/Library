using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Library.Models
{
    public class Book
    {
        [NotNull]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Author { get; set; }
        [NotNull]
        public string Genre { get; set; }
    }
}
