
using System.Text.Json.Serialization;

namespace Forum._3.Models
{
    public class User : IEntityBase
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}