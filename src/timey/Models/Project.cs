using Newtonsoft.Json;
using System;
using timey.Events;
using timey.Models.Infrastructure;

namespace timey.Models
{
    public class Project : ModelBase
    {
        [JsonProperty("projectName")]
        public string ProjectName { get; private set; }

        public Project()
        {
            RegisterHydration<TimeyTaskAdded>(Apply, _ => id.Equals(Guid.Empty));
            RegisterHydration<TimeyTaskChanged>(Apply, @event => !id.Equals(Guid.Empty) && id.Equals(@event.Id));
        }

        public void Apply(TimeyTaskAdded @event)
        {
            if (@event.ProjectId != Guid.Empty)
            {
                id = @event.ProjectId;
                ProjectName = @event.ProjectName;
            }
        }
        public void Apply(TimeyTaskChanged @event)
        {
            if (@event.ProjectId != Guid.Empty)
            {
                ProjectName = @event.ProjectName;
            }
        }
    }
}
