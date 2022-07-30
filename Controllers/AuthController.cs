using System;
using Forum._3.Services.Abstraction;
using Forum._3.Models.ViewModels;
using Forum._3.Data.Abstract;
using Forum._3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Forum._3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        IAuthService authService;
        IUserRepository userRepository;
        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            this.authService = authService;
            this.userRepository = userRepository;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult<AuthData> Post([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = userRepository.GetSingle(u => u.Email == model.Email, u => u.Role);

            if (user == null) {
                return BadRequest(new { email = "no user with this email" });
            }

            var passwordValid = authService.VerifyPassword(model.Password, user.Password);
            if (!passwordValid) {
                return BadRequest(new { password = "invalid password" });
            }

            return authService.GetAuthData(user);
        }

        [Route("register")]
        [HttpPost]
        public ActionResult<AuthData> Post([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var emailUniq = userRepository.isEmailUniq(model.Email);
            if (!emailUniq) return BadRequest(new { email = "user with this email already exists" });
            var usernameUniq = userRepository.IsUsernameUniq(model.Username);
            if (!usernameUniq) return BadRequest(new { username = "user with this name already exists" });

            var id = Guid.NewGuid().ToString();
            var userTemp = new User
            {
                Id = id,
                Username = model.Username,
                Email = model.Email,
                Password = authService.HashPassword(model.Password),
                RoleId = "2",
                
            };
            userRepository.Add(userTemp);
            userRepository.Commit();

            var user = userRepository.GetSingle(u => u.Email == model.Email, u => u.Role);

            return authService.GetAuthData(user);
        }

        [Route("auth/{id}")]
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public ActionResult<UserViewModel> GetAuth(string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = userRepository.GetSingle(id);

            if (user == null)
            {
                return BadRequest(new { username = "user do not exist" });
            }

            UserViewModel userViewModel = new UserViewModel
            {
                Name = user.Username,
                Email = user.Email
            };

            return userViewModel;
        }
    }
}