using System;
using timey.Events.Infrastructure;

namespace timey.Events
{
    public class WorkEnded : EventBase
    {
        public DateTime EndTime { get; set; }
        public string UserId { get; set; }
        public Guid TimeyTaskId { get; set; }
    }
}
