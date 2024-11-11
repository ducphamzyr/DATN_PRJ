namespace DATN_MVC.Services
{
    public interface ISessionService
    {
        bool IsLoggedIn();
        bool IsAdmin();
        string GetUserToken();
        string GetUserName();
        string GetUserRole();
    }
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsLoggedIn()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
            return !string.IsNullOrEmpty(token);
        }

        public bool IsAdmin()
        {
            var role = _httpContextAccessor.HttpContext?.Session.GetString("UserRole");
            return role == "Admin";
        }

        public string GetUserToken()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("UserName");
        }

        public string GetUserRole()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("UserRole");
        }
    }
}
