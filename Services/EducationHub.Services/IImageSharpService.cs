namespace EducationHub.Services
{
    using Microsoft.AspNetCore.Http;

    public interface IImageSharpService
    {
        bool IsValidExtension(IFormFile file);
    }
}
