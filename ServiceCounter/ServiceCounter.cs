using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceCounter
{
    public class ServiceCounter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("izvedenKlic")]
        [JsonPropertyName("izvedenKlic")]
        public string? izvedenKlic { get; set; }
        [BsonElement("casKlica")]
        [JsonPropertyName("casKlica")]
        public DateTime? casKlica { get; set; }
        [BsonElement("counter")]
        [JsonPropertyName("counter")]
        public int? counter { get; set; }

    }
}
