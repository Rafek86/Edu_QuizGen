using FluentValidation;

namespace Edu_QuizGen.Contracts.Authentication;

public class LoginRequestValidator :AbstractValidator<LoginRequest>
{
    public LoginRequestValidator() { 
   
        RuleFor(x=>x.email).EmailAddress().NotEmpty(); 
        
        RuleFor(x=>x.password).NotEmpty(); 
    
    }
}
