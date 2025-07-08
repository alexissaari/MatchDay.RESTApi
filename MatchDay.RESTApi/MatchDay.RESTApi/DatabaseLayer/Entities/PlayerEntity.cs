using MatchDay.RESTApi.DatabaseLayer.Entities.Interfaces;

namespace MatchDay.RESTApi.DatabaseLayer.Entities
{
    public class PlayerEntity : IPersonEntity
    {
        public int? Id { get; set; } // Not required because on creation this won't have an Id yet
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public int? TeamId { get; set; } // Not required because on creation this won't have an Id yet
        public TeamEntity? Team { get; set; } // Not required because on creation this won't have an object to reference yet
    }
}
