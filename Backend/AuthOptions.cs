using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend
{
    public class AuthOptions
    {
        public const string ISSUER = "https://localhost:44332"; 
        public const string AUDIENCE = "http://localhost:3000/"; 
        const string KEY = "mysupersecret_secretkey!"; 
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
