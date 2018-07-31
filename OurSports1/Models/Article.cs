using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OurSports1.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Title { get; set; }
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        public string Secondary_title { get; set; }
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        public string Content { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public DateTime TimeCreate { get; set; }

        private string image;
        public string Image
        {
            get
            {
                return this.image;
            }
            set
            {
                if (value == null)
                {
                    this.image = "Sport.jpg";
                }
                else
                {
                    image = value;
                }
            }
        }

    }
}





