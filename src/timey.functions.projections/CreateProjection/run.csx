#r "../bin/timey.dll"
#r "../bin/Newtonsoft.Json.dll"
#r "System.ServiceModel"
#r "System.Web"
#r "Microsoft.ServiceBus"
#r "Microsoft.Azure.Documents.Client"

using Newtonsoft.Json;
using System.Net;
using System.ServiceModel.Channels;
using System.Web;
using System.Security.Claims;
using System.Linq;
using Microsoft.ServiceBus.Messaging;
using Microsoft.Azure.Documents.Client;
using timey.Commands;
using timey.Handlers;
using timey.Events.Infrastructure;
using timey.Events;
using timey.Models.Infrastructure;
using timey.Models; 

public static async Task Run(EventData eventHubMessage, DocumentClient inputProjections, IAsyncCollector<TimeyEvent> outputEvents, IAsyncCollector<ModelBase> outputProjections, ILogger log)
{
    Type eventType = typeof(EventBase).Assembly.GetType($"{eventHubMessage.Properties["CLR-TYPE"]}");

    var @event = JsonConvert.DeserializeObject(System.Text.Encoding.UTF8.GetString(eventHubMessage.GetBytes()), eventType) as EventBase;

    await outputEvents.AddAsync(TimeyEvent.FromEventData(eventHubMessage));

    switch(@event) 
    { 
        case TimeyTaskAdded taskAdded:
            log.LogInformation(taskAdded.Id.ToString());
            await Handle<TimeyTask>(taskAdded, taskAdded.Id, inputProjections, outputProjections, log);
            await Handle<Customer>(taskAdded, taskAdded.CustomerId, inputProjections, outputProjections, log);
            await Handle<Project>(taskAdded, taskAdded.ProjectId, inputProjections, outputProjections, log);
            await Handle<Budget>(taskAdded, taskAdded.BudgetId, inputProjections, outputProjections, log);
        break;
        case TimeyTaskChanged taskChanged:
            log.LogInformation(taskChanged.Id.ToString());
            await Handle<TimeyTask>(taskChanged, taskChanged.Id, inputProjections, outputProjections, log);
            await Handle<Customer>(taskChanged, taskChanged.CustomerId, inputProjections, outputProjections, log);
            await Handle<Project>(taskChanged, taskChanged.ProjectId, inputProjections, outputProjections, log);
            await Handle<Budget>(taskChanged, taskChanged.BudgetId, inputProjections, outputProjections, log);
        break;
        case WorkEnded workEnded:
            log.LogInformation(workEnded.Id.ToString());
            await Handle<Work>(workEnded, workEnded.Id, inputProjections, outputProjections, log);
        break;
        case WorkStarted workStarted:
            log.LogInformation(workStarted.Id.ToString());
            await Handle<Work>(workStarted, workStarted.Id, inputProjections, outputProjections, log);
        break;
    }
}

public static async Task Handle<T>(EventBase @event, Guid checkId, DocumentClient inputProjections, IAsyncCollector<ModelBase> outputProjections, ILogger log) where T : ModelBase, new()
{
    ModelBase model = new T();
    var doc = inputProjections.CreateDocumentQuery(
        UriFactory.CreateDocumentCollectionUri("projections", "projections"),
        new FeedOptions { MaxItemCount = -1 }).ToList()
        .Where(d => d.Id == checkId.ToString()) 
        .FirstOrDefault();
    if(doc != null) 
    { 
        model = ModelBase.FromDocument<T>(doc) as ModelBase;
    }
    model.ApplyEvent(@event); 
    await outputProjections.AddAsync(model);
}

public class TimeyEvent 
{
    public long SerializedSizeInBytes { get; set; }
    public string Offset { get; set; }
    public string PartitionKey { get; set; }
    public long SequenceNumber { get; set; }
    public DateTime EnqueuedTimeUtc { get; set; }
    public IDictionary<string,object> Properties { get; set; }
    public IDictionary<string,object> SystemProperties { get; set; }
    public string Body { get; set; }

    public static TimeyEvent FromEventData(EventData @event) 
    {
        var timeyEvent = JsonConvert.DeserializeObject<TimeyEvent>(JsonConvert.SerializeObject(@event));
        timeyEvent.Body = System.Convert.ToBase64String(@event.GetBytes());
        return timeyEvent;
    }
}