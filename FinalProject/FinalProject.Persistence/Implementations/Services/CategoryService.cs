using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Category;
using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository
            , IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDto.Name)) throw new Exception("Alredy Exists");

            Category category = _mapper.Map<Category>(categoryDto);
            category.CreatedAt = DateTime.Now;
            category.ModifiedAt = DateTime.Now;
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _categoryRepository.GetbyIdAsync(id);
            if (category is null)
                throw new Exception("Department Not Found");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Category> categories = await _categoryRepository
                   .GetAll(null, null, false, false, skip: (page - 1) * take, take: take, "Products", "Products.Reviews")
                   .ToListAsync();


            return _mapper.Map<IEnumerable<CategoryItemDto>>(categories);
        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetbyIdAsync(id, "Products", "Products.Reviews");

            if (category is null)
                throw new Exception("Not found");

            return _mapper.Map<GetCategoryDto>(category);
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            Category category = await _categoryRepository.GetbyIdAsync(id);
            if (category is null)
                throw new Exception($"Department with id {id} was not found");

            bool exists = await _categoryRepository
                .AnyAsync(c => c.Name == categoryDto.Name && c.Id != id);
            if (exists)
                throw new Exception($"Department with name '{categoryDto.Name}' already exists");

            _mapper.Map(categoryDto, category);
            category.ModifiedAt = DateTime.Now;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
