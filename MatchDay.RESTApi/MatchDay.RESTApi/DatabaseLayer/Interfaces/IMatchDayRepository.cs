using MatchDay.RESTApi.DatabaseLayer.Entities;

namespace MatchDay.RESTApi.DatabaseLayer.Interfaces
{
    public interface IMatchDayRepository
    {
        PlayerEntity? GetPlayer(int id);
        CoachEntity? GetCoach(int id);
        TeamEntity? GetTeam(int id);

        void AddPlayer(PlayerEntity player);
        void AddCoach(CoachEntity coach);
        void AddTeam(TeamEntity team);
    }
}
