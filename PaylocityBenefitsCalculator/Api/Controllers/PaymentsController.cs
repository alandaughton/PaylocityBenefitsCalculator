namespace Api.Controllers
{
    using Api.Calculator;
    using Api.DataAccessLayer;
    using Api.Dtos.Payments;
    using Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    public class PaymentsController : ControllerBase
    {
        [SwaggerOperation(Summary = "Get this year's payments for an employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetPaymentsDto>>> GetAll(
            [SwaggerParameter("The employee id")] int id)
        {
            var dataStorage = DataStorage.CreateDataStorage();
            var employeeModel = dataStorage.GetEmployee(id);

            var payments = this.GetAllPayments(employeeModel);

            var dtoValue = new GetPaymentsDto()
            {
                Id = employeeModel.Id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Salary = employeeModel.Salary
            };

            foreach (var payment in payments)
            {
                dtoValue.Payments.Add(payment);
            }

            var result = new ApiResponse<GetPaymentsDto>
            {
                Data = dtoValue,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Get a single paycheck for an employee")]
        [HttpGet("{id}/{paycheck}")]
        public async Task<ActionResult<ApiResponse<GetPaymentsDto>>> GetForMonth(
            [SwaggerParameter("The employee id")] int id,
            [SwaggerParameter("The paycheck number (1 - 26)")] int paycheck)
        {
            var dataStorage = DataStorage.CreateDataStorage();
            var employeeModel = dataStorage.GetEmployee(id);

            var payments = this.GetAllPayments(employeeModel);

            var dtoValue = new GetPaymentsDto()
            {
                Id = employeeModel.Id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Salary = employeeModel.Salary
            };

            dtoValue.Payments.Add(payments.ElementAt(paycheck - 1));

            var result = new ApiResponse<GetPaymentsDto>
            {
                Data = dtoValue,
                Success = true
            };

            return result;
        }

        private IEnumerable<decimal> GetAllPayments(Employee employee)
        {
            var configuration = BenefitConfiguration.CreateConfiguration();
            var activeBenefits = ActiveBenefitSet.RetrieveActiveBenefits(configuration);
            var paymentsCalculator = new PaymentsCalculator(activeBenefits);
            return paymentsCalculator.CalculatePayments(employee, DateTime.Today.Year);
        }
    }
}
