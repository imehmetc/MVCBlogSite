﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Complain : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }


        // Relational Properties
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
