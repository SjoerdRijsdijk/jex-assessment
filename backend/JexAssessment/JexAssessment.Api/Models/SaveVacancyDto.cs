using System.ComponentModel.DataAnnotations;

namespace JexAssessment.Api.Models
{
    public class SaveVacancyDto
    {
        [Required]
        public int? CompanyId { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
