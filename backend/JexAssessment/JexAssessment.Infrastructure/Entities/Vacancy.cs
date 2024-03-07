namespace JexAssessment.Infrastructure.Entities
{
    public class Vacancy : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
