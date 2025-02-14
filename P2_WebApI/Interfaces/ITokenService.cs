using System;

namespace WebApI.Interfaces;

public interface ITokenService
{


 public string CreateToken(string userId, string email, string username );

public string VerifyTokenAndGetId(string token);

}
