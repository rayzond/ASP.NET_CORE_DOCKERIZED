using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirfarePriceAlertSystem.Models
{
    public class UserAlert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment primary key
        public int ID { get; set; }

        public int UserUID { get; set; } // Foreign key reference to User
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal MaxPrice { get; set; }
        public int MaxConnections { get; set; } = 0;
    }
}
