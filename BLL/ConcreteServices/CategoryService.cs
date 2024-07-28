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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<PostCategory> _postCategoryRepository;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper, IRepository<PostCategory> postCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _postCategoryRepository = postCategoryRepository;
        }
        public async Task AddCategory(CategoryDto category)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            await _categoryRepository.AddAsync(mappedCategory);
        }

        public async Task DeleteCategory(int categoryId)
        {
            var postCategories = await _postCategoryRepository.GetAllWithIncludes().Where(x => x.CategoryId == categoryId).AsNoTracking().ToListAsync();

            foreach (var item in postCategories)
            {
                await _postCategoryRepository.RemoveAsync(item.CategoryId);
            }
            
            await _categoryRepository.RemoveAsync(categoryId);
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
            return mappedCategories;
        }

        public async Task<CategoryDto> GetByIdCategory(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            var mappedCategory = _mapper.Map<CategoryDto>(category);

            return mappedCategory;
        }

        public async Task UpdateCategory(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<List<PostCategoryDto>> GetAllPostCategoriesByPostId(int postId)
        {
            var allPostCategories = await _postCategoryRepository.GetAllAsync();

            List<PostCategory> postCategories = new();


            foreach (var item in allPostCategories)
                if (item.PostId == postId)
                    postCategories.Add(item);


            var mappedPostCategories = _mapper.Map<List<PostCategoryDto>>(postCategories);
            return mappedPostCategories;
        }

        public async Task<List<PostCategoryDto>> GetAllPostCategories()
        {
            var postCategories = _postCategoryRepository.GetAllWithIncludes(x => x.Category);


            var mappedPostCategories = _mapper.Map<List<PostCategoryDto>>(postCategories.ToList());

            return mappedPostCategories;
        }

    }
}
