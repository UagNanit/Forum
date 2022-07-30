using System.Collections.Generic;

namespace Forum._3.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
