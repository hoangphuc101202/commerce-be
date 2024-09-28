﻿using AutoMapper;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Interface;
using System.Text.Json;

namespace Restapi_net8.Services.Implementation
{
    public class CategoriesService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoryRepository categoryRepository, IMapper mapper) { 
            this.categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateCategory(Category category)
        {
            var categoryCreated = await categoryRepository.CreateAsync(category);
            return _mapper.Map<CategoryDTO>(categoryCreated);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var categories = await categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        public async Task<CategoryDTO> GetCategoryById(Guid id)
        {
            var category = await categoryRepository.GetById(id);
            if (category == null)
            {
                return null;
            }
            return _mapper.Map<CategoryDTO>(category);
        }
        public async Task<CategoryDTO> UpdateCategory(Category category, Guid id)
        {
            var categoryToUpdate = await categoryRepository.GetById(id);
            if (categoryToUpdate == null)
            {
                throw new Exception("Category not found");
            }
            var categoryUpdated = await categoryRepository.UpdateAsync(categoryToUpdate.Id, category);
            return _mapper.Map<CategoryDTO>(categoryUpdated);
        }
        public async Task<CategoryDTO> DeleteCategory(Guid id)
        {
            var categoryToDelete = await categoryRepository.GetById(id);
            if (categoryToDelete == null)
            {
                throw new Exception("Category not found");
            }
            var categoryDeleted = await categoryRepository.DeleteAsync(id);
            return _mapper.Map<CategoryDTO>(categoryDeleted);
        }
    }
}
