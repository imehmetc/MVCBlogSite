using BLL.AbstractServices;
using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ConcreteServices
{
    public class PostService : IPostService
    {
        public Task ApprovePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task CreatePost(PostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDto>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDto>> GetPostByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task LikePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task ReportPost(int postId, ComplainDto complainDto)
        {
            throw new NotImplementedException();
        }
    }
}
