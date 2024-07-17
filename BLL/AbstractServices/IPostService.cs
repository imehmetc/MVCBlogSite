using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractServices
{
    public interface IPostService
    {
        Task CreatePost(PostDto postDto);
        Task<List<PostDto>> GetAllPosts();
        Task<List<PostDto>> GettAllUnApprovedPosts();
        Task DeletePost(int postId);
        Task ApprovePost(int postId);
        Task LikePost(int postId);
        Task<List<PostDto>> GetPostByCategory(int categoryId);
        Task ReportPost(int postId, ComplainDto complainDto);
    }
}
