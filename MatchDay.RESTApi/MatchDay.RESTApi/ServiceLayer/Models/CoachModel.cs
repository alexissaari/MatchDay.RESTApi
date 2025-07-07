using MatchDay.RESTApi.ServiceLayer.Models.Interfaces;

namespace MatchDay.RESTApi.ServiceLayer.Models
{
    public class CoachModel : IPersonModel
    {
        public int? Id { get; set; } // Not required because on creation this won't have an Id yet
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}
