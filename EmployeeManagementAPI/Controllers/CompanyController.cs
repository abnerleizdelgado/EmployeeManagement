using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var onlyOneCompany = await _companyService.GetAsync();
            return Ok(onlyOneCompany);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDTO company)
        {
            var companyDb = await _companyService.AddAsync(company);
            return CreatedAtAction(nameof(Get), new { id = companyDb.Id }, companyDb);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Company company)
        {
            if (id != company.Id) return BadRequest("ID mismatch.");
            await _companyService.UpdateAsync(company);

            return NoContent();
        }


    }
}
