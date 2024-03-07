namespace JexAssessment.Infrastructure.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
