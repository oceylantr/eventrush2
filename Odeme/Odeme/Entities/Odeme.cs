using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Odeme
{
    public class Odeme
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string MusteriAdSoyad { get; set; }

        public string OdemeSaati { get; set; }

        public string OdemeTutari { get; set; }
        
    }
}