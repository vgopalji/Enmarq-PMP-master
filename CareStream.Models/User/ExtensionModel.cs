using System.Collections.Generic;
using Newtonsoft.Json;


namespace CareStream.Models
{
    public class ExtensionModel
    {
        public ExtensionModel()
        {
            TargetObjects = new List<string>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "objectId", Required = Required.Default)]
        public string ObjectId { get; set; }

        [JsonProperty(PropertyName = "targetObjects", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public List<string> TargetObjects { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "dataType", Required = Required.Default)]
        public string DataType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "attributetype", Required = Required.Default)]
        public string AttributeType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isbuildin", Required = Required.Default)]
        public bool IsBuildIn { get; set; }
    }

    public class UserAttributeModel
    {
        public UserAttributeModel()
        {
            TargetObjects = new List<string>();
        }

        [JsonProperty(PropertyName = "targetObjects", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public List<string> TargetObjects { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "dataType", Required = Required.Default)]
        public string DataType { get; set; }


    }

}
