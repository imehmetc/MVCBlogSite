using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostCategory : BaseEntity
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }


        // Relational Properties
        public Category Category { get; set; }
        public Post Post { get; set; }
    }
}
