using FluentValidation;
using MatchDay.RESTApi.WebLayer.DTOs;

namespace MatchDay.RESTApi.WebLayer.Validators
{
    public class CreateTeamDtoValidator : AbstractValidator<CreateTeamDto>
    {
        public CreateTeamDtoValidator()
        {
            // Name Validation
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team Name is required.");

            // Players Validation
            RuleForEach(x => x.Roster)
                .SetValidator(new CreatePlayerDtoValidator());

            // Coach Validation
            RuleFor(x => x.Coach)
                .SetValidator(new CreateCoachDtoValidator());
        }
    }
}
