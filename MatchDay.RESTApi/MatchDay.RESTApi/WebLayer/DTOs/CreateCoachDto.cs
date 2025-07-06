namespace MatchDay.RESTApi.WebLayer.DTOs
{
    public class CreateCoachDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TeamId { get; set; }
    }
}
