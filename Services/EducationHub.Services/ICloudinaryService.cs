namespace EducationHub.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> ImageUploadAsync(IFormFile file);
    }
}
