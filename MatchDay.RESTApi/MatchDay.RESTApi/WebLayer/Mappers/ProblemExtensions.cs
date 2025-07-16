using MatchDay.RESTApi.ServiceLayer.Results;

namespace MatchDay.RESTApi.WebLayer.Mappers
{
    public static class ProblemExtensions
    {
        public static IResult ResultToProblem(this Result result)
        {
            if (result == null || result.ErrorResult == null) 
            {
                throw new InvalidOperationException("Cannot create ProblemDetails for null Result or Result.ErrorResult object.");
            }

            return Results.Problem(
                statusCode: GetStatusCode(result.ErrorResult.Type),
                title: GetTitle(result.ErrorResult.Type),
                extensions: new Dictionary<string, object?>
                {
                    { "errors", new[] { result.ErrorResult } }
                });
        }

        public static IResult ValidationResultToProblem(FluentValidation.Results.ValidationResult validationResults)
        {
            return Results.Problem(new HttpValidationProblemDetails(validationResults.ToDictionary())
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = "One or more validation errors occured."
            });
        }

        private static int GetStatusCode(ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.Validation:
                    return StatusCodes.Status400BadRequest;
                case ErrorType.NotFound:
                    return StatusCodes.Status404NotFound;
                case ErrorType.Conflict:
                    return StatusCodes.Status409Conflict;
                default:
                    return StatusCodes.Status500InternalServerError;
            }
        }

        private static string GetTitle(ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.Validation:
                    return "Bad Request";
                case ErrorType.NotFound:
                    return "Not Found";
                case ErrorType.Conflict:
                    return "Conflict";
                default:
                    return "Internal Server Error";
            }
        }
    }
}
