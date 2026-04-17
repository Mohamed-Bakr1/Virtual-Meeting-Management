namespace Domain.Common
{
    public class Result<T> : Result
    {
        public T Data { get; private set; }

        // Private constructor to prevent direct instantiation
        private Result() : base() { }

        public static Result<T> Success(T data, string message = "Operation completed successfully")
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public new static Result<T> Failure(string error, ErrorType errorType = ErrorType.Validation)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Errors = new List<string> { error },
                ErrorType = errorType
            };
        }

        public new static Result<T> Failure(List<string> errors, ErrorType errorType = ErrorType.Validation)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Errors = errors,
                ErrorType = errorType
            };
        }
    }
}