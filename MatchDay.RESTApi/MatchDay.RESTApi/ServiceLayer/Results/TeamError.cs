namespace MatchDay.RESTApi.ServiceLayer.Results
{
    public class TeamError
    {
        public static readonly Error TeamNotFound = new Error(
            ErrorCodes.TeamNotFound,
            "Team not found.",
            ErrorType.NotFound);

        public static readonly Error TeamAlreadyExists = new Error(
            ErrorCodes.TeamAlreadyExists,
            "This team already exists and cannot be added.",
            ErrorType.Conflict);

        public static readonly Error TeamCreationError = new Error(
            ErrorCodes.TeamCreationError,
            "Error occured when creating new team.",
            ErrorType.Conflict);
    }
}
