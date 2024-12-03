using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace API.Domain.Hero.AddressResults
{
    public class AddressResult
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("zipcode")]
        public string ZipCode { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }

        [JsonProperty("referencepoint")]
        public string ReferencePoint { get; set; }

    }
}

