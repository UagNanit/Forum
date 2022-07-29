using Forum._3.Models.ViewModels;
using Forum._3.Models;

namespace Forum._3.Services.Abstraction
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
        AuthData GetAuthData(User user);
    }
}