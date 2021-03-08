using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Person
    {
        public int personId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string middleName { get; set; }

        public DateTime birthDay { get; set; }
    }
}
