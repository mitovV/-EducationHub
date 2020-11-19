namespace EducationHub.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> ImageUploadAsync(IFormFile file)
        {
            byte[] destinationImage;

            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            destinationImage = memoryStream.ToArray();

            using var destinationStream = new MemoryStream(destinationImage);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, destinationStream),
            };

            var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

            return uploadResult.Uri.AbsoluteUri;
        }
    }
}
