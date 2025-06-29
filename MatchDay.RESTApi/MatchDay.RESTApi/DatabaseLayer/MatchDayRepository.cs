using MatchDay.RESTApi.DatabaseLayer.Entities;
using MatchDay.RESTApi.DatabaseLayer.Interfaces;

namespace MatchDay.RESTApi.DatabaseLayer
{
    public class MatchDayRepository : IMatchDayRepository
    {
        public PlayerEntity GetPlayer(int id)
        {
            return new PlayerEntity
            {
                Id = id,
                FirstName = "Alexis",
                LastName = "Saari",
                TeamId = 1,
                Team = new TeamEntity
                {
                    Id = 1,
                    Name = "Hopkins Rugby Football Club",
                }
            };
        }
    }
}
