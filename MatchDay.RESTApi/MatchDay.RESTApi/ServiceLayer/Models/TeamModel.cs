namespace MatchDay.RESTApi.ServiceLayer.Models
{
    public class TeamModel
    {
        public int? Id { get; set; } // Not required because on creation this won't have an Id yet
        public required string Name { get; set; }

        public ICollection<PlayerModel>? Players { get; set; }
        public CoachModel? Coach { get; set; }
    }
}
