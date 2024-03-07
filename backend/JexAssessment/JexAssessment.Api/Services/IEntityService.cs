
namespace JexAssessment.Api.Services
{
    public interface IEntityService
    {
        Task<bool> EntityExists<T>(int id) where T : class;
    }
}