using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractServices
{
    public interface ICategoryService
    {
        Task AddCategory(CategoryDto category);
        Task DeleteCategory(int categoryId);
        Task UpdateCategory(int categoryId);
        Task<List<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetByIdCategory(int categoryId);
        Task<List<PostCategoryDto>> GetAllPostCategoriesByPostId(int postId);
        Task<List<PostCategoryDto>> GetAllPostCategories();
    }
}
