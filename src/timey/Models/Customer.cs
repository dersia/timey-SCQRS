using Newtonsoft.Json;
using System;
using timey.Events;
using timey.Models.Infrastructure;

namespace timey.Models
{
    public class Customer : ModelBase
    {
        [JsonProperty("customerName")]
        public string CustomerName { get; private set; }

        public Customer()
        {
            RegisterHydration<TimeyTaskAdded>(Apply, _ => id.Equals(Guid.Empty));
            RegisterHydration<TimeyTaskChanged>(Apply, @event => !id.Equals(Guid.Empty) && id.Equals(@event.Id));
        }
        
        public void Apply(TimeyTaskAdded @event)
        {
            if (@event.CustomerId != Guid.Empty)
            {
                id = @event.CustomerId;
                CustomerName = @event.CustomerName;
            }
        }

        public void Apply(TimeyTaskChanged @event)
        {
            if (@event.CustomerId != Guid.Empty)
            {
                CustomerName = @event.CustomerName;
            }
        }
    }
}
