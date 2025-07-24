using MatchDay.RESTApi.DatabaseLayer.Entities;

namespace MatchDay.RESTApi.DatabaseLayer.Interfaces
{
    public interface IMatchDayRepository
    {
        Task<IList<TeamEntity>> GetTeams();
        Task<TeamEntity?> GetTeam(int id);
        Task<TeamEntity?> GetTeam(string name);
        Task<int?> AddTeam(TeamEntity team);
    }
}
