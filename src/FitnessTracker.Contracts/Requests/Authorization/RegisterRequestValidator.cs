using FluentValidation;

namespace FitnessTracker.Contracts.Requests.Authorization;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).Length(6, 100);
        RuleFor(x => x.ConfirmPassword).NotEmpty();
        RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Username).NotEmpty();
    }
}