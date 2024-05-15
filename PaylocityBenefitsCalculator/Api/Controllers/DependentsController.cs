using Api.DataAccessLayer;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dataStorage = DataStorage.CreateDataStorage();
        var dependentModel = dataStorage.GetDependent(id);
        var dtoValue = ModelToDto.Convert(dependentModel);

        var result = new ApiResponse<GetDependentDto>
        {
            Data = dtoValue,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dataStorage = DataStorage.CreateDataStorage();
        var dependentModels = dataStorage.GetAllDependents();
        var dtoValues = dependentModels.Select(d => ModelToDto.Convert(d));
        var dependents = new List<GetDependentDto>(dtoValues);

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };

        return result;
    }
}
