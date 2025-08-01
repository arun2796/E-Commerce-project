using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Address
    {
        [JsonIgnore]
        public int Id { get; set; }

        public required string Name { get; set; }
        public string StreetAddress { get; set; } =null!;

        public string? Landmark { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }

        public AddressType AddressType { get; set; } = AddressType.Home;

        [JsonPropertyName("pin_code")]
        public required string PinCode { get; set; }
        public required string Country { get; set; }


    }
}
