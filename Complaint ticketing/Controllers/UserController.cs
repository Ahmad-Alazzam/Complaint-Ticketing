using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace Complaint_ticketing.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost(nameof(Login))]
        public IActionResult Login(string userName, string password, [FromServices] UserService userService)
        {
            userService.Login(userName, password);
            return Ok();
        }

        [HttpPost(nameof(AddNewUser))]
        public IActionResult AddNewUser(string userName, string password, [FromServices] UserService userService)
        {
            userService.AddNewUser(userName, password);
            return Ok();
        }

        [HttpPost(nameof(UpdateUserInfo))]
        public IActionResult UpdateUserInfo(UserDto user, [FromServices] UserService userService)
        {
            userService.UpdateUserInfo(user);
            return Ok();
        }
    }
}
