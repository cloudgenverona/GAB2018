namespace CosmoGab.Models
{
    using Newtonsoft.Json;

    public class Item // : IHaveEntityType
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }

        //[JsonProperty(PropertyName = "entityType")]
        //public string EntityType => "Item";
    }
}