namespace MVCBlogSite.Models
{
    public class PostLikeViewModel : BaseViewModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }


        // Relational Properties
        public PostViewModel PostViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }
    }
}
