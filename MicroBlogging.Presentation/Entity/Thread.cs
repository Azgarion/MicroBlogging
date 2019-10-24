using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroBlogging.Entity
{
    public class Thread
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        public string Value { get; set; }
    }
}