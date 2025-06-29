using MatchDay.RESTApi.ServiceLayer.Models;

namespace MatchDay.RESTApi.DatabaseLayer.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PlayerEntity> Players { get; set; }
    }
}
