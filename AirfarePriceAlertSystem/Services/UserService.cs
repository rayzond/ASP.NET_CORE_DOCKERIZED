using System.Collections.Generic;
using AirfarePriceAlertSystem.Data;
using AirfarePriceAlertSystem.Models;

namespace AirfarePriceAlertSystem.Services
{
    public class UserService
    {
        private readonly UserDAO _userDAO;

        public UserService(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public List<User> GetAllUsers()
        {
            return _userDAO.GetAllUsers();
        }

        public User? GetUserById(int id)
        {
            return _userDAO.GetUserById(id);
        }

        public User CreateUser(User user)
        {
            return _userDAO.CreateUser(user);
        }

        public bool UpdateUser(User user)
        {
            return _userDAO.UpdateUser(user);
        }

        public bool DeleteUser(int id)
        {
            return _userDAO.DeleteUser(id);
        }
    }
}
