//Defines authentication operations
using JWT.Models;

namespace JWT.Service
{
    public interface IAuthService
    {
        string Register(RegisterRequest request);
        string Login(LoginRequest request);
    }
}


//Register() is responsible for registering a new user.
//RegisterRequest contains the registration details
