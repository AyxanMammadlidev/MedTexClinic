using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetDoctorCommentsAsync(int doctorId);
        Task<Comment> GetByIdWithDetailsAsync(int id);
    }
}
