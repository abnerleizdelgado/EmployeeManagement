using AutoMapper;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Interfaces.Services;

namespace EmployeeManagement.Domain.Services
{
    public class CompanyService : ServiceBase<Company, CompanyDTO>, ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task<CompanyDTO> GetAsync()
        {
            var company = await _repository.GetAsync();
            return _mapper.Map<CompanyDTO>(company);
        }
    }

}
