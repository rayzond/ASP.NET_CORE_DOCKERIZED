using System.Collections.Generic;
using AirfarePriceAlertSystem.Data;
using AirfarePriceAlertSystem.Models;

namespace AirfarePriceAlertSystem.Services
{
    public class UserAlertService
    {
        private readonly UserAlertDAO _userAlertDAO;

        public UserAlertService(UserAlertDAO userAlertDAO)
        {
            _userAlertDAO = userAlertDAO;
        }

        public List<UserAlert> GetAllUserAlerts()
        {
            return _userAlertDAO.GetAllUserAlerts();
        }

        public UserAlert? GetUserAlertById(int id)
        {
            return _userAlertDAO.GetUserAlertById(id);
        }

        public List<UserAlert> GetUserAlertsByUserId(int userId)
        {
            return _userAlertDAO.GetUserAlertsByUserId(userId);
        }

        public UserAlert CreateUserAlert(UserAlert userAlert)
        {
            return _userAlertDAO.CreateUserAlert(userAlert);
        }

        public bool UpdateUserAlert(UserAlert userAlert)
        {
            return _userAlertDAO.UpdateUserAlert(userAlert);
        }

        public bool DeleteUserAlert(int id)
        {
            return _userAlertDAO.DeleteUserAlert(id);
        }

        public bool DeleteAllUserAlertsByUserId(int userId)
        {
            return _userAlertDAO.DeleteAllUserAlertsByUserId(userId);
        }
    }
}
