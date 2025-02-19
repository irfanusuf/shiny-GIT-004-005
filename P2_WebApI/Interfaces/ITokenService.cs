using System;

namespace WebApI.Interfaces;

public interface ITokenService
{


 public string CreateToken(string userId, string email, string username , int time);

public string VerifyTokenAndGetId(string token);

}
