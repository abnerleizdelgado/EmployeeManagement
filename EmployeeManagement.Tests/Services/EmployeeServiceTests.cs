using AutoMapper;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Services;
using Moq;
using System.Xml.Linq;

namespace EmployeeManagement.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly IMapper _mapper;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();

            _service = new EmployeeService(_employeeRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 },
                new Employee("colaborador2", "98765432100", "colaborador2@email.com", "987654321") { Id = 2 }
            };
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEmployee_WhenFound()
        {
            var employee = new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 };
            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);
            var result = await _service.GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task AddAsync_AddsAndReturnsEmployee()
        {
            var employeeDto = new EmployeeDTO { Name = "colaborador1" };
            var employee = new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 };
            _employeeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).ReturnsAsync(employee);
            var result = await _service.AddAsync(employeeDto);
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            var employeeDto = new EmployeeDTO { Id = 1, Name = "Updated Name" };
            _employeeRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);
            await _service.UpdateAsync(employeeDto);
            _employeeRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Employee>(e => e.Id == employeeDto.Id)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryDelete()
        {
            var employee = new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 };
            _employeeRepositoryMock.Setup(repo => repo.DeleteAsync(employee)).Returns(Task.CompletedTask);
            await _service.DeleteAsync(1);
            _employeeRepositoryMock.Verify(repo => repo.DeleteAsync(employee), Times.Once);
        }
    }
}
