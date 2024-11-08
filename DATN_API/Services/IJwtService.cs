namespace DATN_API.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
        bool ValidateToken(string token);
    }
}