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
        [Route("Player/{playerId}")]
        public ActionResult<PlayerDto> GetPlayer(int playerId)
        {
            var model = this.service.GetPlayer(playerId);

            if (model == null) return NotFound();

            return new PlayerDto
            {
                FullName = GetFullName(model.FirstName, model.LastName),
                TeamName = model.TeamName,
            };
        }

        [HttpGet]
        [Route("Coach/{coachId}")]
        public ActionResult<CoachDto> GetCoach(int coachId)
        {
            var model = this.service.GetCoach(coachId);

            if (model == null) return NotFound();

            return new CoachDto
            {
                FullName = GetFullName(model.FirstName, model.LastName),
                TeamName = model.TeamName,
            };
        }

        [HttpGet]
        [Route("Team/{teamId}")]
        public ActionResult<TeamDto> GetTeam(int teamId)
        {
            var model = this.service.GetTeam(teamId);

            if (model == null) return NotFound();

            return new TeamDto
            {
                TeamName = model.Name,
                Players = model.Players.Select(p => GetFullName(p.FirstName, p.LastName)).ToList(),
                CoachName = model.CoachName,
            };
        }

        [HttpPost]
        [Route("Player")]
        public ActionResult PostPlayer(CreatePlayerDto player)
        {
            var model = new PlayerModel
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                TeamId = player.TeamId,
            };

            this.service.AddPlayer(model);

            return Ok();
        }

        [HttpPost]
        [Route("Coach")]
        public ActionResult PostCoach(CreateCoachDto coach)
        {
            var model = new CoachModel
            {
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                TeamId = coach.TeamId,
            };

            this.service.AddCoach(model);

            return Ok();
        }

        [HttpPost]
        [Route("Team")]
        public ActionResult PostTeam(CreateTeamDto team)
        {
            var model = new TeamModel
            {
                Name = team.Name,
            };

            this.service.AddTeam(model);

            return Ok();
        }

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
    }
}
