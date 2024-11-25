using EmployeeManagement.Domain.Entities;


namespace EmployeeManagement.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByUserAsync(string user);
    }
}
