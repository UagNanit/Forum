using Forum._3.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnlineCheckersAPI_0._3.Models
{
    public class Topic : IEntityBase
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Post> Posts { get; set; }

        public Topic()
        {
            Posts = new List<Post>();
        }

    }
}
