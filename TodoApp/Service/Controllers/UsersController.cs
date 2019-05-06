using Data.Entity;
using Data.Framework;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {        
        [HttpGet]
        [Route("current")]
        public UserDetails Current()
        {
            return CommonCode.GetCurrentUser(Request);
        }        

        [HttpPost]
        [Route("authenticate")]
        public UserDetails Authenticate([FromBody] LoginCredentials credentials)
        {
            // Basic sanity and sanatize check
            credentials = Sanitize(credentials);
            if (credentials == null)
                return null;

            // Protection against Brute Force password checking 'goes here'

            // Return Authorization token in return for Username/Password
            var token = Logic.Authenticate.GetTokenFor(credentials.Username, credentials.Password);
            
            return new UserDetails()
            {
                IsAuthenticated = token != null,
                DisplayName = credentials.Username,
                AuthToken = token,
                Error = (token != null ? "" : "Login Failed. Try 'test' and 'pwd123'. ")
            };
        }

        [HttpPost]
        [Route("create")]
        public UserDetails Create([FromBody] LoginCredentials credentials)
        {
            // Basic sanity and sanatize check
            credentials = Sanitize(credentials);
            if (credentials == null)
                return null;

            // Protection against DDOS etc 'goes here'

            // Create a new user
            var token = Logic.Authenticate.Create(credentials.Username, credentials.Password);

            return new UserDetails()
            {
                IsAuthenticated = token != null,
                DisplayName = credentials.Username,
                AuthToken = token
            };
        }

        [HttpPost]
        [Route("logout")]
        public UserDetails Logout()
        {
            if (!Request.Headers.Contains("auth_token"))
                return UserDetails.Anonymous;

            // Delete token from storage
            Repository.Delete<AuthToken>(Where.EqualStr("Token", Request.Headers.GetValues("auth_token").FirstOrDefault()));

            // user is loged out
            return UserDetails.Anonymous;

        }

            private LoginCredentials Sanitize(LoginCredentials credentials)
        {
            // Sanitize user inputs
            if (!AcceptableString(credentials.Username) || !AcceptableString(credentials.Password))
                return null;
            
            return credentials;
        }

        private bool AcceptableString(string str)
        {
            return !string.IsNullOrWhiteSpace(str) && str.Length < 100 && str.IndexOfAny(new[] { '<', '>' }) == -1;
        }
    }
}