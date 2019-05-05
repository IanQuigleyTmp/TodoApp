
using Data.Entity;
using Data.Framework;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    [RoutePrefix("api/todo")]
    public class TodoController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public IEnumerable<TodoDetails> All()
        {
            var user = CommonCode.GetCurrentUser(Request);

            // No user, return empty list
            if (!user.IsAuthenticated)
                return new List<TodoDetails>();

            // Would use something like AutoMapper, but this is quicker for now
            return from todoEntry
                   in Repository.Select<TodoEntry>(Where.EqualLong("OwnerId", user.UserId))
                   select
                       new TodoDetails()
                       {
                           Id = todoEntry.Id,
                           Description = todoEntry.Description,
                           IsCompleted = todoEntry.IsCompleted,
                           LastUpdatedTicks = CommonCode.ConvertDateToTicks(todoEntry.LastUpdated)
                       };
        }

        [HttpPost]
        [Route("create")]
        public IEnumerable<TodoDetails> Create([FromBody] TodoItem item)
        {
            var user = CommonCode.GetCurrentUser(Request);
            if (user.IsAuthenticated && item != null)
            {
                var todoEntry = new TodoEntry() { Description = HttpUtility.HtmlEncode(item.Description), IsCompleted = item.IsCompleted, LastUpdated = DateTime.Now, OwnerId = user.UserId };
                Repository.SaveOrUpdate(todoEntry);
            }

            return All();
        }

        [HttpPatch]
        [Route("update")]
        public IEnumerable<TodoDetails> Update([FromBody] TodoItem item)
        {
            var user = CommonCode.GetCurrentUser(Request);
            if (user.IsAuthenticated && item != null)
            {
                var todoEntryList = Repository.Select<TodoEntry>(Where.And(Where.EqualLong("OwnerId", user.UserId), Where.EqualLong("Id", item.Id)));
                if (todoEntryList.Count == 1)
                {
                    var todoEntry = todoEntryList[0];

                    todoEntry.IsCompleted = item.IsCompleted;
                    todoEntry.LastUpdated = DateTime.Now;

                    Repository.SaveOrUpdate(todoEntry);
                }
            }

            return All();
        }

        [HttpDelete]
        [Route("delete")]
        public IEnumerable<TodoDetails> Delete([FromBody] TodoItem item)
        {
            var user = CommonCode.GetCurrentUser(Request);
            if (user.IsAuthenticated)
                Repository.Delete<TodoEntry>(Where.And(Where.EqualLong("OwnerId", user.UserId), Where.EqualLong("Id", item.Id)));

            return All();                
        }
    }

    }
