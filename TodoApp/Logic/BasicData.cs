using Data.Entity;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BasicData
    {
        public static void Create()
        {
            Authenticate.Create("test", "pwd123");

            var user = Repository.Select<Login>(Where.EqualStr("username", "test")).FirstOrDefault();

            Repository.SaveOrUpdate(new TodoEntry() { IsCompleted = false, OwnerId = user.Id, Description = "Go to the gym.", LastUpdated = DateTime.Now.AddDays(-2).AddHours(-2) });
            Repository.SaveOrUpdate(new TodoEntry() { IsCompleted = false, OwnerId = user.Id, Description = "Visit Spain and go hiking.", LastUpdated = DateTime.Now.AddDays(-3).AddHours(-12).AddMinutes(-33113) });
            Repository.SaveOrUpdate(new TodoEntry() { IsCompleted = false, OwnerId = user.Id, Description = "Learn Japanese.", LastUpdated = DateTime.Now.AddMinutes(-1213) });
            Repository.SaveOrUpdate(new TodoEntry() { IsCompleted = true, OwnerId = user.Id, Description = "Invent Time Machine.", LastUpdated = DateTime.Now.AddMinutes(213) });
            Repository.SaveOrUpdate(new TodoEntry() { IsCompleted = true, OwnerId = user.Id, Description = "Remember to go to work.", LastUpdated = DateTime.Now.AddMinutes(-1213) });
        }
    }
}
