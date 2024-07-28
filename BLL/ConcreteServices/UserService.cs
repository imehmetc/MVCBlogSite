using AutoMapper;
using BLL.AbstractServices;
using BLL.Dtos;
using DAL.AbstractRepositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ConcreteServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Post> _postRepository;

        public UserService(IRepository<User> userRepository, IMapper mapper, IRepository<Post> postRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> Login(string userName, string password)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
            return _mapper.Map<UserDto>(user);
        }

        public async Task Register(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(user); // Repository'deki AddAsync
        }

        public async Task<List<PostDto>> GetAllPostsByUserId(int userId)
        {
            var posts = _postRepository.GetAllWithIncludes(x => x.User);
            var userPosts = posts.Where(x => x.UserId == userId);

            var mappedPosts = _mapper.Map<List<PostDto>>(userPosts.ToList());

            return mappedPosts;
        }

        public async Task Update(UserDto userDto)
        {
            await _userRepository.UpdateAsync(_mapper.Map<User>(userDto));
        }
        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var mappedUser = _mapper.Map<UserDto>(user);

            return mappedUser;
        }

    }
}
