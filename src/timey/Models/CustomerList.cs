using Newtonsoft.Json;
using System.Collections.Generic;

namespace timey.Models
{
    public class CustomerList
    {
        [JsonProperty("customers")]
        public IList<Customer> Customers { get; } = new List<Customer>();
    }
}
