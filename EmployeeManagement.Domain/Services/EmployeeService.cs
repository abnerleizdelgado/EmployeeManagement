using AutoMapper;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Interfaces.Services;

namespace EmployeeManagement.Domain.Services
{
    public class EmployeeService : ServiceBase<Employee, EmployeeDTO>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

}
