using Common.DTOs;
using DomainLayer.Models.Users;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace Complaint_ticketing.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost(nameof(Login))]
        public ActionResult<UserDto> Login(string userName, string password, [FromServices] UserManagerService userService)
        {
            try
            {
                return Ok(userService.Login(userName, password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(Register))]
        public IActionResult Register(UserDto user, [FromServices] UserManagerService userService)
        {
            try
            {
                userService.AddNewUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(UpdateUserInfo))]
        public IActionResult UpdateUserInfo(UserExtendedDetails userInfo, [FromServices] UserManagerService userService)
        {
            try
            {
                userService.UpdateUserInfo(userInfo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
