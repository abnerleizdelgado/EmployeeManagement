namespace EmployeeManagement.Domain.Entities
{
    public class Company : EntityBase
    {
        public string? Cnpj { get; set; }
        public string? Address { get; set; }
        public string? site { get; set; }
    }
}
