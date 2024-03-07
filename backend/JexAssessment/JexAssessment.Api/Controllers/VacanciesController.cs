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
    public class VacanciesController(JexContext context, IMapper mapper, IEntityService entityService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<ListResponseDto<VacancyDto>>> GetVacancies([FromQuery] GetVacanciesDto model)
        {
            var query = context.Vacancies.AsQueryable();
            if (model.CompanyId.HasValue)
            {
                query = query.Where(vacancy => vacancy.CompanyId == model.CompanyId);
            }

            var pagedDbVacancies = await query.ToListAsync();
            var vacancies = pagedDbVacancies.Select(mapper.Map<VacancyDto>);
            return Ok(new ListResponseDto<VacancyDto>
            {
                Items = vacancies
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VacancyDto>> GetVacancy([FromRoute] int id)
        {
            var dbVacancy = await context.Vacancies.FindAsync(id);
            if (dbVacancy == null)
            {
                return NotFound();
            }

            var vacancy = mapper.Map<VacancyDto>(dbVacancy);
            return Ok(vacancy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancy([FromRoute] int id, [FromBody] SaveVacancyDto model)
        {
            var dbVacancy = await context.Vacancies.FindAsync(id);
            if (dbVacancy == null)
            {
                return NotFound();
            }

            if (model.CompanyId.HasValue && !await entityService.EntityExists<Company>(model.CompanyId.Value))
            {
                return Problem(
                    title: "Not found",
                    detail: "The given company doesn't exist",
                    statusCode: StatusCodes.Status404NotFound);
            }

            try
            {
                dbVacancy = mapper.Map(model, dbVacancy);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await entityService.EntityExists<Vacancy>(id))
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
        public async Task<ActionResult<VacancyDto>> PostVacancy(SaveVacancyDto model)
        {
            if (model.CompanyId.HasValue && !await entityService.EntityExists<Company>(model.CompanyId.Value))
            {
                return Problem(
                    title: "Not found",
                    detail: "The given company doesn't exist",
                    statusCode: StatusCodes.Status404NotFound);
            }

            var dbVacancy = mapper.Map<Vacancy>(model);
            await context.Vacancies.AddAsync(dbVacancy);
            await context.SaveChangesAsync();

            var vacancy = mapper.Map<VacancyDto>(dbVacancy);

            return CreatedAtAction(nameof(GetVacancy), new CreatedDto { Id = dbVacancy.Id }, vacancy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy([FromRoute] int id)
        {
            var vacancy = await context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            context.Vacancies.Remove(vacancy);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
