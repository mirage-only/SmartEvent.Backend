using FluentValidation;
using SmartEvent.Backend.Application.DTOs.UserDTOs.Requests;

namespace SmartEvent.Backend.Application.Common.Validators;

public class RegisterUserRequestDtoValidator: AbstractValidator<RegisterUserRequestDto>
{
    public RegisterUserRequestDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Incorrect email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must have at least 8 characters");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required");
    }
}