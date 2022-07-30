using Forum._3.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forum._3.Models
{
    public class Section : IEntityBase
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Topic> Topics { get; set; }
     
        public Section()
        {
            Topics = new List<Topic>();
        }
    }
}
