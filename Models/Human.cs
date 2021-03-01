using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Human
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HumanId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public DateTime DateBorn { get; set; }
    }
}
