using FluentValidation;
using MatchDay.RESTApi.WebLayer.DTOs;

namespace MatchDay.RESTApi.WebLayer.Validators
{
    public class GetTeamDtoValidator : AbstractValidator<int>
    {
        public GetTeamDtoValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .NotEmpty()
                .WithMessage("TeamId is required.");

            RuleFor(x => x)
                .GreaterThan(0)
                .WithMessage("TeamId must be a positive number.");
        }
    }
}
