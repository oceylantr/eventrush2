using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Konak
{
    public class Konak
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string MusteriAdSoyad { get; set; }

        public string Otel { get; set; }

        public string GirisSaati { get; set; }
        
    }
}