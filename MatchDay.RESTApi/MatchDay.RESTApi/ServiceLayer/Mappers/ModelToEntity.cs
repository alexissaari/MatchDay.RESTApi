using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Mappers
{
    public static class ModelToEntity
    {
        public static PlayerEntity ToEntity(PlayerModel model)
        {
            return new PlayerEntity
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                TeamId = model.TeamId,
            };
        }
    }
}
