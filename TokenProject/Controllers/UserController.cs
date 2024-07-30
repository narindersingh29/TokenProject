
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenProject.dtos;
using TokenProject.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TokenProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
             _logger = logger;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {

            _logger.LogInformation("Get All the Users");
            return Ok(await _userService.GetAllUsers());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByid(int id)
        {
            var userDetails = await _userService.GetUserById(id);

            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            try
            {
                _logger.LogInformation("Post the Users");

                return Ok(await _userService.CreateUser(userDto));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Data Couldnot be saved.");
            }
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UserDto userDto)
        {
            if (userDto != null)
            {
                var isUserUpdated = await _userService.UpdateUser(userDto);
                if (isUserUpdated)
                {
                    return Ok(isUserUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isUserDeleted = await _userService.DeleteUser(id);

            if (isUserDeleted)
            {
                return Ok(isUserDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
