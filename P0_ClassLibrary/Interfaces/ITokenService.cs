using System;

namespace P0_ClassLibrary.Interfaces;

public interface ITokenService
{


    public string CreateToken(Guid userId, string email, string username, int timeInDays);

    public string CreateToken(string userId, string email, string username, int timeInDays);

    public string VerifyTokenAndGetId(string token);

    // public string VerifyTokenAndGetId(string token);

}
