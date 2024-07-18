using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class PostLikeDto : BaseDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }


        // Relational Properties
        public PostDto PostDto { get; set; }
        public UserDto UserDto { get; set; }
    }
}
