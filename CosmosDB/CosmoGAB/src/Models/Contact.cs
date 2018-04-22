namespace CosmoGab.Models
{
    using System;

    using Newtonsoft.Json;

    public class Contact // : IHaveEntityType
    {
        [JsonProperty(PropertyName = "contactType")]
        public ContactType ContactType { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string Firstname { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string Lastname { get; set; }

        [JsonProperty(PropertyName = "birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        //[JsonProperty(PropertyName = "entityType")]
        //public string EntityType => "Contact";
    }
}