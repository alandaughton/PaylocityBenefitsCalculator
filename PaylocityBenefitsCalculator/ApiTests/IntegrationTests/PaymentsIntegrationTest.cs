namespace ApiTests.IntegrationTests
{
    using Api.Dtos.Payments;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class PaymentsIntegrationTest : IntegrationTest
    {
        [Fact]
        public async Task WhenAskedForAllPayments_LeBronJames_ShouldReturnAllPayments()
        {
            var response = await HttpClient.GetAsync("/api/v1/payments/1");
            var payments = new GetPaymentsDto()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                Payments = new decimal[]
                    {
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.26m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.26m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.27m,
                        2439.26m,
                        2439.27m,
                        2439.27m
                    }
            };

            await response.ShouldReturn(HttpStatusCode.OK, payments);
        }

        [Fact]
        public async Task WhenAskedForOnePaycheck_LeBronJames_Eighth_ShouldReturnCorrectAmount()
        {
            var response = await HttpClient.GetAsync("/api/v1/payments/1/8");
            var payments = new GetPaymentsDto()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                Payments = new decimal[]
                    {
                        2439.26m
                    }
            };

            await response.ShouldReturn(HttpStatusCode.OK, payments);
        }

        [Fact]
        public async Task WhenAskedForAllPayments_JaMorant_ShouldReturnAllPayments()
        {
            var response = await HttpClient.GetAsync("/api/v1/payments/2");
            var payments = new GetPaymentsDto()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                Payments = new decimal[]
                    {
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.16m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.15m,
                        2189.16m
                    }
            };

            await response.ShouldReturn(HttpStatusCode.OK, payments);
        }

        [Fact]
        public async Task WhenAskedForOnePaycheck_JaMorant_Thirteenth_ShouldReturnCorrectAmount()
        {
            var response = await HttpClient.GetAsync("/api/v1/payments/2/13");
            var payments = new GetPaymentsDto()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                Payments = new decimal[]
                    {
                        2189.16m
                    }
            };

            await response.ShouldReturn(HttpStatusCode.OK, payments);
        }

        [Fact]
        public async Task WhenAskedForAllPayments_MichaelJordan_ShouldReturnAllPayments()
        {
            var response = await HttpClient.GetAsync("/api/v1/payments/3");
            var payments = new GetPaymentsDto()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                Payments = new decimal[]
                    {
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.18m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.18m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.18m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.19m,
                        4567.18m,
                        4567.19m,
                        4567.19m
                    }
            };

            await response.ShouldReturn(HttpStatusCode.OK, payments);
        }

        [Fact]
        public async Task WhenAskedForOnePaycheck_MichaelJordan_Last_ShouldReturnCorrectAmount()
        {
            var response = await HttpClient.GetAsync("/api/v1/payments/3/26");
            var payments = new GetPaymentsDto()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                Payments = new decimal[]
                    {
                        4567.19m
                    }
            };

            await response.ShouldReturn(HttpStatusCode.OK, payments);
        }

        [Fact]
        public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
        {
            var response = await HttpClient.GetAsync($"/api/v1/payments/{int.MinValue}");
            await response.ShouldReturn(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task WhenAskedForANonexistentPaycheck_ZeroValue_ShouldReturn422()
        {
            var response = await HttpClient.GetAsync($"/api/v1/payments/1/0");
            await response.ShouldReturn(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task WhenAskedForANonexistentPaycheck_ValueTooBig_ShouldReturn422()
        {
            var response = await HttpClient.GetAsync($"/api/v1/payments/1/27");
            await response.ShouldReturn(HttpStatusCode.UnprocessableEntity);
        }
    }
}
