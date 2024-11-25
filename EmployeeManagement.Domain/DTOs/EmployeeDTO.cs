
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.DTOs
{
    public class EmployeeDTO : EntityBase
    {
        public string? Cpf { get; set; }
    }
}
