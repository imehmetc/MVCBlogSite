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
        Task<List<PostDto>> GetPostByCategory(List<int> categoryIds);
        Task ReportPost(int userId, int postId);
        Task<bool> ComplainExists(int userId, int postId);
        Task<List<ComplainDto>> GetComplainPosts();
        //Task AddPostCategory(int postId, int categoryId);
        Task AddRange(List<PostCategoryDto> postCategoryDto);
        Task Add(PostCategoryDto postCategoryDto);
        Task<List<PostDto>> GetAllUnApprovedPostsFilterByUser(int userId);
    }
}
