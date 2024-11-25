using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Repository;
using EmployeeManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Tests.Repositories
{
    public class EmployeeRepositoryTests
    {
        private readonly DataContext _context;
        private readonly EmployeeRepository _repository;

        public EmployeeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("EmployeeTestDb")
                .Options;

            _context = new DataContext(options);
            _repository = new EmployeeRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEmployees()
        {
            _context.Employees.AddRange(
                new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 },
                new Employee("colaborador2", "98765432100", "colaborador2@email.com", "987654321") { Id = 2 }
            );
            await _context.SaveChangesAsync();
            var result = await _repository.GetAllAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEmployee_WhenFound()
        {
            var employee = new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            var result = await _repository.GetByIdAsync(employee.Id);
            Assert.NotNull(result);
            Assert.Equal(employee.Name, result.Name);
        }

        [Fact]
        public async Task AddAsync_AddsEmployeeToDatabase()
        {
            var employee = new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 };
            await _repository.AddAsync(employee);
            var result = await _context.Employees.FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(employee.Name, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEmployeeInDatabase()
        {
            var employee = new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            employee.Name = "Updated Name";
            await _repository.UpdateAsync(employee);
            var result = await _context.Employees.FindAsync(employee.Id);
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEmployeeFromDatabase()
        {
            var employee = new Employee("colaborador1", "12345678901", "colaborador1@email.com", "123456789") { Id = 1 };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            await _repository.DeleteAsync(employee);
            var result = await _context.Employees.FindAsync(employee.Id);
            Assert.Null(result);
        }
    }
}
