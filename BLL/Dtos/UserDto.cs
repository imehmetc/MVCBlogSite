using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class UserDto : BaseDto
    {
        public string? Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;


        // Relational Properties
        public List<PostDto> PostDtos { get; set; }
        public List<ComplainDto> ComplainDtos { get; set; }
    }
}
