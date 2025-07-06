using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.ServiceLayer.Mappers
{
    public static class EntityToModel
    {
        public static PlayerModel? ToModel(PlayerEntity entity)
        {
            if (entity == null) return null;

            return new PlayerModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                TeamId = entity.TeamId,
                TeamName = entity.Team.Name,
            };
        }

        public static CoachModel? ToModel(CoachEntity entity)
        {
            if (entity == null) return null;

            return new CoachModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                TeamId = entity.TeamId,
                TeamName = entity.Team.Name,
            };
        }

        public static TeamModel? ToModel(TeamEntity entity)
        {
            if (entity == null) return null;

            return new TeamModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Players = entity.Players.Select(ToModel).ToList(),
                CoachName = GetFullName(entity.Coach.FirstName, entity.Coach.LastName),
            };
        }

        private static string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
    }
}
