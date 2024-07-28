using AutoMapper;
using BLL.AbstractServices;
using BLL.Dtos;
using DAL.AbstractRepositories;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Complain> _complainRepository;
        private readonly IComplainRepository _complainRepository1;
        private readonly IRepository<PostCategory> _postCategoryRepository;
        private readonly IRepository<User> _userRepository;
        private readonly AppDbContext _context;

        public PostService(IRepository<Post> postRepository, IMapper mapper, IRepository<Complain> complainRepository, IComplainRepository complainRepository1, IRepository<PostCategory> postCategoryRepository,IRepository<User> userRepository, AppDbContext context)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _complainRepository = complainRepository;
            _complainRepository1 = complainRepository1;
            _postCategoryRepository = postCategoryRepository;
            _userRepository = userRepository;
            _context = context;
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
        public async Task UpdateViewCountAsync(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            post.ViewCount++;
            post.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
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
        public async Task<List<PostDto>> GetPostByCategory(List<int> categoryIds)
        {
            var postCategories = await _postCategoryRepository.GetAllAsync();

            postCategories = postCategories.Where(x => categoryIds.Contains(x.CategoryId)).ToList();
            
            List<Post> allPosts = new();
            foreach (var item in postCategories)
            {
                var post = await _postRepository.GetByIdAsync(item.PostId);
                if (post != null && post.IsApproved == true)
                    allPosts.Add(post);
            }
           
            return _mapper.Map<List<PostDto>>(allPosts.Distinct());
        }

        public async Task ReportPost(int userId, int postId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var post = await _postRepository.GetByIdAsync(postId);

            var complain = _mapper.Map<ComplainDto>(new Complain());
            complain.PostDto = _mapper.Map<PostDto>(post);
            complain.UserDto = _mapper.Map<UserDto>(user);
            complain.UserId = userId;
            complain.PostId = postId;
            complain.Title = "Şikayet Başlığı";
            complain.Content = "Şikayet İçeriği";

            await _complainRepository.AddAsync(_mapper.Map<Complain>(complain));
        }

        public async Task<bool> ComplainExists(int userId, int postId)
        {
            var complains = await _complainRepository.GetAllAsync();
            var complainedPost = complains.FirstOrDefault(x => x.UserId == userId && x.PostId == postId);

            if (complainedPost != null)
                return true;

            return false;
        }


        public async Task<List<ComplainDto>> GetComplainPosts()
        {
            List<Complain> allComplains = new();

            var complains = await _complainRepository.GetAllAsync();

            foreach (var item in complains)
            {
                var isHere = allComplains.Any(x => x.PostId == item.PostId);
                if (!isHere)
                {
                    allComplains.Add(item);
                }
            }
            var complainViewModels = _mapper.Map<List<ComplainDto>>(allComplains);
            
            foreach (var complain in complainViewModels)
            {
                complain.ComplainCount = await _complainRepository1.CountComplains(complain.PostId);
            }

            return complainViewModels;
        }

        public async Task AddRange(List<PostCategoryDto> postCategoryDto)
        {
            await _postCategoryRepository.AddRangeAsync(_mapper.Map<List<PostCategory>>(postCategoryDto));
        }
        public async Task Add(PostCategoryDto postCategoryDto)
        {
            await _postCategoryRepository.AddAsync(_mapper.Map<PostCategory>(postCategoryDto));
        }

        public async Task<List<PostDto>> GetAllUnApprovedPostsFilterByUser(int userId)
        {
            var unApprovedPosts = await GettAllUnApprovedPosts();
            unApprovedPosts = unApprovedPosts.Where(x => x.UserId == userId).ToList();

            return unApprovedPosts;
        }

  
    }
}
