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
        [RegularExpression(@"^[a-zA-Z' '?!]+$", ErrorMessage = "Use letters only please")]
        public string Title { get; set; }
        [RegularExpression(@"^[a-zA-Z' ']+$", ErrorMessage = "Use letters only please")]
        public string State { get; set; }
        [RegularExpression("^[a-zA-Z0-9' ']*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Street { get; set; }

        [RegularExpression(@"^[a-zA-Z' ']+$", ErrorMessage = "Use letters only please")]

        public string City { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public int NumHouse { get; set; }


    }
}
