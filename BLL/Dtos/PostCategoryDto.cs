using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class PostCategoryDto : BaseDto
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }


        // Relational Properties
        public CategoryDto Category { get; set; }
        public PostDto Post { get; set; }
    }
}
