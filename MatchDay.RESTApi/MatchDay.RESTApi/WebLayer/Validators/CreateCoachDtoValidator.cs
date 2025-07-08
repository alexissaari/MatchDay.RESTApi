using FluentValidation;
using MatchDay.RESTApi.WebLayer.DTOs;

namespace MatchDay.RESTApi.WebLayer.Validators
{
    public class CreateCoachDtoValidator : AbstractValidator<CreateCoachDto>
    {
        public CreateCoachDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Coach's first name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Coach's last name is required.");
        }
    }
}
