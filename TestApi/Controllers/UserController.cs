using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        // GET: api/user/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)
        {
            var classBaseUserDTO = await _userService.InsertUser(user);

            if (classBaseUserDTO.AnyClass == null)
            {
                return BadRequest(classBaseUserDTO.Error);
            }

            return CreatedAtAction("GetUser", new { id = classBaseUserDTO.AnyClass.Id }, classBaseUserDTO.AnyClass);
        }

        // POST: api/user/authentication
        [Route("authentication")]
        [HttpPost]
        public async Task<ActionResult<string>> PostAuthentication(UserDTO user)
        {
            var response = await _userService.Authentication(user);

            if (!response.Equals("OK"))
            {
                return BadRequest(response);
            }

            return Ok();
        }

        // PUT: api/user/1
        [HttpPut("{id}")]
        public ActionResult PutUser(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            string result = _userService.EditUser(user);

            if (!result.Equals("OK"))
            {
                return BadRequest(result);
            }

            return NoContent();
        }

        // DELETE: api/user/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            string result = _userService.RemoveUser(id);

            if (!result.Equals("OK"))
            {
                return BadRequest(result);
            }

            return NoContent();
        }
    }
}
