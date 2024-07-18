using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PostLike : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }


        // Relational Properties
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
