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


    public async Task<string> UploadImageAsync(IFormFile image, string folder)
    {
        if (image == null || image.Length == 0)
        {
            throw new ArgumentNullException("File is null");
        }

        using var stream = image.OpenReadStream();


        var uploadParams = new ImageUploadParams()
        {

            File = new FileDescription(image.FileName, stream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true,
            Folder = folder
        };

        var uploadResult = await cloudinary.UploadAsync(uploadParams);

        return uploadResult.SecureUrl.ToString();



    }

    public async Task<string> UploadVideoAsync(IFormFile video, string folder)
    {
        try
        {
            if (video == null || video.Length == 0)
            {
                throw new ArgumentNullException("File is null");
            }

            using var stream = video.OpenReadStream();

            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(video.FileName, stream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                Folder = folder

            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();


        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
