using Forum._3.Models;
using System.Collections.Generic;

namespace OnlineCheckersAPI_0._3.Models
{
    public class Section : IEntityBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }
        public Section()
        {
            Topics = new List<Topic>();
        }
    }
}
