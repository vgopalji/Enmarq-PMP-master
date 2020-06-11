using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Graph;

namespace CareStream.Models
{
    public class UserGroup
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "user", Required = Required.Default)]
        public User User { get; set; }

        [JsonProperty(PropertyName = "groups", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Groups { get; set; }

        [JsonProperty(PropertyName = "allgroups", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> AllGroups { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
