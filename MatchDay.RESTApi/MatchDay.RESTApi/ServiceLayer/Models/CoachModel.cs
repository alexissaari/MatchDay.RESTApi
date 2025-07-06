using MatchDay.RESTApi.ServiceLayer.Models.Interfaces;

namespace MatchDay.RESTApi.ServiceLayer.Models
{
    public class CoachModel : IPersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}
