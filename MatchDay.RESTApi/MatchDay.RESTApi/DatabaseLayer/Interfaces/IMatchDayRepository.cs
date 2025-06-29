using MatchDay.RESTApi.DatabaseLayer.Entities;

namespace MatchDay.RESTApi.DatabaseLayer.Interfaces
{
    public interface IMatchDayRepository
    {
        PlayerEntity? GetPlayer(int id);
        void CreatePlayer(PlayerEntity player);
        void CreateTeam(TeamEntity team);
    }
}
