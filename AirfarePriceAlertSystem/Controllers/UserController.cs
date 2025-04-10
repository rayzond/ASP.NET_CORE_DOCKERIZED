using Microsoft.AspNetCore.Mvc;
using AirfarePriceAlertSystem.Models;
using AirfarePriceAlertSystem.Services;

namespace AirfarePriceAlertSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UID }, createdUser);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.UID)
                return BadRequest("ID mismatch.");

            var updated = _userService.UpdateUser(user);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userService.DeleteUser(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
