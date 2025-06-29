namespace MatchDay.RESTApi.DatabaseLayer.Entities.Interfaces
{
    public interface IPersonEntity
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
