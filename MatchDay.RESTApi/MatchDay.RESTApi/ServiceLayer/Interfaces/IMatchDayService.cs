using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Interfaces
{
    public interface IMatchDayService
    {
        PlayerModel GetPlayer(int id);
    }
}
