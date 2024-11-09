namespace DATN_MVC.Services
{
    public interface ISessionService
    {
        bool IsLoggedIn();
        string GetUserName();
        void SaveLoginInfo(string token, string userName, string role);
        void ClearLoginInfo();
    }
}
