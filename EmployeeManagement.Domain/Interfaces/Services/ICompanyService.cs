using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Interfaces.Services
{
    public interface ICompanyService : IServiceBase<CompanyDTO>
    {
        Task<CompanyDTO> GetAsync();
    }
}
