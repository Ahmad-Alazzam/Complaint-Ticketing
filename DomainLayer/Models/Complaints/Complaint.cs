using Common.Enum;
using DomainLayer.Models.Demands;
using DomainLayer.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Complaints
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }
        public string ComplaintTextAr { get; set; }
        public string ComplaintTextEn { get; set; }
        public Language SelectedLanguage { get; set; } = Language.Arabic;

        [NotMapped]
        public string ComplaintText => SelectedLanguage == Language.Arabic ? ComplaintTextAr : ComplaintTextEn;

        public ComplaintStatus Status { get; set; } = ComplaintStatus.UnderReview;

        public virtual ICollection<Demand> Demands { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
