using Forum._3.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forum._3.Models
{
    public class Post : IEntityBase
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreation { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string TopicId { get; set; }
        [JsonIgnore]
        public Topic Topic { get; set; }

    }
}
