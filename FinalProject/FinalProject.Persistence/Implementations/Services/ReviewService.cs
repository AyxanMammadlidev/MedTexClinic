using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.ProductReview;
using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository,
            IProductRepository productRepository
            , IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(string userId, CreateReviewDto reviewDto)
        {
            if (!await _productRepository.AnyAsync(p => p.Id == reviewDto.ProductId))
                throw new Exception("Product does not exists");
            Review review = _mapper.Map<Review>(reviewDto);
            review.UserId = userId;
            review.CreatedAt = DateTime.Now;
            review.ModifiedAt = DateTime.Now;
            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            Review review = await _reviewRepository.GetbyIdAsync(id);
            if (review is null)
                throw new NotFoundException($"Review with ID {id} not found.");

            _reviewRepository.Delete(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Review> reviews = await _reviewRepository.GetAll(skip: (page - 1) * take, take: take)
                  .ToListAsync();
            ;
            return _mapper.Map<IEnumerable<ReviewItemDto>>(reviews);
        }

        public async Task<GetReviewDto> GetByIdAsync(int id)
        {
            Review review = await _reviewRepository.GetbyIdAsync(id);
            if (review is null)
                throw new NotFoundException($"Review with ID {id} not found.");

            return _mapper.Map<GetReviewDto>(review);
        }

        public async Task UpdateAsync(int id, UpdateReviewDto reviewDto)
        {
            Review review = await _reviewRepository.GetbyIdAsync(id);
            if (review is null)
                throw new NotFoundException($"Review with ID {id} not found.");

            _mapper.Map(reviewDto, review);
            review.ModifiedAt = DateTime.Now;
            _reviewRepository.Update(review);
            await _reviewRepository.SaveChangesAsync();
        }
    }
}
