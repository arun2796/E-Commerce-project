using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.InputParams;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;

namespace Application.Common
{
    public class ImageServicesC 
    {
        private readonly Cloudinary _cloudinary;

        public ImageServicesC(IOptions<CloudinarySetting> options)
        {
            var account = new Account
            {
                Cloud = options.Value.CloudName,
                ApiKey = options.Value.ApiKey,
                ApiSecret = options.Value.ApiSecret,
            };
            _cloudinary=new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddProductImage(IFormFile file)
        {
            var uploadResult=new ImageUploadResult();

            if (file!=null && file.Length > 0)
            {
                using var reader = file.OpenReadStream();
                var path=new FileDescription(file.Name,reader);
                var uploadParams = new ImageUploadParams
                {
                    File = path,
                    Folder="main_project"
                };
                uploadResult=await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
        public async Task<DeletionResult> DeleteProductImage(string PublicId)
        {
            var deleteResult=new DeletionParams (PublicId);

            var result =await _cloudinary.DestroyAsync(deleteResult);

            return result;


        }
    }
}
