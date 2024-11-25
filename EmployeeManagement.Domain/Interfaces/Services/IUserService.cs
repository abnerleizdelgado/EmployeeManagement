using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<UserDTO>
    {
        Task<User> GetByUserAsync(string username);
    }
}
