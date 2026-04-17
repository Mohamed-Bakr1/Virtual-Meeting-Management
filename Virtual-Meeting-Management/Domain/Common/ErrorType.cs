namespace Domain.Common
{
    public enum ErrorType
    {
        Validation,             // 400 Bad Request
        NotFound,               // 404 Not Found
        Unauthorized,           // 401 Unauthorized
        Forbidden,              // 403 Forbidden
        Conflict,               // 409 Conflict
        ServerError,            // 500 Internal Server Error
        ServiceUnavailable,     // 503 Service Unavailable
    }
}