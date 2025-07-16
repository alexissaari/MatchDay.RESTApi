namespace MatchDay.RESTApi.ServiceLayer.Results
{
    public static class ErrorCodes
    {
        // Players
        public readonly static string PlayerNotFound = "PlayerNotFound";
        public readonly static string PlayerAlreadyExists = "PlayerAlreadyExists";

        // Coaches
        public readonly static string CoachNotFound = "CoachNotFound";
        public readonly static string CoachAlreadyExists = "CoachAlreadyExists";

        // Teams
        public readonly static string TeamNotFound = "TeamNotFound";
        public readonly static string TeamAlreadyExists = "TeamAlreadyExists";
        public readonly static string TeamCreationError = "TeamCreationError";
    }
}
