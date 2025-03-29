using System.Collections.Generic;
using System.Linq;
using AirfarePriceAlertSystem.Models;

namespace AirfarePriceAlertSystem.Data
{
    public class UserAlertDAO
    {
        private readonly ApplicationDbContext _context;

        public UserAlertDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserAlert> GetAllUserAlerts()
        {
            return _context.UserAlerts.ToList();
        }

        public UserAlert? GetUserAlertById(int id)
        {
            return _context.UserAlerts.Find(id);
        }

        public List<UserAlert> GetUserAlertsByUserId(int userId)
        {
            return _context.UserAlerts.Where(a => a.UserUID == userId).ToList();
        }

        public UserAlert CreateUserAlert(UserAlert userAlert)
        {
            _context.UserAlerts.Add(userAlert);
            _context.SaveChanges();
            return userAlert;
        }

        public bool UpdateUserAlert(UserAlert userAlert)
        {
            var existingAlert = _context.UserAlerts.Find(userAlert.ID);
            if (existingAlert == null)
                return false;

            existingAlert.From = userAlert.From;
            existingAlert.To = userAlert.To;
            existingAlert.MaxPrice = userAlert.MaxPrice;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteUserAlert(int id)
        {
            var userAlert = _context.UserAlerts.Find(id);
            if (userAlert == null)
                return false;

            _context.UserAlerts.Remove(userAlert);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteAllUserAlertsByUserId(int userId)
        {
            var userAlerts = _context.UserAlerts.Where(a => a.UserUID == userId).ToList();
            if (userAlerts.Count == 0)
                return false;

            _context.UserAlerts.RemoveRange(userAlerts);
            _context.SaveChanges();
            return true;
        }
    }
}
