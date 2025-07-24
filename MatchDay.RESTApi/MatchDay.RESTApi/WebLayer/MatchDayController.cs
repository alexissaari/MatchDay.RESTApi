using FluentValidation;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.WebLayer.DTOs;
using MatchDay.RESTApi.WebLayer.Mappers;
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
        [Route("Teams")]
        public async Task<IResult> GetTeams()
        {
            var result = await this.service.GetTeams();
            if (!result.IsSuccess)
            {
                return ProblemExtensions.ResultToProblem(result);
            }

            IList<TeamModel> models = (List<TeamModel>)result.SuccessResult;

            var responseDtos = models.Select(team => new GetTeamResponseDto
            {
                TeamName = team.Name ?? string.Empty,
                Roster = team.Players?.Select(p => GetFullName(p.FirstName, p.LastName)).ToList() ?? new List<string>(),
                CoachName = team.Coach == null ? string.Empty : GetFullName(team.Coach.FirstName, team.Coach.LastName),
            }).ToList();

            return Results.Ok(responseDtos);
        }

        [HttpGet]
        [Route("Teams/{teamId}")]
        public async Task<IResult> GetTeam(int teamId, GetTeamDtoValidator validator)
        {
            var validationResults = validator.Validate(teamId);
            if (!validationResults.IsValid)
            {
                return ProblemExtensions.ValidationResultToHttpValidationProblemDetails(validationResults);
            }

            var result = await this.service.GetTeam(teamId);
            if (!result.IsSuccess)
            {
                return ProblemExtensions.ResultToProblem(result);
            }

            var model = (TeamModel)result.SuccessResult;
            return Results.Ok(new GetTeamResponseDto
            {
                TeamName = model.Name ?? string.Empty,
                Roster = model.Players?.Select(p => GetFullName(p.FirstName, p.LastName)).ToList() ?? new List<string>(),
                CoachName = model.Coach == null ? string.Empty : GetFullName(model.Coach.FirstName, model.Coach.LastName),
            });
        }

        [HttpPost]
        [Route("Teams")]
        public async Task<IResult> PostTeam(CreateTeamDto request, CreateTeamDtoValidator validator)
        {
            if (request == null)
            {
                return Results.BadRequest("Request body cannot be null.");
            }

            var validationResults = validator.Validate(request);

            if (!validationResults.IsValid)
            {
                return ProblemExtensions.ValidationResultToHttpValidationProblemDetails(validationResults);
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
                    LastName = p.LastName,
                }).ToList(),
            };

            var result = await this.service.CreateTeam(model);
            if (!result.IsSuccess)
            {
                return ProblemExtensions.ResultToProblem(result);
            }

            var teamId = (int)result.SuccessResult;

            return Results.Created("", teamId);
        }

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
    }
}
