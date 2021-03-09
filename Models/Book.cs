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
        public int bookId { get; set; }
        public string name { get; set; }
        public int authorId { get; set; }

        public Author Author { get; set; }

        public List<Genre> genres { get; set; }
    }
}
