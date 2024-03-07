using AutoMapper;
using JexAssessment.Api.Models;
using JexAssessment.Api.Services;
using JexAssessment.Infrastructure;
using JexAssessment.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JexAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController(JexContext context, IMapper mapper, IEntityService entityService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<ListResponseDto<CompanyDto>>> GetCompanies([FromQuery] GetCompaniesDto model)
        {
            var query = context.Companies
                .AsQueryable();
            if (model.HasVacancies.HasValue)
            {
                query = query.Where(c => c.Vacancies.Count != 0 == model.HasVacancies);
            }

            var dbCompanies = await query.ToListAsync();
            var companies = dbCompanies.Select(mapper.Map<CompanyDto>);
            return Ok(new ListResponseDto<CompanyDto>
            {
                Items = companies
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany([FromRoute] int id)
        {
            var dbCompany = await context.Companies.FindAsync(id);
            if (dbCompany == null)
            {
                return NotFound();
            }

            var company = mapper.Map<CompanyDto>(dbCompany);
            return Ok(company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany([FromRoute] int id, [FromBody] SaveCompanyDto model)
        {
            var dbCompany = await context.Companies.FindAsync(id);
            if (dbCompany == null)
            {
                return NotFound();
            }

            try
            {
                dbCompany = mapper.Map(model, dbCompany);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await entityService.EntityExists<Company>(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(SaveCompanyDto model)
        {
            var dbCompany = mapper.Map<Company>(model);

            await context.Companies.AddAsync(dbCompany);
            await context.SaveChangesAsync();

            var company = mapper.Map<CompanyDto>(dbCompany);
            return CreatedAtAction(nameof(GetCompany), new CreatedDto { Id = dbCompany.Id }, company);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] int id)
        {
            var company = await context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            context.Companies.Remove(company);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
