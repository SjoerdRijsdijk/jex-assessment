using AutoMapper;
using JexAssessment.Api.Models;
using JexAssessment.Infrastructure.Entities;

namespace JexAssessment.Api.Mapping
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<Vacancy, VacancyDto>();
            CreateMap<SaveVacancyDto, Vacancy>();
        }
    }
}
