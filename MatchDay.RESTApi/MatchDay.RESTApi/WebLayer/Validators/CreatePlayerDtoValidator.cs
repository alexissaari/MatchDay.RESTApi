using FluentValidation;
using MatchDay.RESTApi.WebLayer.DTOs;

namespace MatchDay.RESTApi.WebLayer.Validators
{
    public class CreatePlayerDtoValidator : AbstractValidator<CreatePlayerDto>
    {
        public CreatePlayerDtoValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Player's first name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Player's last name is required.");
        }
    }
}
