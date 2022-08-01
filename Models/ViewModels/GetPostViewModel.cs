using System;

namespace Forum._3.Models.ViewModels
{
    public class GetPostViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreation { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string TopicId { get; set; }
    }
}
