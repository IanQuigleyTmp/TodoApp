using Data.Entity;
using Data.Framework;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Service.Controllers
{
    public class CommonCode
    {
        internal static UserDetails GetCurrentUser(HttpRequestMessage request)
        {
            if (!request.Headers.Contains("auth_token"))
                return UserDetails.Anonymous;

            var authToken = Repository.Select<AuthToken>(Where.EqualStr("Token", request.Headers.GetValues("auth_token").FirstOrDefault()));
            if (authToken.Count == 0)
                return UserDetails.Anonymous;

            // (we don't have user profiles, so just grab their login name for "display name")
            var login = Repository.Select<Login>(Where.EqualLong("Id", authToken.First().OwnerId));
            if (login.Count == 0)
                return UserDetails.Anonymous;

            return new UserDetails() { DisplayName = login[0].Username, IsAuthenticated = true, UserId = login[0].Id };
        }

        internal static long ConvertDateToTicks(DateTime date)
        {
            return (long)date
                        .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                        .TotalMilliseconds;
        }
    }
}