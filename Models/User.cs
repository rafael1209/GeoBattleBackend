using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GeoBattleBackend.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("createdAtUtc")]
        public DateTime CreatedAtUtc { get; set; }

        [BsonElement("authToken")]
        public required string AuthToken { get; set; }
    }
}
