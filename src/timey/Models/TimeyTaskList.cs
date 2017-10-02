using Newtonsoft.Json;
using System.Collections.Generic;

namespace timey.Models
{
    public class TimeyTaskList
    {
        [JsonProperty("timeyTasks")]
        public IList<TimeyTask> TimeyTasks { get; } = new List<TimeyTask>();
    }
}
