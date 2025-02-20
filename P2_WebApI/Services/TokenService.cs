
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApI.Interfaces;



public class TokenService : ITokenService   // inheritance 

{
    private readonly string _secretKey;     // private feild 
    public TokenService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("SecretKey is not configured.");
    }

    public string CreateToken(string userId, string email, string username , int time )
    {
        var tokenHandler = new JwtSecurityTokenHandler();   // intializing new instance of  JwtSecurityTokenHandler

        var key = Encoding.ASCII.GetBytes(_secretKey);  /// secret in ascii format 

        var payload = new SecurityTokenDescriptor      // creation of payload
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username)
            ]),
            
            Expires = DateTime.UtcNow.AddMinutes(time),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(payload);     // creation of token 
        return tokenHandler.WriteToken(token);        // returning of token 
    }



    public Guid VerifyTokenAndGetId(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_secretKey);

        
            var validationParameters = new TokenValidationParameters   // 
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

           
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim != null)
            {
                return Guid.Parse(userIdClaim.Value) ; 
            }
            else
            {
                throw new Exception("User ID not found in token.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Token validation failed: " + ex.Message);
        }
    }


}

