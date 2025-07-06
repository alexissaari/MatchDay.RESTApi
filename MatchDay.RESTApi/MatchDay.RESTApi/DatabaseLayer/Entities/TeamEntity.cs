using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.DatabaseLayer.Entities
{
    public class TeamEntity
    {
        // Team
        public int Id { get; set; }
        public string Name { get; set; }

        // Players - nullable so that a team can exist without players assigned yet
        public ICollection<PlayerEntity>? Players { get; set; }

        // Coach - nullable so that a team can exist without a coach assigned yet
        public int? CoachId { get; set; }
        public CoachEntity? Coach { get; set; }
    }
}
