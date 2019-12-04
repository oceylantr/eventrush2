using System;
using Newtonsoft.Json;

namespace Arac.EventSourcing
{
    public class Event {

        public Event(){
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Description = "default desc";
        }

        [JsonProperty]
        public Guid Id {get; private set;}

        [JsonProperty]
        public DateTime CreationDate {get; private set;}

        [JsonProperty]
        public String Description {get; private set;}

        
    }
}