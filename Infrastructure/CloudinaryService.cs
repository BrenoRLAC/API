using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CloudinaryServiceInterface.Infrastructure;


namespace CloudinaryServices.Infrastructure
{   
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
       
        public CloudinaryService(IConfiguration configuration)
        {

            var cloudName = configuration["Cloudinary:cloud_name"];
            var apiKey = configuration["Cloudinary:api_key"];
            var apiSecret = configuration["Cloudinary:api_secret"];


            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);

        }

        public List<ImageUploadResult> UploadImages(List<IFormFile> files)
        {
            var results = new List<ImageUploadResult>();

            foreach (var file in files)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult != null)
                {
                    results.Add(new ImageUploadResult
                    {
                        PublicId = uploadResult.PublicId,
                        SecureUrl = uploadResult.SecureUrl
                    });
                }
            }

            return results;
        }

    }
}
