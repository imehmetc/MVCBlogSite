using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractServices
{
    public interface IPostLikeService
    {
        Task LikePost(int userId, int postId);
        Task UnLikePost(int userId, int postId);
        Task<List<PostLikeDto>> GetAllPostLikes();
    }
}
