using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public async Task<User> GetByUserAsync(string user)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Username == user);
        }
    }
}
