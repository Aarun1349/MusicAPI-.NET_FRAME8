using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.DataProtection;

namespace MusicAPI.Helpers
{


    public static class CloudinaryService
    {

        private static string CloudName = "diegai7ax";
        private static string ApiKey = "363439996596414";
        private static string ApiSecret = "Qu1hRI5438fSZ44BD69sbc5T9jU";
      
        
        // Method to upload an image to Cloudinary
        public static async Task<string> CloudinaryImageUpload(IFormFile file)
        {
            Account account = new Account(CloudName, ApiKey, ApiSecret);
            Cloudinary cloudinary = new Cloudinary(account);
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.AbsoluteUri; // Returns the Cloudinary image URL
                }
                else
                {
                    throw new Exception("File upload failed: " + uploadResult.Error.Message);
                }
            }
        }

        // Method to upload an audio file to Cloudinary
        public static async Task<string> CloudinaryAudioUpload(IFormFile file)
        {

            Account account = new Account(CloudName, ApiKey, ApiSecret);
            Cloudinary cloudinary = new Cloudinary(account);
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.AbsoluteUri; // Returns the Cloudinary Audio file URL
                }
                else
                {
                    throw new Exception("File upload failed: " + uploadResult.Error.Message);
                }
            }
        }
    }
}