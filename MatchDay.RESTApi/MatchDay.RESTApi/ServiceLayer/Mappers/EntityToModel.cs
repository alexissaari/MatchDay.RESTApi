using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Mappers
{
    public static class EntityToModel
    {
        public static PlayerModel ToModel(PlayerEntity entity)
        {
            return new PlayerModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                TeamId = entity.TeamId,
                TeamName = entity.Team.Name,
            };
        }
    }
}
