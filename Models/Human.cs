using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Human
    {
        [NotNull]
        public int Id { get; set; }

        [NotNull]
        public string Surname { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string SecondName { get; set; }

        [NotNull]
        public DateTime DateBorn { get; set; }
    }
}
