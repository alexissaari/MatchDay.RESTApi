using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.DatabaseLayer.Entities
{
    public class TeamEntity
    {
        // Team
        public int Id { get; set; }
        public string Name { get; set; }

        // Players
        public ICollection<PlayerEntity> Players { get; set; }

        // Coach
        public int? CoachId { get; set; }
        public CoachEntity Coach { get; set; }
    }
}
