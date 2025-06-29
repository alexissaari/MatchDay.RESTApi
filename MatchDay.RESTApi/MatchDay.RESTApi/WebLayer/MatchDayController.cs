using MatchDay.RESTApi.ServiceLayer.Interfaces;
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

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
    }
}
