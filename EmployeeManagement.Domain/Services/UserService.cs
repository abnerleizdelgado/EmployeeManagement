using AutoMapper;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Interfaces.Services;
using EmployeeManagement.Domain.Services;

public class UserService : ServiceBase<User, UserDTO>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IMapper mapper)
        : base(userRepository, mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByUserAsync(string username)
    {
        return await _userRepository.GetByUserAsync(username);
    }
}
