using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurSports1.Models
{
    public class ViewModle
    {
        public IEnumerable<Article> articles { get; set; }
        public IEnumerable<Author> authors { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public Article article { get; set; }
        public Author author { get; set; }
    }
}
