using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        [StringLength(128)]
        public string Code { get; set; }

        [StringLength(512)]
        public string Name { get; set; }

        [StringLength(1028)]
        public string? Description { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}