namespace Api.Controllers
{
    using Api.Calculator;
    using Api.DataAccessLayer;
    using Api.Dtos.Payments;
    using Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// By running the debugger in Visual Studio, you can view the employees and their dependents via
    /// the auto-generated client.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        [SwaggerOperation(Summary = "Get this year's payments for an employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetPaymentsDto>>> GetAll(
            [SwaggerParameter("The employee id")] int id)
        {
            IDataStorage? dataStorage = null;
            Employee? employeeModel = null;
            try
            {
                dataStorage = DataStorage.CreateDataStorage();
                employeeModel = dataStorage.GetEmployee(id);
            }
            catch (DataNotFoundException)
            {
                return new NotFoundResult();
            }

            var payments = this.GetAllPayments(employeeModel);

            var dtoValue = new GetPaymentsDto()
            {
                Id = employeeModel.Id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Salary = employeeModel.Salary,
                Payments = payments.ToArray()
            };

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
            if (paycheck < 1 || paycheck > 26)
            {
                return new UnprocessableEntityResult();
            }

            IDataStorage? dataStorage = null;
            Employee? employeeModel = null;
            try
            {
                dataStorage = DataStorage.CreateDataStorage();
                employeeModel = dataStorage.GetEmployee(id);
            }
            catch (DataNotFoundException)
            {
                return new NotFoundResult();
            }

            var payments = this.GetAllPayments(employeeModel);

            var dtoValue = new GetPaymentsDto()
            {
                Id = employeeModel.Id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Salary = employeeModel.Salary,
                Payments = new decimal[] { payments.ElementAt(paycheck - 1) }
            };

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
