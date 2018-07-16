using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurSports1.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string WriterName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ArticleID { get; set; }
        public virtual Article Article { get; set; }
    }
}
