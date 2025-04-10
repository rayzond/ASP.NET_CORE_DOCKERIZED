using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AirfarePriceAlertSystem.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment primary key
        public int UID { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;

        public List<UserAlert> Alerts { get; set; } = new();
    }
}
