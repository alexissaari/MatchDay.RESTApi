using MatchDay.RESTApi.DatabaseLayer.Entities.Interfaces;

namespace MatchDay.RESTApi.DatabaseLayer.Entities
{
    public class PlayerEntity : IPersonEntity
    {
        // Player
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Team - nullable so that a player can exist without a team
        public int? TeamId { get; set; }
        public TeamEntity? Team { get; set; }
    }
}
