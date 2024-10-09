using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace API_Project.Entities
{
#pragma warning disable
    public class User
    {
        [BsonId]
        [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("user_name"),BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [BsonElement("created_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
