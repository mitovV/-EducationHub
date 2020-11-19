namespace EducationHub.Services
{
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        Task<string> ImageUploadAsync(string fileName, byte[] file);
    }
}
