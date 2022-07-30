using Forum._3.Models;
using System;

namespace OnlineCheckersAPI_0._3.Models
{
    public class Post : IEntityBase
    {

        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
