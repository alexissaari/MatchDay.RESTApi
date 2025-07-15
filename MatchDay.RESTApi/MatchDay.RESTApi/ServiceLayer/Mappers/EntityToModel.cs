using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Mappers
{
    public record EntityToModel
    {
        public static PlayerModel ToModel(PlayerEntity entity)
        {
            return new PlayerModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                TeamId = entity.TeamId,
                TeamName = entity.Team?.Name,
            };
        }

        public static CoachModel ToModel(CoachEntity entity)
        {
            return new CoachModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                TeamId = entity.TeamId,
                TeamName = entity.Team?.Name,
            };
        }

        public static TeamModel ToModel(TeamEntity entity)
        {
            return new TeamModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Players = entity.Players?.Select(ToModel).ToList() ?? null,
                Coach = entity.Coach == null ? null : ToModel(entity.Coach),
            };
        }
    }
}
