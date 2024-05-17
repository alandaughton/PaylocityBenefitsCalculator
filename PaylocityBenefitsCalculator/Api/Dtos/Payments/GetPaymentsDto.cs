using Api.Dtos.Dependent;

namespace Api.Dtos.Payments
{
    public class GetPaymentsDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Salary { get; set; }
        public ICollection<decimal> Payments { get; set; } = new List<decimal>();
    }
}
