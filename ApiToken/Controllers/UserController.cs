using ApiToken.Model;
using ApiToken.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiToken.Controllers
{
    [Authorize(Roles = "Menager")]
    [Route("api/Estudo")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSevice _user;

        public UserController(IUserSevice user)
        {
            _user = user;
        }

        [HttpPost]
        public IActionResult AddUser(Login user)
        {
            _user.Add(user);
            return Ok("User Inserted successfully!");
        }
    }
}
