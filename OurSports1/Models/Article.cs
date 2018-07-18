using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OurSports1.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Secondary_title { get; set; }

        public string Content { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public DateTime TimeCreate { get; set; }
        public string Image { get; set; }
    }
 }

       



   