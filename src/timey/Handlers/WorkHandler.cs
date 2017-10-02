using System;
using System.Collections.Generic;
using timey.Commands;
using timey.Events;
using timey.Events.Infrastructure;

namespace timey.Handlers
{
    public class WorkHandler
    {
        public IEnumerable<EventBase> Handle(StartWork command, string userId)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (command.TimeyTaskId == Guid.Empty)
                throw new ArgumentNullException(nameof(command.TimeyTaskId));
            if (command.WorkId == Guid.Empty)
                throw new ArgumentNullException(nameof(command.WorkId));
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            return new List<EventBase> { RaiseEvent(command, userId) };
        }

        public IEnumerable<EventBase> Handle(EndWork command, string userId)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (command.TimeyTaskId == Guid.Empty)
                throw new ArgumentNullException(nameof(command.TimeyTaskId));
            if (command.WorkId == Guid.Empty)
                throw new ArgumentNullException(nameof(command.WorkId));
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            return new List<EventBase> { RaiseEvent(command, userId) };
        }

        private WorkStarted RaiseEvent(StartWork command, string userId)
        {
            return new WorkStarted
            {
                Id = command.WorkId,
                TimeyTaskId = command.TimeyTaskId,
                UserId = userId,
                StartTime = DateTime.UtcNow
            };
        }

        private WorkEnded RaiseEvent(EndWork command, string userId)
        {
            return new WorkEnded
            {
                Id = command.WorkId,
                TimeyTaskId = command.TimeyTaskId,
                UserId = userId,
                EndTime = DateTime.UtcNow
            };
        }
    }
}
