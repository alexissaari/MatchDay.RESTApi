namespace MatchDay.RESTApi.ServiceLayer.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlayerModel> Players { get; set; }
    }
}
