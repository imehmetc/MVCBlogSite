namespace MVCBlogSite.Models
{
    public class UserViewModel : BaseViewModel
    {
        public string? Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;


        // Relational Properties
        public List<PostViewModel> PostViewModels { get; set; }
        public List<ComplainViewModel> ComplainViewModels { get; set; }
    }
}
