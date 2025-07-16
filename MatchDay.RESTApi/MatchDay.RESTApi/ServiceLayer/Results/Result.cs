namespace MatchDay.RESTApi.ServiceLayer.Results
{
    public record Result
    {
        private Result(bool isSuccess, Error? errorResult = null, object? successResult = null)
        {
            IsSuccess = isSuccess;

            if (!isSuccess && errorResult != null)
            {
                ErrorResult = errorResult;
            }
            else if (isSuccess && successResult != null)
            {
                SuccessResult = successResult;
            }
            else
            {
                throw new InvalidOperationException($"Cannot create Result.");
            }
        }

        public bool IsSuccess { get; }
        public Error? ErrorResult { get; } = null!;
        public object? SuccessResult { get; } = default!;

        public static Result Success(object successfulResult)
        {
            return new Result(isSuccess: true, successResult: successfulResult);
        }

        public static Result Failure(Error error)
        {
            return new Result(isSuccess: false, errorResult: error);
        }
    }
}
