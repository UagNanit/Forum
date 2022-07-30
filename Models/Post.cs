using Forum._3.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Forum._3.Models
{
    public class Post : IEntityBase
    {

        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreation { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
