using Common.Enum;
using DomainLayer.Models.Complaints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Demands
{
    public class Demand
    {
        [Key]
        public int Id { get; set; }

        public string DemandTextAr { get; set; }
        public string DemandTextEn { get; set; }
        public Language SelectedLanguage { get; set; } = Language.Arabic;

        [NotMapped]
        public string DemandText => SelectedLanguage == Language.Arabic ? DemandTextAr : DemandTextEn;
        public bool IsDeleted { get; set; }

        [Required]
        public int ComplaintId { get; set; }
    }
}
