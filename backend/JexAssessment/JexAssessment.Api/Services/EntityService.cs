using JexAssessment.Infrastructure;

namespace JexAssessment.Api.Services
{
    public class EntityService(JexContext context) : IEntityService
    {
        public async Task<bool> EntityExists<T>(int id) where T : class
        {
            return (await context.FindAsync<T>(id)) != null;
        }
    }
}
