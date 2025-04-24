using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using P4_WebMVC.Interfaces;

namespace P4_WebMVC.Services;

public class CloudinaryService : ICloudinaryService
{


    private readonly Cloudinary cloudinary;

    public CloudinaryService(IConfiguration configuration)
    {
        var cloudinaryUrl = configuration["Cloudinary:CLOUDINARY_URL"]
         ?? throw new ArgumentNullException("Cloudinary Url is not set!");

        this.cloudinary = new Cloudinary(cloudinaryUrl);
        cloudinary.Api.Secure = true;
    }


    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentNullException("File is null");
        }

        using var stream = file.OpenReadStream();


        var  uploadParams = new ImageUploadParams(){

            File = new FileDescription(file.FileName , stream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true
        };

       var uploadResult =  await cloudinary.UploadAsync(uploadParams);

        return uploadResult.SecureUrl.ToString();



    }
}
