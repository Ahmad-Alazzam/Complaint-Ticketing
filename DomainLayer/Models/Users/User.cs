using Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserTypeEnum UserType { get; set; }

        [ForeignKey("UserExtendedDetails")]
        public int UserExtendedDetailsId { get; set; }
        public virtual UserExtendedDetails UserDetails { get; set; }
    }
}
