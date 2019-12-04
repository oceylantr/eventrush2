using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ucus
{
    public class Ucus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string MusteriAdSoyad { get; set; }

        public string UcusSaati { get; set; }

        public string KalkisYeri { get; set; }
        
        public string VarisYeri { get; set; }

        public string OdemeTutari { get; set; }
    }
}