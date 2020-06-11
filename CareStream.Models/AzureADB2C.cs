using Newtonsoft.Json;

namespace CareStream.Models
{
    public class AzureADB2C
    {
        [JsonProperty(PropertyName = "TenantId")]
        public string TenantId { get; set; }

        [JsonProperty(PropertyName = "AppId")]
        public string AppId { get; set; }

        [JsonProperty(PropertyName = "ClientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty(PropertyName = "B2cExtensionAppClientId")]
        public string B2cExtensionAppClientId { get; set; }

        [JsonProperty(PropertyName = "B2cExtensionAppObjectId")]
        public string B2cExtensionAppObjectId { get; set; }

        [JsonProperty(PropertyName = "Instance")]
        public string Instance { get; set; }

        [JsonProperty(PropertyName = "GraphResource")]
        public string GraphResource { get; set; }

        [JsonProperty(PropertyName = "GraphResourceEndPoint")]
        public string GraphResourceEndPoint { get; set; }

        [JsonProperty(PropertyName = "AADGraphResourceId")]
        public string AADGraphResourceId { get; set; }

        [JsonProperty(PropertyName = "AADGraphVersion")]
        public string AADGraphVersion { get; set; }
    }
}
