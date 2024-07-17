using AutoMapper;
using BLL.AbstractServices;
using BLL.Dtos;
using DAL.AbstractRepositories;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ConcreteServices
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public PostService(IRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task ApprovePost(int postId)
        {
            var postToBeApproved = await _postRepository.GetByIdAsync(postId);
            postToBeApproved.IsApproved = true;
            await _postRepository.UpdateAsync(postToBeApproved);
        }

        public async Task CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.AddAsync(post);
        }

        public async Task DeletePost(int postId)
        {
            await _postRepository.DeleteAsync(postId);
        }

        public async Task<List<PostDto>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllAsync();
            posts = posts.Where(x => x.IsApproved); // Admin onayı olanları getir.
            return _mapper.Map<List<PostDto>>(posts);
        }
        public async Task<List<PostDto>> GettAllUnApprovedPosts()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<List<PostDto>>(posts);
        }
        public Task<List<PostDto>> GetPostByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task LikePost(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            post.Likes++;
            await _postRepository.UpdateAsync(post);
        }

        public Task ReportPost(int postId, ComplainDto complainDto)
        {
            throw new NotImplementedException();
        }
    }
}
