using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Konak.EventSourcing
{
    public class SubscriptionManager
    {
        private readonly IMongoCollection<Konfig> _konfigs;
        private readonly IConfiguration _configuration;

        public SubscriptionManager(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["SubscriberSettings:ConnectionString"]);
            var database = client.GetDatabase(_configuration["SubscriberSettings:DatabaseName"]);
            _konfigs = database.GetCollection<Konfig>(_configuration["SubscriberSettings:CollectionName"]);
        }

        public string subscribeServicesFor(IEventBus eventBus, string subscriber){

            List<Konfig> konfigs =_konfigs.Find(konfig => konfig.Subscriber.Equals("Konak")).ToList();

            foreach (var konf in konfigs)
            {
                eventBus.Subscribe(konf.PublishQueue);
            }

            return "success";

        }
    }
}