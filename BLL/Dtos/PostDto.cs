using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class PostDto : BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsApproved { get; set; } = false;
        public int Likes { get; set; }
        public int ViewCount { get; set; }
        public int UserId { get; set; }


        // Relational Properties
        public UserDto UserDto { get; set; }
        public List<ComplainDto> ComplainDtos { get; set; }
        public List<PostLikeDto> PostLikeDtos { get; set; }
    }
}
