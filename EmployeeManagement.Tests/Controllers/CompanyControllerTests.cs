using EmployeeManagement.Api.Controllers;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Interfaces.Services;
using Moq;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Tests.Controllers
{
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyService> _companyServiceMock;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _companyServiceMock = new Mock<ICompanyService>();
            _controller = new CompanyController(_companyServiceMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenCompanyExists()
        {
            // Arrange
            var companyDto = new CompanyDTO { Id = 1, Name = "Test Company" };
            _companyServiceMock.Setup(s => s.GetAsync())
                               .ReturnsAsync(companyDto);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.As<OkObjectResult>();
            okResult.Value.Should().Be(companyDto);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            _companyServiceMock.Setup(s => s.GetAsync())
                               .ReturnsAsync((CompanyDTO)null);

            // Act
            var result = await _controller.Get();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
