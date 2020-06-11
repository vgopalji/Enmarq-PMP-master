using Newtonsoft.Json;
using System.Collections.Generic;

namespace CareStream.Models
{
    public class CustomExtension
    {
        [JsonProperty(PropertyName = "odatametadata", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string odatametadata { get; set; }

        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public List<Value> value { get; set; }
    }

    public class Value
    {
        [JsonProperty(PropertyName = "odata.type", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string odatatype { get; set; }

        [JsonProperty(PropertyName = "objectType", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string objectType { get; set; }

        [JsonProperty(PropertyName = "objectId", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string objectId { get; set; }

        [JsonProperty(PropertyName = "deletionTimestamp", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public object deletionTimestamp { get; set; }

        [JsonProperty(PropertyName = "appDisplayName", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string appDisplayName { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string name { get; set; }

        [JsonProperty(PropertyName = "dataType", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public string dataType { get; set; }

        [JsonProperty(PropertyName = "isSyncedFromOnPremises", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public bool isSyncedFromOnPremises { get; set; }

        [JsonProperty(PropertyName = "targetObjects", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public List<string> targetObjects { get; set; }
    }
}
