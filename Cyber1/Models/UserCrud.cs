using System.ComponentModel.DataAnnotations;

namespace Cyber1.Models
{
    public class UserCrud
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
