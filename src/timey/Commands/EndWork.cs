using System;

namespace timey.Commands
{
    public class EndWork
    {
        public Guid WorkId { get; set; }
        public Guid TimeyTaskId { get; set; }
    }
}
