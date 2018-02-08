using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using timey.Events.Infrastructure;

namespace timey.Models.Infrastructure
{
    public abstract class ModelBase
    {
        protected IDictionary<Type, List<(Func<EventBase, bool> applyIf, Action<EventBase> hydration)>> _hydraters = new Dictionary<Type, List<(Func<EventBase, bool> applyIf, Action<EventBase> hydration)>>();
        protected Func<EventBase, bool> DefaultApplyIf() => @event => !id.Equals(Guid.Empty) && id.Equals(@event.Id);

        [JsonProperty("id")]
        public Guid id { get; protected set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get => GetType().Name; }

        public void RegisterHydration<T>(Action<T> hydrater, Func<EventBase, bool> applyIf) where T : EventBase
        {
            if (!_hydraters.TryGetValue(typeof(T), out var hydraters))
            {
                hydraters = new List<(Func<EventBase, bool> applyIf, Action<EventBase> hydration)>();
                _hydraters.Add(typeof(T), hydraters);
            }
            hydraters.Add((applyIf, @event => hydrater(@event as T)));
        }

        public void ApplyEvent(EventBase @event)
        {
            if (_hydraters.TryGetValue(@event.GetType(), out var hydraters))
            {
                foreach (var hydrater in hydraters)
                    if (hydrater.applyIf(@event))
                        hydrater.hydration(@event);
            }
        }

        public void ApplyEvents(params EventBase[] @events)
        {
            foreach (var @event in @events)
                ApplyEvent(@event);
        }

        public void ApplyEvents(IEnumerable<EventBase> @events)
        {
            ApplyEvents(events.ToArray());
        }
    }
}
