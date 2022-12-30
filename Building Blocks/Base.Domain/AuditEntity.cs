using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Base.Domain
{
    public class AuditEntity<T>
    {
        [JsonProperty("id")]
        public string Id
        {
            get { return Convert.ToString(Key); }
        }

        [JsonProperty("key")]
        public T Key { get; set; }
        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; } = string.Empty;
        [JsonProperty("creationTime")]
        public DateTime CreationTime { get; set; } = DateTime.Now;
        [JsonProperty("modifiedTime")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;
        [JsonProperty("deletionTime")]
        public DateTime? DeletionTime { get; set; }
        [JsonProperty("createdBy")]
        public int? CreatedBy { get; set; }
        [JsonProperty("modifiedBy")]
        public int? ModifiedBy { get; set; }
        [JsonProperty("deletedBy")]
        public int? DeletedBy { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}