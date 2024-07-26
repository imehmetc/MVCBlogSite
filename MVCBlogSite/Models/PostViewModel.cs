using DAL.Entities;

namespace MVCBlogSite.Models
{
    public class PostViewModel :BaseViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsApproved { get; set; } = false;
        public int Likes { get; set; }
        public int UserId { get; set; }


        // Relational Properties
        public UserViewModel UserViewModel { get; set; }
        public List<ComplainViewModel>? ComplainViewModels { get; set; }
        public List<PostLikeViewModel>? PostLikeViewModels { get; set; }
    }
}
