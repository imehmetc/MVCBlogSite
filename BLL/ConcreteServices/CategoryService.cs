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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task AddCategory(CategoryDto category)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            await _categoryRepository.AddAsync(mappedCategory);
        }

        public async Task DeleteCategory(int categoryId)
        {
            await _categoryRepository.DeleteAsync(categoryId);
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
    }
}
