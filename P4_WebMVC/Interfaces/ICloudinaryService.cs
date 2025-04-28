using System;

namespace P4_WebMVC.Interfaces;

public interface ICloudinaryService
{


public Task <string> UploadImageAsync (IFormFile image , string folder);

public Task <string> UploadVideoAsync(IFormFile video , string folder);


}
