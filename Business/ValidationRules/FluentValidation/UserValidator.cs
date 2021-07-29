using System.Reflection;
using Core.Entities.Concrete;
using FluentValidation;
using FluentValidation.Internal;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.FirstName).MinimumLength(2);
            RuleFor(x => x.FirstName).MaximumLength(15);
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.LastName).MinimumLength(2);
            RuleFor(x => x.LastName).MaximumLength(15);
            RuleFor(x => x.Email).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        }
    }
}