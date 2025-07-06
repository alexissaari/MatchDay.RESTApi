namespace MatchDay.RESTApi.WebLayer.DTOs
{
    public class TeamDto
    {
        public string TeamName { get; set; }

        public ICollection<string> Players { get; set; }

        public string CoachName { get; set; }
    }
}
