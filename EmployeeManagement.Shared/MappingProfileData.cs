using AutoMapper;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Shared
{
    public class MappingProfileData : Profile
    {
        public MappingProfileData()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
