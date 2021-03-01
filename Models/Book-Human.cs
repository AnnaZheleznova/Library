using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book_Human
    {
        public int BookHumanId { get; set; }

        public string FIOHuman { get; set; }

        public string NameBook { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
