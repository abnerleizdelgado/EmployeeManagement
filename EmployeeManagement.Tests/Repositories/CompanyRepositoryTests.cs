using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Repository;
using EmployeeManagement.Shared;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace EmployeeManagement.Tests.Repositories
{
    public class CompanyRepositoryTests
    {
        private readonly DataContext _context;
        private readonly CompanyRepository _repository;

        public CompanyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new DataContext(options);
            _repository = new CompanyRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCompany_WhenEntityExists()
        {
            // Arrange
            var company = new Company { Id = 1, Name = "Test Company" };
            await _context.Company.AddAsync(company);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            result.Should().Be(company);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenEntityDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync(99);

            // Assert
            result.Should().BeNull();
        }
    }

}
