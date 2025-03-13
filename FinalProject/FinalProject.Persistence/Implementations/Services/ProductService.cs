using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Product;
using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewService _reviewService;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(IProductRepository productRepository,
            ICategoryRepository categoryService,
            IMapper mapper,
            IReviewService reviewService,
            ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryService;
            _reviewService = reviewService;
            _cloudinaryService = cloudinaryService;
        }
        public async Task CreateAsync(CreateProductDto productDto)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception("Category does not exists");

            (string imageUrl, string publicId) = await _cloudinaryService.UploadAsync(productDto.Photo);
            Product product = _mapper.Map<Product>(productDto);
            product.ImageUrl = imageUrl;
            product.ImagePublicId = publicId;
            product.CreatedAt = DateTime.Now;
            product.ModifiedAt = DateTime.Now;
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _productRepository.GetbyIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product with ID {id} not found.");

            if (!string.IsNullOrEmpty(product.ImagePublicId))
                await _cloudinaryService.DeleteAsync(product.ImagePublicId);

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<ICollection<ProductItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Product> products = await _productRepository.GetAll(null, null, false, false, skip: (page - 1) * take, take: take, "Reviews")
            .ToListAsync(); ;


            return _mapper.Map<ICollection<ProductItemDto>>(products);
        }

        public async Task<GetProductDto> GetByIdAsync(int id)
        {
            Product product = await _productRepository.GetbyIdAsync(id, "Reviews");
            if (product is null)
                throw new NotFoundException($"Product with ID {id} not found.");

            return _mapper.Map<GetProductDto>(product);
        }

        public async Task UpdateAsync(int id, UpdateProductDto productDto)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception("Category does not exists");
            Product product = await _productRepository.GetbyIdAsync(id);
            if (product is null)
                throw new NotFoundException($"Product with ID {id} not found.");
            if (!string.IsNullOrEmpty(product.ImagePublicId))//There is some products without image. This statement is Unnecessary at production enviroment
                await _cloudinaryService.DeleteAsync(product.ImagePublicId);
            (string imageUrl, string publicId) = await _cloudinaryService.UploadAsync(productDto.Photo);
            _mapper.Map(productDto, product);

            product.ImageUrl = imageUrl;
            product.ImagePublicId = publicId;
            product.ModifiedAt = DateTime.Now;
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }


        public async Task<IEnumerable<GetProductDto>> FilterByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetProductsByPriceRange(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }



        public async Task<IEnumerable<GetProductDto>> GetProductsByPriceDescending(int page, int take)
        {
            var products = await _productRepository.GetProductsByPriceDescending(page, take);

            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }

        public async Task<IEnumerable<GetProductDto>> GetProductsByPriceAscending(int page, int take)
        {
            var products = await _productRepository.GetProductsByPriceAscending(page, take);

            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }

        public async Task<IEnumerable<GetProductDto>> GetProductsByRating(int page, int take)
        {
            var products = await _productRepository.GetProductsByRating(page, take);

            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }
    }
}
