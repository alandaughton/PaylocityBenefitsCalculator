using Api.DataAccessLayer;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// By running the debugger in Visual Studio, you can view the employees and their dependents via
/// the auto-generated client.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var dataStorage = DataStorage.CreateDataStorage();
            var employeeModel = dataStorage.GetEmployee(id);
            var dtoValue = ModelToDto.Convert(employeeModel);

            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = dtoValue,
                Success = true
            };

            return result;
        }
        catch (DataNotFoundException)
        {
            return new NotFoundResult();
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var dataStorage = DataStorage.CreateDataStorage();
        var employeeModels = dataStorage.GetAllEmployees();
        var dtoValues = employeeModels.Select(e => ModelToDto.Convert(e));
        var employees = new List<GetEmployeeDto>(dtoValues);

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }
}
