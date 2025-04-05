using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlagoDiy.DataAccessLayer.Entites;
using Microsoft.IdentityModel.Tokens;

namespace BlagoDiy.Controllers;

public static class JwtHelper
{
    public static string? GenerateToken(User user)
    {
        if (user == null)
        {
            return null;
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretnotsosecretsecretnotsosecretsecretnotsosecretsecretnotsosecretsecretnotsosecret"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.Name),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(300),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public static User? DecodeToken(string token)
    {

        if (token.Contains("Bearer "))
        {
            token = token.Replace("Bearer ", "");
        }
        
        Console.WriteLine(token);
        
        if (string.IsNullOrEmpty(token))
        {
            throw new SecurityTokenMalformedException("Token is null or empty.");
        }

        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(token))
        {
            throw new SecurityTokenMalformedException("JWT is not well formed. Token is - " + token);
        }

        var jwtToken = handler.ReadJwtToken(token);
    
        var id = int.Parse(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);
        var email = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
        var name = jwtToken.Claims.First(claim => claim.Type == "name").Value;
    
        return new User()
        {
            Id = id,
            Email = email,
            Name = name,
        };
    }
}