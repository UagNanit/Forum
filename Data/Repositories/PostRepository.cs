using Forum._3.Data.Abstract;
using Forum._3.Models;



namespace Forum._3.Data.Repositories {
    public class PostRepository : EntityBaseRepository<Post>, IPostRepository
    {
        public PostRepository(DBContext context) : base (context) { }

        public bool IsOwner(string postId, string userId)
        {
            var post = this.GetSingle(postId);
           
            return post.UserId == userId;
        }
    }
}