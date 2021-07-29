using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class PlaylistValidator:AbstractValidator<Playlist>
    {
        public PlaylistValidator()
        {
            RuleFor(x => x.VideoList).NotEmpty();
            RuleFor(x => x.VideoList).NotNull();
        }
    }
}