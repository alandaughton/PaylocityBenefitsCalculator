namespace Api.Dtos.Payments
{
    public class GetPaymentsDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Salary { get; set; }
        public decimal[] Payments { get; set; } = new decimal[0];
    }
}
