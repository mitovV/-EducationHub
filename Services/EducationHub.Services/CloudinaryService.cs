namespace EducationHub.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> ImageUploadAsync(string fileName, byte[] file)
        {
            using var destinationStream = new MemoryStream(file);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, destinationStream),
            };

            var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

            return uploadResult.Uri.AbsoluteUri;
        }
    }
}
