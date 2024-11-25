using AutoMapper;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Services;
using Moq;
using FluentAssertions;


namespace EmployeeManagement.Tests.Services
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CompanyService _service;

        public CompanyServiceTests()
        {
            _repositoryMock = new Mock<ICompanyRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new CompanyService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedDto_WhenEntityExists()
        {
            // Arrange
            var companyId = 1;
            var company = new Company { Id = companyId, Name = "Test Company" };
            var companyDto = new CompanyDTO { Id = companyId, Name = "Test Company" };

            _repositoryMock.Setup(r => r.GetByIdAsync(companyId))
                           .ReturnsAsync(company);
            _mapperMock.Setup(m => m.Map<CompanyDTO>(company))
                       .Returns(companyDto);

            // Act
            var result = await _service.GetByIdAsync(companyId);

            // Assert
            result.Should().Be(companyDto);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenEntityDoesNotExist()
        {
            // Arrange
            var companyId = 1;
            _repositoryMock.Setup(r => r.GetByIdAsync(companyId))
                           .ReturnsAsync((Company)null);

            // Act
            var result = await _service.GetByIdAsync(companyId);

            // Assert
            result.Should().BeNull();
        }
    }

}
