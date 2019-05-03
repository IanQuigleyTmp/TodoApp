using Data.Entity;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Authenticate
    {
        public static bool User(string username, string password)
        {
            return Repository.Select<Login>( 
                    Where.And( Where.EqualStr("Username", username), Where.EqualStr("Password", Encode(password))))
                    .Count == 1;
        }

        public static void Create(string username, string password)
        {
            var login = new Login() { Username = username, Password = Encode(password) };
            Repository.SaveOrUpdate(login);
        }

        private static string Encode(string password)
        {
            // Quick hash of password. Not good enough for production scenario
            var sha1data = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(sha1data);            
        }
    }

  
}
