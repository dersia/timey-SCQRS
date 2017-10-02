using Newtonsoft.Json;
using System.Collections.Generic;

namespace timey.Models
{
    public class WorkList
    {
        [JsonProperty("workList")]
        public IList<Work> Works { get; } = new List<Work>();
    }
}
