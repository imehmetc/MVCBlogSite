using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Photo { get; set; }
        public bool IsApproved { get; set; } = false;
        public int Likes { get; set; }
        public int UserId { get; set; }


        // Relational Properties
        public User User { get; set; }
        public List<Complain> Complains { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}
