using System;
using System.Collections.Generic;
using timey.Commands;
using timey.Events;
using timey.Events.Infrastructure;

namespace timey.Handlers
{
    public class TimeyTaskHandler
    {
        public IEnumerable<EventBase> Handle(AddTimeyTask command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (command.TimeyTaskId == Guid.Empty)
                throw new ArgumentNullException(nameof(command.TimeyTaskId));
            if (string.IsNullOrEmpty(command.Name))
                throw new ArgumentNullException(nameof(command.Name));

            var events = new Queue<EventBase>();
            events.Enqueue(RiseEvent(command));
            return events;
        }

        public IEnumerable<EventBase> Handle(ChangeTimeyTask command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (command.TimeyTaskId == Guid.Empty)
                throw new ArgumentNullException(nameof(command.TimeyTaskId));
            if (string.IsNullOrEmpty(command.Name))
                throw new ArgumentNullException(nameof(command.Name));

            var events = new Queue<EventBase>();
            events.Enqueue(RiseEvent(command));
            return events;
        }

        private TimeyTaskAdded RiseEvent(AddTimeyTask command)
        {
            return new TimeyTaskAdded
            {
                Id = command.TimeyTaskId,
                Name = command.Name,
                Description = command.Description,
                BudgetId = command.BudgetId,
                BudgetName = command.BudgetName,
                CustomerId = command.CustomerId,
                CustomerName = command.CustomerName,
                ProjectId = command.ProjectId,
                ProjectName = command.ProjectName
            };
        }

        private TimeyTaskChanged RiseEvent(ChangeTimeyTask command)
        {
            return new TimeyTaskChanged
            {
                Id = command.TimeyTaskId,
                Name = command.Name,
                Description = command.Description,
                BudgetId = command.BudgetId,
                BudgetName = command.BudgetName,
                CustomerId = command.CustomerId,
                CustomerName = command.CustomerName,
                ProjectId = command.ProjectId,
                ProjectName = command.ProjectName
            };
        }
    }
}
