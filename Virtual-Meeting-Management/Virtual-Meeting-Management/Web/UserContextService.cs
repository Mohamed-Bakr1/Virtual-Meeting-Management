using Application.Interfaces;
using System.Security.Claims;

namespace Virtual_Meeting_Management.Web
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContextService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string GetUserID()
        {
            var user = _httpContext.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetUserRole()
        {
            var user = _httpContext.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.Role)?.Value;

        }
    }
}