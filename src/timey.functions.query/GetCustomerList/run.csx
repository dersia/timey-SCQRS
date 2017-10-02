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

public static HttpResponseMessage Run(HttpRequestMessage req, IEnumerable<Microsoft.Azure.Documents.Document> documents, TraceWriter log)
{
    var list = new CustomerList();
    foreach(var doc in documents) 
    {
        list.Customers.Add(ModelBase.FromDocument<Customer>(doc));
    }
    return req.CreateResponse(HttpStatusCode.OK, list);
}
