using BLL.Dtos;

namespace MVCBlogSite.Models
{
    public class PostCategoryViewModel : BaseViewModel
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }


        // Relational Properties
        public CategoryViewModel Category { get; set; }
        public PostViewModel Post { get; set; }
    }
}
