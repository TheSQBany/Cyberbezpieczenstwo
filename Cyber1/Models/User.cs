using MessagePack;

namespace Cyber1.Models
{
    public class User
    {
        public string? Password { get; set; }

        public string? NewPassword { get; set; }

        public string? Token { get; set; }
    }
}
