using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class VideoValidator:AbstractValidator<Video>
    {
        public VideoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).NotNull();
            RuleFor(x => x.Title).MinimumLength(5);
            RuleFor(x => x.Title).MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Description).NotNull();
            RuleFor(x => x.Description).MinimumLength(25);
            RuleFor(x => x.Description).MaximumLength(250);
            RuleFor(x => x.Extension).NotNull();
            RuleFor(x => x.Extension).NotEmpty();
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.Path).NotNull();
        }
        //TODO Validation Exception Messages will be added.
    }
}