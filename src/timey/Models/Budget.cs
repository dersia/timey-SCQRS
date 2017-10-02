using Newtonsoft.Json;
using System;
using timey.Events;
using timey.Models.Infrastructure;

namespace timey.Models
{
    public class Budget : ModelBase
    {
        [JsonProperty("budgetName")]
        public string BudgetName { get; private set; }

        public Budget()
        {
            RegisterHydration<TimeyTaskAdded>(Apply, _ => id.Equals(Guid.Empty));
            RegisterHydration<TimeyTaskChanged>(Apply, @event => !id.Equals(Guid.Empty) && id.Equals(@event.Id));
        }

        public void Apply(TimeyTaskAdded @event)
        {
            if (@event.BudgetId != Guid.Empty)
            {
                id = @event.BudgetId;
                BudgetName = @event.BudgetName;
            }
        }

        public void Apply(TimeyTaskChanged @event)
        {
            if (@event.BudgetId != Guid.Empty)
            {
                BudgetName = @event.BudgetName;
            }
        }
    }
}
