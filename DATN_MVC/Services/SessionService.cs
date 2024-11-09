namespace DATN_MVC.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.Session.GetString("JWTToken"));
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("UserName");
        }

        public void SaveLoginInfo(string token, string userName, string role)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString("JWTToken", token);
            session.SetString("UserName", userName);
            session.SetString("UserRole", role);
        }

        public void ClearLoginInfo()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }
    }
}
