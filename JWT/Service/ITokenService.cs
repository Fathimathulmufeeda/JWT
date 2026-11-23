//Defines token-generation operations
namespace JWT.Service
{
    public interface ITokenService
    {
        string GenerateAccessToken(int userId, string email, string role);
    }
}
