using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Interfaces
{
    public interface IMatchDayService
    {
        Task<PlayerModel?> GetPlayer(int id);
        Task<CoachModel?> GetCoach(int id);
        Task<TeamModel?> GetTeam(int id);

        Task AddPlayer(PlayerModel player);
        Task AddCoach(CoachModel coach);
        Task CreateTeam(TeamModel team);
    }
}
