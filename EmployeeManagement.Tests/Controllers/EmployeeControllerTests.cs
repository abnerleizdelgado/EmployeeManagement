using EmployeeManagement.Api.Controllers;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_employeeServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithEmployees()
        {
            var employees = new List<EmployeeDTO> { new EmployeeDTO { Id = 1, Name = "Colaborador 1" }, new EmployeeDTO { Id = 2, Name = "Colaborador 2" }};
            _employeeServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(employees);

            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEmployees = Assert.IsType<List<EmployeeDTO>>(okResult.Value);
            Assert.Equal(2, returnedEmployees.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkWithEmployee_WhenFound()
        {
            var employee = new EmployeeDTO { Id = 1, Name = "Colaborador 1" };
            _employeeServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(employee);
            var result = await _controller.GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEmployee = Assert.IsType<EmployeeDTO>(okResult.Value);
            Assert.Equal(employee.Id, returnedEmployee.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotFound()
        {
            _employeeServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((EmployeeDTO)null);
            var result = await _controller.GetById(1);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithEmployee()
        {
            var employeeDto = new EmployeeDTO { Id = 1, Name = "Colaborador 1" };
            _employeeServiceMock.Setup(service => service.AddAsync(employeeDto)).ReturnsAsync(employeeDto);
            var result = await _controller.Create(employeeDto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedEmployee = Assert.IsType<EmployeeDTO>(createdResult.Value);
            Assert.Equal(employeeDto.Id, returnedEmployee.Id);
            Assert.Equal("GetById", createdResult.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenValid()
        {
            var employeeDto = new EmployeeDTO { Id = 1, Name = "Colaborador 1" };
            _employeeServiceMock.Setup(service => service.UpdateAsync(employeeDto)).Returns(Task.CompletedTask);
            var result = await _controller.Update(1, employeeDto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var employeeDto = new EmployeeDTO { Id = 2, Name = "Colaborador 2" };
            var result = await _controller.Update(1, employeeDto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenValid()
        {
            _employeeServiceMock.Setup(service => service.DeleteAsync(1)).Returns(Task.CompletedTask);
            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }


    }
}
