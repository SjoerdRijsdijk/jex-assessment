namespace JexAssessment.Api.Models
{
    public class ListResponseDto<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
