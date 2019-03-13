using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class AuthOptions
    {
        public const string ISSUER = "https://localhost:44331"; // издатель токена
        public const string AUDIENCE = "http://localhost:3000/"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
