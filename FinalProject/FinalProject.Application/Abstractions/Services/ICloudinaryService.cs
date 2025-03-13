using Microsoft.AspNetCore.Http;

namespace FinalProject.Application.Abstractions.Services
{
    public interface ICloudinaryService
    {
        Task<(string imageUrl, string publicId)> UploadAsync(IFormFile file);
        Task DeleteAsync(string publicId);
    }
}
