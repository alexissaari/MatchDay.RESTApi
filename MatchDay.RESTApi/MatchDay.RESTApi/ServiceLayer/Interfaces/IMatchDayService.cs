using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Interfaces
{
    public interface IMatchDayService
    {
        PlayerModel? GetPlayer(int id);
        CoachModel? GetCoach(int id);
        TeamModel? GetTeam(int id);

        void AddPlayer(PlayerModel player);
        void AddCoach(CoachModel coach);
        void CreateTeam(TeamModel team);
    }
}
