using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.DTOs
{
    public class CompanyDTO : EntityBase
    {
        public string? Cnpj { get; set; }
        public string? Address { get; set; }
        public string? site { get; set; }
    }
}
