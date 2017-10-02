using Newtonsoft.Json;
using System;
using timey.Events;
using timey.Models.Infrastructure;

namespace timey.Models
{
    public class Work : ModelBase
    {
        [JsonProperty("startTime")]
        public DateTime StartTime { get; private set; }
        [JsonProperty("endTime")]
        public DateTime? EndTime { get; private set; }
        [JsonProperty("userId")]
        public string UserId { get; private set; }
        [JsonProperty("timeyTaskId")]
        public Guid TimeyTaskId { get; private set; }

        public Work()
        {
            RegisterHydration<WorkStarted>(Apply, _ => id.Equals(Guid.Empty));
            RegisterHydration<WorkEnded>(Apply, @event => !id.Equals(Guid.Empty) && id.Equals(@event.Id) && !EndTime.HasValue);
        }

        public void Apply(WorkStarted @event)
        {
            id = @event.Id;
            StartTime = @event.StartTime;
            UserId = @event.UserId;
            TimeyTaskId = @event.TimeyTaskId;
        }

        public void Apply(WorkEnded @event)
        {
            if(TimeyTaskId.Equals(@event.TimeyTaskId)) 
                EndTime = @event.EndTime;
        }
    }
}
