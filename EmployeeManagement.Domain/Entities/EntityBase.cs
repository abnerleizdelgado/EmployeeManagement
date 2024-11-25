using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
