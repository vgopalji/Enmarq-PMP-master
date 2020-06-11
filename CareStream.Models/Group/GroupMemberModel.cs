using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CareStream.Models
{
    public class GroupMemberModel
    {
        public GroupMemberModel()
        {
            Members = new Dictionary<string, string>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "members", Required = Required.Default)]
        public Dictionary<string, string> Members { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "assignedmembers", Required = Required.Default)]
        public List<UserModel> AssignedMembers { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "groupid", Required = Required.Default)]
        public string GroupId { get; set; }

    }

    public class GroupMemberAssignModel
    {
        public GroupMemberAssignModel()
        {
            SelectedMembers = new List<string>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "groupid", Required = Required.Default)]
        public string GroupId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "selectedmembers", Required = Required.Default)]
        public List<string> SelectedMembers { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
