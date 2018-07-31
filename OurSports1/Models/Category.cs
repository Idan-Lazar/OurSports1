using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OurSports1.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [RegularExpression(@"^[a-zA-Z' ']+$", ErrorMessage = "Use letters only please")]

        public string Title { get; set; }

        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
