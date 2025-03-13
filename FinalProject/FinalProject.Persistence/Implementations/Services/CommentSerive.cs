using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Comment;
using FinalProject.Domain.Entities;
using SendGrid.Helpers.Errors.Model;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class CommentSerive : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;


        public CommentSerive(ICommentRepository commentRepository
            , IDoctorRepository doctorRepository
            , IMapper mapper
           )
        {
            _commentRepository = commentRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        public async Task CreateCommentAsync(string userId, CreateCommentDto commentDto)
        {
            if (!await _doctorRepository.AnyAsync(d => d.Id == commentDto.DoctorId))
                throw new Exception("doctor does not exists");
            Comment comment = _mapper.Map<Comment>(commentDto);
            comment.UserId = userId;
            comment.CreatedAt = DateTime.Now;
            comment.ModifiedAt = DateTime.Now;
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

        }

        public async Task DeleteCommentAsync(int id)
        {
            Comment comment = await _commentRepository.GetbyIdAsync(id);
            if (comment is null)
                throw new Exception("Comment Not Found");

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task<GetCommentDto> GetCommentByIdAsync(int id)
        {
            Comment comment = await _commentRepository.GetByIdWithDetailsAsync(id);

            if (comment is null)
                throw new NotFoundException("Comment not found");

            return _mapper.Map<GetCommentDto>(comment);
        }

        public async Task<IEnumerable<CommentItemDto>> GetDoctorCommentsAsync(int doctorId)
        {
            IEnumerable<Comment> comments = await _commentRepository.GetDoctorCommentsAsync(doctorId);
            return _mapper.Map<IEnumerable<CommentItemDto>>(comments);
        }

        public async Task UpdateCommentAsync(int id, UpdateCommentDto commentDto)
        {
            Comment comment = await _commentRepository.GetbyIdAsync(id);

            if (comment is null)
                throw new Exception("Comment does not exists");

            _mapper.Map(commentDto, comment);

            _commentRepository.Update(comment);

            await _commentRepository.SaveChangesAsync();
        }
    }
}
