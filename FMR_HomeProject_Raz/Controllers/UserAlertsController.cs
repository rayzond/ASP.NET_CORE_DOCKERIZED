using Microsoft.AspNetCore.Mvc;
using AirfarePriceAlertSystem.Models;
using AirfarePriceAlertSystem.Services;

namespace AirfarePriceAlertSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAlertsController : ControllerBase
    {
        private readonly UserAlertService _userAlertService;

        public UserAlertsController(UserAlertService userAlertService)
        {
            _userAlertService = userAlertService;
        }

        // GET: api/UserAlerts
        [HttpGet]
        public ActionResult<IEnumerable<UserAlert>> GetAllUserAlerts()
        {
            return Ok(_userAlertService.GetAllUserAlerts());
        }

        // GET: api/UserAlerts/{id}
        [HttpGet("{id}")]
        public ActionResult<UserAlert> GetUserAlert(int id)
        {
            var alert = _userAlertService.GetUserAlertById(id);
            if (alert == null)
                return NotFound();
            return Ok(alert);
        }

        // GET: api/UserAlerts/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<UserAlert>> GetUserAlertsByUserId(int userId)
        {
            var alerts = _userAlertService.GetUserAlertsByUserId(userId);
            return Ok(alerts);
        }

        // POST: api/UserAlerts
        [HttpPost]
        public ActionResult<UserAlert> CreateUserAlert(UserAlert userAlert)
        {
            var createdAlert = _userAlertService.CreateUserAlert(userAlert);
            return CreatedAtAction(nameof(GetUserAlert), new { id = createdAlert.ID }, createdAlert);
        }

        // PUT: api/UserAlerts/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUserAlert(int id, UserAlert userAlert)
        {
            if (id != userAlert.ID)
                return BadRequest("ID mismatch.");

            var updated = _userAlertService.UpdateUserAlert(userAlert);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/UserAlerts/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUserAlert(int id)
        {
            var deleted = _userAlertService.DeleteUserAlert(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/UserAlerts/user/{userId}
        [HttpDelete("user/{userId}")]
        public IActionResult DeleteAllUserAlerts(int userId)
        {
            var deleted = _userAlertService.DeleteAllUserAlertsByUserId(userId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
