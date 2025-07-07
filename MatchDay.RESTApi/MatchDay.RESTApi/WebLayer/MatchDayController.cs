using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.WebLayer.DTOs;
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
        public ActionResult<GetTeamResponseDto> GetTeam(int teamId)
        {
            var model = this.service.GetTeam(teamId);

            if (model == null) return NotFound();

            return new GetTeamResponseDto
            {
                TeamName = model.Name ?? string.Empty,
                Roster = model.Players?.Select(p => GetFullName(p.FirstName, p.LastName)).ToList() ?? new List<string>(),
                CoachName = model.Coach == null ? string.Empty : GetFullName(model.Coach.FirstName, model.Coach.LastName),
            };
        }

        [HttpPost]
        [Route("Team")]
        public ActionResult PostTeam(CreateTeamDto team)
        {
            var model = new TeamModel
            {
                Name = team.Name,
                Coach = new CoachModel()
                {
                    FirstName = team.Coach?.FirstName ?? string.Empty,
                    LastName = team.Coach?.LastName ?? string.Empty,
                },
                Players = team.Roster == null ? null : team.Roster?.Select(p => new PlayerModel
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList(),
            };

            this.service.CreateTeam(model);

            return Ok();
        }

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
    }
}
