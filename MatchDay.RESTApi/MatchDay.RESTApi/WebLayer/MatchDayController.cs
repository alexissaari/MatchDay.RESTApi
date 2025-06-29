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

        [HttpGet(Name = "GetPlayers")]
        public PlayerResponse Get(int id)
        {
            var model = this.service.GetPlayer(id);

            return new PlayerResponse
            {
                FullName = GetFullName(model.FirstName, model.LastName),
                TeamName = model.TeamName,
            };
        }

        [HttpPost]
        [Route("Player")]
        public ActionResult Post(CreatePlayer player)
        {
            var model = new PlayerModel
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                TeamId = player.TeamId,
            };

            this.service.CreatePlayer(model);

            return Ok();
        }

        [HttpPost]
        [Route("Team")]
        public ActionResult Post(CreateTeam team)
        {
            var model = new TeamModel
            {
                Name = team.Name,
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
