using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractServices
{
    public interface IUserService
    {
        Task Register(UserDto userDto);
        Task<UserDto> Login(string userName, string password);
        Task<List<UserDto>> GetAllUsers();
        Task<List<PostDto>> GetAllPostsByUserId(int userId);
        Task Update(UserDto userDto);

        Task<UserDto> GetUserById(int id);
    }
}
