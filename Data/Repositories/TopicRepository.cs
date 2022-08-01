using Forum._3.Data.Abstract;
using Forum._3.Models;



namespace Forum._3.Data.Repositories {
    public class TopicRepository : EntityBaseRepository<Topic>, ITopicRepository
    {

        public TopicRepository(DBContext context) : base (context) { }

       

       
    }
}