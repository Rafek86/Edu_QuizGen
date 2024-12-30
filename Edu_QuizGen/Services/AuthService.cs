using Edu_QuizGen.Errors;
using Edu_QuizGen.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Edu_QuizGen.Services;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor,
    IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly int _refreshTokenExpiryDays = 14;


    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string passowrd, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var result = await _signInManager.PasswordSignInAsync(user, passowrd, false, false);

        if (result.Succeeded)
        {
            var (token, expiresin) = _jwtProvider.GenrateToken(user);

            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpirydays = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpirydays
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id, user.Email!, user.FirstName, user.LastName, token, expiresin, refreshToken, refreshTokenExpirydays);

            return Result.Success<AuthResponse>(response);
        }

        return Result.Failure<AuthResponse>(result.IsNotAllowed? UserErrors.EmailNotConfirmed:UserErrors.InvalidCredentials);
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

        var user =await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.isActive);

        if (userRefreshToken is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);


        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var (Token, expiresin) = _jwtProvider.GenrateToken(user);

        var newRefreshToken =GenerateRefreshToken();
        var newRefreshTokenExpirydays = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays); 

        user.RefreshTokens.Add(new RefreshToken { 
        Token =newRefreshToken,
        ExpiresOn =newRefreshTokenExpirydays  
        });

        await _userManager.UpdateAsync(user); 
    
        var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, Token, expiresin, newRefreshToken, newRefreshTokenExpirydays);
      
        return Result.Success(response);    

    }

    public async Task<Result> RevokeRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.isActive);

        if (userRefreshToken is null)
            return Result.Failure(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;


        await _userManager.UpdateAsync(user);

        return Result.Success();        
    }

    public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken) 
    {
        var emailExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email);

        if (emailExist)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var user = new ApplicationUser {
            
        Email=request.Email,
        UserName=request.Email,
        FirstName=request.FirstName,
        LastName=request.LastName,

        };

        var result = await _userManager.CreateAsync(user,request.Password);

        if (result.Succeeded) {
            #region Before
            //    var (token, expirin) = _jwtProvider.GenrateToken(user);
            //    var refreshToken = GenerateRefreshToken();
            //    var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            //    user.RefreshTokens.Add(new RefreshToken
            //    {
            //        Token = refreshToken,
            //        ExpiresOn = refreshTokenExpiration
            //    });
            //    await _userManager.UpdateAsync(user);

            //    var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expirin, refreshToken, refreshTokenExpiration);

            //    return Result.Success(response); 
            #endregion
            var code =await _userManager.GenerateEmailConfirmationTokenAsync(user); 
            code=WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //TODO:: SendEmail 
            await SendConfirmationEmail(user, code);

            //Just in the DEV Level
            return Result.Success(code);    
        }

        
         var error = result.Errors.First();
        
         return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }   
        
    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken) 
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        
        if (user is null)
            return Result.Failure(UserErrors.InvalidCode);

        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfirmation);


        var code = request.Code;

        try
        { 
            code=Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));    
        }
        catch 
        {
            return Result.Failure(UserErrors.InvalidCode);  
        }

        var result =await _userManager.ConfirmEmailAsync(user, code);   

        if (result.Succeeded) { 
            return Result.Success();    
        }

         var error = result.Errors.First();
        
         return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
    
    public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request) 
    {

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Success();

        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfirmation);
        
        var code =await _userManager.GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //TODO:: SendEmail 
        await SendConfirmationEmail(user, code);

        return Result.Success(code);    
    }

    public async Task<Result> SendResetPasswordCodeAsync(string email) { 
    
        if(await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Success();

        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        code =WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); 

        await SendResetPasswordEmail(user, code);   

        return Result.Success(code);
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request) {

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
            return Result.Failure(UserErrors.InvalidCode);

        IdentityResult result;

        try {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
        }
        catch (FormatException ex) 
        {
            result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());   
        }

        if (result == IdentityResult.Success) 
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code,error.Description,StatusCodes.Status401Unauthorized));
    }

    private async Task SendConfirmationEmail(ApplicationUser user,string code) {

        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
            new Dictionary<string, string> {
                    {"{{name}}",user.FirstName },
                    { "{{action_url}}", $"{origin}/Auth/confirm-email?UserId={user.Id}&code={code}" }
            }
            );
        await _emailSender.SendEmailAsync(user.Email, "Confirm Email by Quiz_Gen_Team✅", emailBody);
    }

    private async Task SendResetPasswordEmail(ApplicationUser user,string code) {

        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
            new Dictionary<string, string> {
                    {"{{name}}",user.FirstName },
                    { "{{action_url}}", $"{origin}/Auth/forgetPassword?email={user.Email}&code={code}" }
            }
            );
        await _emailSender.SendEmailAsync(user.Email, "Change Password ‼️", emailBody);
    }
    private string GenerateRefreshToken() =>
         Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));


}
