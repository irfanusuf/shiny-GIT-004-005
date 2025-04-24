using System;

namespace P4_WebMVC.Interfaces;

public interface ICloudinaryService
{


public Task <string> UploadImageAsync (IFormFile file);


}
