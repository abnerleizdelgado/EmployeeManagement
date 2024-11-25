using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<Company> GetAsync();

    }
}
