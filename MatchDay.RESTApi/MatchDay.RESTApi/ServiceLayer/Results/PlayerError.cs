namespace MatchDay.RESTApi.ServiceLayer.Results
{
    public static class PlayerError
    {
        public static readonly Error PlayerNotFound = new Error(
            ErrorCodes.PlayerNotFound,
            "Player not found.",
            ErrorType.NotFound);

        public static readonly Error PlayerAlreadyExists = new Error(
            ErrorCodes.PlayerAlreadyExists, 
            "This player already exists and cannot be added.",
            ErrorType.Conflict);
    }
}
