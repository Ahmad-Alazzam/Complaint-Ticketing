using Common.Enum;

namespace Common.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserExtendedDetailsDto UserDetails { get; set; }
    }
}
