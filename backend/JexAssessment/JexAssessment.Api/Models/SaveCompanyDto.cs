using System.ComponentModel.DataAnnotations;

namespace JexAssessment.Api.Models
{
    public class SaveCompanyDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
