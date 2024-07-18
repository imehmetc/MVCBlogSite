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
    public class PostLikeService : IPostLikeService
    {
        private readonly IRepository<PostLike> _postLikeRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public PostLikeService(IRepository<PostLike> postLikeRepository, IRepository<Post> postRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _postLikeRepository = postLikeRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task LikePost(int userId, int postId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var post = await _postRepository.GetByIdAsync(postId);

            PostLike postLike = new PostLike();
            postLike.Post = post;
            postLike.User = user;
            post.Likes++;

            await _postLikeRepository.AddAsync(postLike);
            await _postRepository.UpdateAsync(post);
        }

        public async Task UnLikePost(int userId, int postId)
        {
            var postLikes = await _postLikeRepository.GetAllAsync();
            var postLike = postLikes.FirstOrDefault(x => x.PostId == postId && x.UserId == userId);

            await _postLikeRepository.RemoveAsync(postLike.Id);

            var post = await _postRepository.GetByIdAsync(postId);

            post.Likes--;

            await _postRepository.UpdateAsync(post);
            
        }

        public async Task<List<PostLikeDto>> GetAllPostLikes()
        {
            var postLikes = await _postLikeRepository.GetAllAsync();

            return _mapper.Map<List<PostLikeDto>>(postLikes);
        }
    }
}
