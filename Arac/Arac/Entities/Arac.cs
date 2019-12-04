using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Arac
{
    public class Arac
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string MusteriAdSoyad { get; set; }

        public string AracMarka { get; set; }

        public string TeslimSaati { get; set; }
        
    }
}