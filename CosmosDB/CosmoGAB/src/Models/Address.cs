namespace CosmoGab.Models
{
    using Newtonsoft.Json;

    public class Address
    {
        [JsonProperty(PropertyName = "addressLine")]
        public string AddressLine { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}