﻿using System.Collections.Generic;

namespace Library.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public List<Book> Books { get; set; } 
    }
}
