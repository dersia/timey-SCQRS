#r "../bin/timey.dll"
#r "../bin/Newtonsoft.Json.dll"
#r "System.ServiceModel"
#r "System.Web"
#r "Microsoft.ServiceBus"

using System.Net;
using timey.Commands;
using timey.Handlers;
using Newtonsoft.Json;
using System.ServiceModel.Channels;
using System.Web;
using System.Security.Claims;
using System.Linq;
using Microsoft.ServiceBus.Messaging;
using timey.Events.Infrastructure;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, string userId, IAsyncCollector<EventData> outputEvents, ILogger log)
{
    var jsonAddTimeyTaskCommand = await req.Content.ReadAsStringAsync();
    var addTimeyTaskCommand = JsonConvert.DeserializeObject<AddTimeyTask>(jsonAddTimeyTaskCommand);
    
    var timeyTaskHandler = new TimeyTaskHandler();
    var addTimeyTaskEvents = timeyTaskHandler.Handle(addTimeyTaskCommand);

    await SaveEvents(addTimeyTaskEvents, outputEvents);
    return req.CreateResponse(HttpStatusCode.OK); 
}

public static async Task SaveEvents(IEnumerable<EventBase> @events, IAsyncCollector<EventData> outputEvents) 
{
    foreach(var @event in @events) 
    {
        var jsonEvent = JsonConvert.SerializeObject(@event);
        var eventData = new EventData(System.Text.Encoding.UTF8.GetBytes(jsonEvent));
        eventData.Properties.Add("CLR-TYPE", @event.GetType().FullName);
        await outputEvents.AddAsync(eventData);
    }
}
