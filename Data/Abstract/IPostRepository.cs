using Forum._3.Models;
using System.Security.Claims;

namespace Forum._3.Data.Abstract
{
    public interface IPostRepository : IEntityBaseRepository<Post>
    {
        public bool IsOwner(string postId, string userId);


    }
}