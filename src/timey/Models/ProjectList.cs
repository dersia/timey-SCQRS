using Newtonsoft.Json;
using System.Collections.Generic;

namespace timey.Models
{
    public class ProjectList
    {
        [JsonProperty("projects")]
        public IList<Project> Projects { get; } = new List<Project>();
    }
}
