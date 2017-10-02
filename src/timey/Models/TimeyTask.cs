using Newtonsoft.Json;
using System;
using timey.Events;
using timey.Models.Infrastructure;

namespace timey.Models
{
    public class TimeyTask : ModelBase
    {
        [JsonProperty("customerName")]
        public string CustomerName { get; private set; }
        [JsonProperty("customerId")]
        public Guid CustomerId { get; private set; }
        [JsonProperty("projectName")]
        public string ProjectName { get; private set; }
        [JsonProperty("projectId")]
        public Guid ProjectId { get; private set; }
        [JsonProperty("budgetName")]
        public string BudgetName { get; private set; }
        [JsonProperty("budgetId")]
        public Guid BudgetId { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("description")]
        public string Description { get; private set; }

        public TimeyTask()
        {
            RegisterHydration<TimeyTaskAdded>(Apply, _ => id.Equals(Guid.Empty));
            RegisterHydration<TimeyTaskChanged>(Apply, @event => !id.Equals(Guid.Empty) && id.Equals(@event.Id));
        }

        public void Apply(TimeyTaskAdded @event)
        {
            id = @event.Id;
            CustomerName = @event.CustomerName;
            CustomerId = @event.CustomerId;
            ProjectName = @event.ProjectName;
            ProjectId = @event.ProjectId;
            BudgetName = @event.BudgetName;
            BudgetId = @event.BudgetId;
            Name = @event.Name;
            Description = @event.Description;
        }

        public void Apply(TimeyTaskChanged @event)
        {
            CustomerName = @event.CustomerName;
            CustomerId = @event.CustomerId;
            ProjectName = @event.ProjectName;
            ProjectId = @event.ProjectId;
            BudgetName = @event.BudgetName;
            BudgetId = @event.BudgetId;
            Name = @event.Name;
            Description = @event.Description;
        }
    }
}
