//Implements registration and login logici just  
using JWT.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
namespace JWT.Service
{
    public class AuthService : IAuthService
    {
        private static List<User> users = new List<User>();
        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        private readonly ITokenService _tokenService;
        public AuthService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public string Login(LoginRequest request)
        {
            // 1. Find the user using email
            var user = users.FirstOrDefault(
                u => u.Email == request.Email
            );

            // 2. Check whether user exists
            if (user == null)
            {
                return "Invalid email or password";
            }

            // 3. Verify entered password against stored hash
            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            // 4. Check password verification result
            if (result == PasswordVerificationResult.Failed)
            {
                return "Invalid email or password";
            }

            // 5. Generate JWT access token
            var token = _tokenService.GenerateAccessToken(
                user.Id,
                user.Email,
                user.Role
            );

            return token;
        }
        public string Register(RegisterRequest request)
        {
            var existingUser = users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null) {
                return "email already exist";
            }
            var user = new User
            {
                Id = users.Count + 1,
                Name = request.Name,
                Email = request.Email,
                Role = "User"
            };
            user.PasswordHash = passwordHasher.HashPassword(user, request.password);
            users.Add(user);
            return "User registered successfully";
        }

    }
}
