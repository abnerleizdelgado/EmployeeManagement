namespace EmployeeManagement.Domain.Entities
{
    public class Employee : EntityBase
    {
        public string? Cpf { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        public Employee(string name, string? cpf, string? email, string? phone)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
            CreateDate = DateTime.UtcNow;
        }

        public void Update(string name, string? cpf, string? email, string? phone)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
