namespace MatchDay.RESTApi.WebLayer.DTOs
{
    public class GetTeamResponseDto
    {
        public string TeamName { get; set; }

        public ICollection<string>? Roster { get; set; }

        public string? CoachName { get; set; }
    }
}
