using MatchDay.RESTApi.DatabaseLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Interfaces;
using MatchDay.RESTApi.ServiceLayer.Mappers;
using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer
{
    public class MatchDayService : IMatchDayService
    {
        IMatchDayRepository repository;

        public MatchDayService(IMatchDayRepository repository)
        {
            this.repository = repository;
        }

        public PlayerModel GetPlayer(int id)
        {
            var entity = this.repository.GetPlayer(id);
            return EntityToModel.ToModel(entity);
        }
    }
}
