using MatchDay.RESTApi.DatabaseLayer.Entities;

namespace MatchDay.RESTApi.DatabaseLayer.Interfaces
{
    public interface IMatchDayRepository
    {
        Task<PlayerEntity?> GetPlayer(int id);
        Task<CoachEntity?> GetCoach(int id);
        Task<TeamEntity?> GetTeam(int id);

        Task AddPlayer(PlayerEntity player);
        Task AddCoach(CoachEntity coach);
        Task<int?> AddTeam(TeamEntity team);
    }
}
