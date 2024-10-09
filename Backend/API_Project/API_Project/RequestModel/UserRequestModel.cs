using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace API_Project.RequestModel
{
    public class UserRequestModel
    {
        [BsonElement("user_name")]
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [BsonElement("email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
    }
}
