using System;

namespace P4_WebMVC.Interfaces;

public interface ITokenService
{


 public string CreateToken(Guid userId, string email, string username , int time);

public Guid VerifyTokenAndGetId(string token);

}
