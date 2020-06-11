using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CareStream.Models
{
    public class GroupModel
    {
        public GroupModel()
        {
            GroupTypes = new List<string>
            {
                CareStreamConst.Security,
                CareStreamConst.O365,
            };
            NoOfMembers = 0;
            NoOfOwners = 0;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "createdDateTime", Required = Required.Default)]
        public DateTimeOffset? CreatedDateTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "groupTypes", Required = Required.Default)]
        public IEnumerable<string> GroupTypes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "groupType", Required = Required.Default)]
        public string GroupType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "displayName", Required = Required.Default)]
        public string DisplayName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mailEnabled", Required = Required.Default)]
        public bool? MailEnabled { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mailNickname", Required = Required.Default)]
        public string MailNickname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "securityEnabled", Required = Required.Default)]
        public bool? SecurityEnabled { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "visibility", Required = Required.Default)]
        public string Visibility { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "objectid", Required = Required.Default)]
        public string ObjectId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "allowExternalSenders", Required = Required.Default)]
        public bool? AllowExternalSenders { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "autoSubscribeNewMembers", Required = Required.Default)]
        public bool? AutoSubscribeNewMembers { get; set; }
        //owner and members
        [JsonProperty(PropertyName = "additionaldata", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> AdditionalData { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "noofowners", Required = Required.Default)]
        public int NoOfOwners { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "noofmembers", Required = Required.Default)]
        public int NoOfMembers { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "source", Required = Required.Default)]
        public string Source { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
