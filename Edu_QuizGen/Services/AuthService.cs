using Edu_QuizGen.Contracts.Authentication;
using System.Security.Cryptography;

namespace Edu_QuizGen.Services;

public class AuthService(UserManager<ApplicationUser> userManager ,IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly int _refreshTokenExpiryDays = 14;


    public async Task<AuthResponse?> GetTokenAsync(string email, string passowrd, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return null;

        var isValid = await _userManager.CheckPasswordAsync(user, passowrd);    
        
        if(!isValid)
            return null;

        var (token, expiresin) = _jwtProvider.GenrateToken(user);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpirydays = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token=refreshToken,
            ExpiresOn=refreshTokenExpirydays
        });
        await _userManager.UpdateAsync(user);

        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresin,refreshToken,refreshTokenExpirydays);
    }


    public async Task<AuthResponse> GetRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return null;

        var user =await _userManager.FindByIdAsync(userId);

        if (user is null)
            return null;

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.isActive);

        if (userRefreshToken is null)
            return null;

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var (Token, expiresin) = _jwtProvider.GenrateToken(user);

        var newRefreshToken =GenerateRefreshToken();
        var newRefreshTokenExpirydays = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays); 

        user.RefreshTokens.Add(new RefreshToken { 
        Token =newRefreshToken,
        ExpiresOn =newRefreshTokenExpirydays  
        });

      await  _userManager.UpdateAsync(user); 
    
        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, Token, expiresin,newRefreshToken,newRefreshTokenExpirydays);

    }

    public async Task<bool> RevokeRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return false;

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return false;

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.isActive);

        if (userRefreshToken is null)
            return false;

        userRefreshToken.RevokedOn = DateTime.UtcNow;


        await _userManager.UpdateAsync(user);

        return true;        
    }


    private string GenerateRefreshToken() =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));


}
