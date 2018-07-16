using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurSports1.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string AuthorName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

        public string Image { get; set; }
    }
}
