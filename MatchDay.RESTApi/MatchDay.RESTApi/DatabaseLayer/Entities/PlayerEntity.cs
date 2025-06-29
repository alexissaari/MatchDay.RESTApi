namespace MatchDay.RESTApi.DatabaseLayer.Entities
{
    public class PlayerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int TeamId { get; set; }
        public TeamEntity Team { get; set; }
    }
}
