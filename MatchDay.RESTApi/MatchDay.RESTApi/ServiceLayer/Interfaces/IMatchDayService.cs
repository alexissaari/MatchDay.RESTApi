using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Interfaces
{
    public interface IMatchDayService
    {
        Task<TeamModel?> GetTeam(int id);
        Task<int> CreateTeam(TeamModel team);
    }
}
