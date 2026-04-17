namespace Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public ErrorType ErrorType { get; protected set; }

        public Result()
        {
            Errors = new List<string>();
        }

        public static Result Success(string message = "Operation completed successfully")
        {
            return new Result { IsSuccess = true, Message = message };
        }

        public static Result Failure(string error)
        {
            return new Result { IsSuccess = false, Errors = new List<string> { error } };
        }

        public static Result Failure(List<string> errors)
        {
            return new Result { IsSuccess = false, Errors = errors };
        }

        public static Result Failure(string message, ErrorType errorType)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                ErrorType = errorType
            };
        }
    }
}