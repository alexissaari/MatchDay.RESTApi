using FluentValidation;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.WebLayer.DTOs;
using MatchDay.RESTApi.WebLayer.Validators;
using Microsoft.AspNetCore.Mvc;

namespace MatchDay.RESTApi.WebLayer
{
    [ApiController]
    [Route("[controller]")]
    public class MatchDayController : ControllerBase
    {
        IMatchDayService service;

        public MatchDayController(IMatchDayService service) 
        {
            this.service = service;
        }

        [HttpGet]
        [Route("Team/{teamId}")]
        public async Task<IResult> GetTeam(int teamId, GetTeamDtoValidator validator)
        {
            var validationResults = validator.Validate(teamId);

            if (!validationResults.IsValid)
            {
                var problemDetails = FormatProblemDetails(validationResults);
                return Results.Problem(problemDetails);
            }

            var model = await this.service.GetTeam(teamId);

            if (model == null)
            {
                return Results.NotFound($"No Team found with teamId = {teamId}");
            }

            return Results.Ok(new GetTeamResponseDto
            {
                TeamName = model.Name ?? string.Empty,
                Roster = model.Players?.Select(p => GetFullName(p.FirstName, p.LastName)).ToList() ?? new List<string>(),
                CoachName = model.Coach == null ? string.Empty : GetFullName(model.Coach.FirstName, model.Coach.LastName),
            });
        }

        [HttpPost]
        [Route("Team")]
        public async Task<IResult> PostTeam(CreateTeamDto request, IValidator<CreateTeamDto> validator)
        {
            var validationResults = validator.Validate(request);

            if (!validationResults.IsValid)
            {
                var problemDetails = FormatProblemDetails(validationResults);
                return Results.Problem(problemDetails);
            }

            var model = new TeamModel
            {
                Name = request.Name,
                Coach = new CoachModel()
                {
                    FirstName = request.Coach?.FirstName ?? string.Empty,
                    LastName = request.Coach?.LastName ?? string.Empty,
                },
                Players = request.Roster == null ? null : request.Roster?.Select(p => new PlayerModel
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList(),
            };
            
            int teamId = await this.service.CreateTeam(model);

            return Results.Ok($"New Team entry successfully created! TeamId = {teamId}");
        }

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }

        private HttpValidationProblemDetails FormatProblemDetails(FluentValidation.Results.ValidationResult validationResults)
        {
            return new HttpValidationProblemDetails(validationResults.ToDictionary())
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Failed",
                Detail = "One or more validation errors occured."
            };
        }
    }
}
