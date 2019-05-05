using Data.Entity;
using Data.Framework;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Logic
{
    public class Authenticate
    {
        public static string GetTokenFor(string username, string password)
        {
            // Select from Login, where Username & Password, expect 1 record returned
            var login = Repository.Select<Login>(Where.And(Where.EqualStr("Username", username), Where.EqualStr("Password", Encode(password))));
            if (login.Count != 1)
                return null;

            // User is authentic, so return "Auth Token"
            return NewAuthToken(login[0].Id);
        }

        public static string Create(string username, string password)
        {
            // Check if user already exists, if so return null
            var exists = Repository.Select<Login>(Where.EqualStr("Username", username));
            if (exists.Count > 0)
                return null;

            // Create new Login
            var login = new Login() { Username = username, Password = Encode(password) };
            Repository.SaveOrUpdate(login);

            // Return new AuthToken for authenticated user
            return NewAuthToken(login.Id);
        }

        public static long OwnerId(string token)
        {
            var authToken = Repository.Select<AuthToken>(Where.EqualStr("Token", token)).FirstOrDefault();
            return authToken == null ? 0 : authToken.OwnerId;
        }

        private static string NewAuthToken(long ownerId)
        {
            var token = Guid.NewGuid().ToString();
            Repository.SaveOrUpdate(new AuthToken() { OwnerId = ownerId, Token = token });
            return token;
        }

        private static string Encode(string password)
        {
            // Quick hash of password. Not good enough for production scenario
            var sha1data = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.ASCII.GetBytes("HardSalt_" + password));
            return Convert.ToBase64String(sha1data);            
        }
    }
}
