﻿
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Edu_QuizGen.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public (string token, int expiresIn) GenrateToken(ApplicationUser user)
    {
        Claim[] Claims = [
            new (JwtRegisteredClaimNames.Sub,user.Id),
            new (JwtRegisteredClaimNames.Email,user.Email!),
            new (JwtRegisteredClaimNames.GivenName,user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName,user.LastName),
            new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            ];


        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expiresin = 30;

        var token = new JwtSecurityToken(
            claims:Claims,
            issuer:_options.Issuer,
            audience:_options.Audience,
            expires:DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
            signingCredentials:signingCredentials
            );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresin);
    }

    public string? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        try 
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer=false,
                ClockSkew=TimeSpan.Zero
            },out SecurityToken validatedToken);

            var jwtToken =(JwtSecurityToken) validatedToken;

        return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        }
        catch 
        {
        return null;    
        }   

    }
}
