using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Forum._3.Services.Abstraction;
using Forum._3.Models.ViewModels;
using Forum._3.Data.Abstract;
using Forum._3.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
//using System.Collections.Generic;

namespace Forum._3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicPostController : ControllerBase
    {
        ITopicRepository topicRepository;
        IPostRepository postRepository;
        IUserRepository userRepository;


        public TopicPostController(ITopicRepository topicRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
          
            this.topicRepository = topicRepository;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }


        [Route("topics")]
        [HttpGet]
        public ActionResult<IEnumerable<Topic>> GetTopics()
        {
            var res = topicRepository.GetAll();
            if (res == null) { return BadRequest("No data"); }
            return Ok(res);
        }

        [Route("posts/{id}")]
        [HttpGet]
        public ActionResult<IEnumerable<GetPostViewModel>> GetPosts(string id)
        {

            var res = postRepository.AllIncluding(post => post.TopicId == id, post => post.User);
            
            
            if (res == null) { return BadRequest("No data"); }
            
            var posts = new List<GetPostViewModel>();
            foreach(Post p in res){
                posts.Add(new GetPostViewModel
                {
                    Id = p.Id,
                    Text = p.Text,
                    DateCreation = p.DateCreation,
                    UserId = p.UserId,
                    UserName = p.User.Username,
                    TopicId = p.TopicId,
                });
            }

            PostComparer postComparer = new PostComparer();
            posts.Sort(new PostComparer());
            return Ok(posts);
        }



        [Route("addPost")]
        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public ActionResult<AuthData> AddPost([FromBody] AddPostViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = Guid.NewGuid().ToString();
            var postTemp = new Post
            {
                Id = id,
                UserId = model.UserId,
                DateCreation = DateTime.Now,
                TopicId = model.TopicId,
                Text = model.Text,
            };
            postRepository.Add(postTemp);
            postRepository.Commit();
            return Ok();
        }

        [Route("editPost")]
        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public ActionResult<AuthData> EditPost([FromBody] UpdatePostViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (!postRepository.IsOwner(model.PostId, model.UserId)&&!userRepository.isAdmin(model.UserId)) return Forbid("You are not the owner of this post");

            var postTemp = postRepository.GetSingle(model.PostId);
            postTemp.Text = model.Text;
            postRepository.Update(postTemp);
            postRepository.Commit();
            return Ok();
        }

        [Route("addTopic")]
        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public ActionResult<AuthData> AddTopic([FromBody] AddTopicViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var topicTemp = new Topic
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name
            };
            topicRepository.Add(topicTemp);
            topicRepository.Commit();

            var postTemp = new Post
            {
                Id = Guid.NewGuid().ToString(),
                UserId = model.UserId,
                DateCreation = DateTime.Now,
                TopicId = topicTemp.Id,
                Text = model.Text,
            };
            postRepository.Add(postTemp);
            postRepository.Commit();

            return Ok();
        }
    }
}
