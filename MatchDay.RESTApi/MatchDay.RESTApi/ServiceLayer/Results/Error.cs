﻿namespace MatchDay.RESTApi.ServiceLayer.Results
{
    public record Error
    {
        public Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
    }
}
