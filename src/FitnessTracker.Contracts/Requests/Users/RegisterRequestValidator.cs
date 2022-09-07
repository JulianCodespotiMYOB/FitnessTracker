using FluentValidation;

namespace FitnessTracker.Contracts.Requests.Users;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).Length(6, 100);
        RuleFor(x => x.BenchPressMax).GreaterThan(0).When(x => x.BenchPressMax.HasValue);
        RuleFor(x => x.SquatMax).GreaterThan(0).When(x => x.SquatMax.HasValue);
        RuleFor(x => x.DeadliftMax).GreaterThan(0).When(x => x.DeadliftMax.HasValue);
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.BuddyName).NotEmpty();
    }
}