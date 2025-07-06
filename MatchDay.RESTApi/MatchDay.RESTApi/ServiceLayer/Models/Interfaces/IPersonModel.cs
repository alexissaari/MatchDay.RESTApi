namespace MatchDay.RESTApi.ServiceLayer.Models.Interfaces
{
    public interface IPersonModel
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
