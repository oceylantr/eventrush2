using System;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Konak.EventSourcing
{
public class Konfig{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; private set;}

        [JsonProperty]
        public string Subscriber {get; private set;}

        [JsonProperty]
        public string PublishQueue {get; private set;}

    }
}