namespace Api.Controllers
{
    using Api.Dtos.Dependent;
    using Api.Dtos.Employee;
    using Api.Models;

    /// <summary>
    /// Utility for conversion from the model classes to the classes that are returned by the api controllers.
    /// </summary>
    public static class ModelToDto
    {
        public static GetDependentDto Convert(Dependent dependent)
        {
            return ConvertSimpleProps(dependent);
        }

        public static GetEmployeeDto Convert(Employee employee)
        {
            var dto = ConvertSimpleProps(employee);

            foreach (var depDto in employee.Dependents.Select(d => Convert(d)))
            {
                dto.Dependents.Add(depDto);
            }

            return dto;
        }

        private static GetDependentDto ConvertSimpleProps(Dependent dependent)
        {
            return new GetDependentDto
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                DateOfBirth = dependent.DateOfBirth,
                Relationship = dependent.Relationship
            };
        }

        private static GetEmployeeDto ConvertSimpleProps(Employee employee)
        {
            return new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary
            };
        }
    }
}
