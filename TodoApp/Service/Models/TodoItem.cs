using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class TodoItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_completed")]
        public bool IsCompleted { get; set; }
    }
}