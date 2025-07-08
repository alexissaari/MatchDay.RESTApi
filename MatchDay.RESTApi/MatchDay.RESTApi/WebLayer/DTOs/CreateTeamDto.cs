namespace MatchDay.RESTApi.WebLayer.DTOs
{
    public class CreateTeamDto
    {
        public string Name {  get; set; }
        public ICollection<CreatePlayerDto>? Roster { get; set; }
        public CreateCoachDto? Coach { get; set; }
    }
}
