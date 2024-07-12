using DAL.Entities;

namespace MVCBlogSite.Models
{
    public class ComplainViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }


        // Relational Properties
        public UserViewModel UserViewModel { get; set; }
        public PostViewModel PostViewModel { get; set; }
    }
}
