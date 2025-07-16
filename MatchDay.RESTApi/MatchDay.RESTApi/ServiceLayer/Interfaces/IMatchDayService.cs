using MatchDay.RESTApi.ServiceLayer.Models;
using MatchDay.RESTApi.ServiceLayer.Results;

namespace MatchDay.RESTApi.ServiceLayer.Interfaces
{
    public interface IMatchDayService
    {
        Task<Result> GetTeam(int id);
        Task<int> CreateTeam(TeamModel team);
    }
}
