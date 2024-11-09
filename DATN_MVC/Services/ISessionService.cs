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
}
