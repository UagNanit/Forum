using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Forum._3.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "http://localhost:49147"; // издатель токена
        public const string AUDIENCE = "https://d7w55.csb.app"; // потребитель токена
        const string KEY = "103g5779e7423w32"; // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
