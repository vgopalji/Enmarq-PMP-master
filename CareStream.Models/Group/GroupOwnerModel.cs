using System.Collections.Generic;
using Newtonsoft.Json;

namespace CareStream.Models
{
    public class GroupOwnerModel
    {
        public GroupOwnerModel()
        {
            Owners = new Dictionary<string, string>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "owners", Required = Required.Default)]
        public Dictionary<string, string> Owners { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "assignedowners", Required = Required.Default)]
        public List<UserModel> AssignedOwners {get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "groupid", Required = Required.Default)]
        public string GroupId { get; set; }


    }

    public class GroupOwnerAssignModel
    {
        public GroupOwnerAssignModel()
        {
            SelectedOwners = new List<string>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "groupid", Required = Required.Default)]
        public string GroupId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "selectedowners", Required = Required.Default)]
        public List<string> SelectedOwners { get; set; }
    }
}
