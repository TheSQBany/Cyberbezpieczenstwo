using System.ComponentModel.DataAnnotations;

namespace Cyber1.Models
{
    public class LoggModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
