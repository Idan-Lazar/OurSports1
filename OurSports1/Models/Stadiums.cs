using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurSports1.Models
{
    public class Stadiums
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }

        public string State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int NumHouse { get; set; }


    }
}
