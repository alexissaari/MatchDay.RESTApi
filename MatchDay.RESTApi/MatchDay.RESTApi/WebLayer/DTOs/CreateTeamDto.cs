namespace MatchDay.RESTApi.WebLayer.DTOs
{
    public class CreateTeamDto
    {
        public required string Name {  get; set; }
        public ICollection<CreatePlayerDto>? Roster { get; set; }
        public CreateCoachDto? Coach { get; set; }
    }
}
