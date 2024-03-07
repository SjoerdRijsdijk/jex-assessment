using AutoMapper;
using JexAssessment.Api.Models;
using JexAssessment.Infrastructure.Entities;

namespace JexAssessment.Api.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<SaveCompanyDto, Company>();
        }
    }
}
