using System;
using timey.Events.Infrastructure;

namespace timey.Events
{
    public class WorkStarted : EventBase
    {
        public DateTime StartTime { get; set; }
        public string UserId { get; set; }
        public Guid TimeyTaskId { get; set; }
    }
}
