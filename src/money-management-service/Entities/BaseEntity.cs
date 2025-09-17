using System.ComponentModel.DataAnnotations;

namespace money_management_service.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        [MaxLength(50)]
        public string UpdatedBy { get; set; }
    }
}
