namespace EducationHub.Services
{

    using Microsoft.AspNetCore.Http;

    public interface IImageSharpService
    {
        bool IsValidImage(IFormFile file);
    }
}
