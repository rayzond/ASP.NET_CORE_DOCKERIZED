using System.Collections.Generic;
using System.Linq;
using AirfarePriceAlertSystem.Models;

namespace AirfarePriceAlertSystem.Data
{
    public class UserDAO
    {
        private readonly ApplicationDbContext _context;

        public UserDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.UID);
            if (existingUser == null)
                return false;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Age = user.Age;
            existingUser.Country = user.Country;
            existingUser.Currency = user.Currency;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
