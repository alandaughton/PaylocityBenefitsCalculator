namespace Api.DataAccessLayer
{
    using System.Collections.Generic;
    using Api.Models;

    /// <summary>
    /// This interface represents the data storage for the business logic.
    /// </summary>
    public interface IDataStorage
    {
        IEnumerable<Employee> GetAllEmployees();

        IEnumerable<Dependent> GetAllDependents();

        Employee GetEmployee(int employeeId);

        Dependent GetDependent(int dependentId);
    }

    /// <summary>
    /// This class will implement the data storage for the business logic.
    /// </summary>
    /// <remarks>
    /// Since there isn't a real database, this class will use hard-coded data.
    /// Each method will return copies of the hard-coded data, with the properties completely populated.
    /// </remarks>
    public class DataStorage : IDataStorage
    {
        private readonly Dictionary<int, Employee> _employees;
        private readonly Dictionary<int, Dependent> _dependents;

        private DataStorage()
        {
            this._employees = InitializeEmployees();
            this._dependents = InitializeDependents();
        }

        /// <summary>
        /// Factory method to return the interface that represents the data storage for the business logic.
        /// </summary>
        /// <returns></returns>
        public static IDataStorage CreateDataStorage()
        {
            return new DataStorage();
        }

        /// <summary>
        /// This method will give a complete copy of the hard-coded data with all properties
        /// correctly populated.  Since the number of hard-coded items is small, this method
        /// will be used by all other methods to ensure that the returned data is fully correct.
        /// </summary>
        public IEnumerable<Employee> GetAllEmployees()
        {
            var allEmployees = new List<Employee>();

            foreach (var employee in this._employees.Values)
            {
                var retEmp = this.CopyEmployee(employee);

                foreach (var dependent in this._dependents.Values)
                {
                    if (dependent.EmployeeId == employee.Id)
                    {
                        var retDep = this.CopyDependent(dependent);

                        retDep.Employee = retEmp;
                        retEmp.Dependents.Add(retDep);
                    }
                }

                allEmployees.Add(retEmp);
            }

            return allEmployees;
        }

        public IEnumerable<Dependent> GetAllDependents()
        {
            var allDependents = new List<Dependent>();

            var employees = new Dictionary<int, Employee>();
            foreach (var employee in this.GetAllEmployees())
            {
                employees[employee.Id] = employee;
            }

            foreach (var employee in employees.Values)
            {
                foreach (var dependent in employee.Dependents)
                {
                    allDependents.Add(dependent);
                }
            }

            return allDependents;
        }

        public Employee GetEmployee(int employeeId)
        {
            foreach (var employee in this.GetAllEmployees())
            {
                if (employee.Id == employeeId)
                {
                    return employee;
                }
            }

            throw new Exception($"Unrecognized employee id {employeeId}");
        }

        public Dependent GetDependent(int dependentId)
        {
            foreach (var employee in this.GetAllEmployees())
            {
                foreach (var dependent in employee.Dependents)
                {
                    if (dependent.Id == dependentId)
                    {
                        return dependent;
                    }
                }
            }

            throw new Exception($"Unrecognized dependent id {dependentId}");
        }

        private Employee CopyEmployee(Employee employee)
        {
            var copy = new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary
            };

            return copy;
        }

        private Dependent CopyDependent(Dependent dependent)
        {
            var copy = new Dependent
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                DateOfBirth = dependent.DateOfBirth,
                EmployeeId = dependent.EmployeeId,
                Relationship = dependent.Relationship
            };

            return copy;
        }

        private static Dictionary<int, Employee> InitializeEmployees()
        {
            var employees = new Dictionary<int, Employee>();

            var employee = new Employee();
            employee.Id = 1;
            employee.FirstName = "LeBron";
            employee.LastName = "James";
            employee.DateOfBirth = new DateTime(1984, 12, 30);
            employee.Salary = 75420.99m;
            employees[1] = employee;

            employee = new Employee();
            employee.Id = 2;
            employee.FirstName = "Ja";
            employee.LastName = "Morant";
            employee.DateOfBirth = new DateTime(1999, 8, 10);
            employee.Salary = 92365.22m;
            employees[2] = employee;

            employee = new Employee();
            employee.Id = 3;
            employee.FirstName = "Michael";
            employee.LastName = "Jordan";
            employee.DateOfBirth = new DateTime(1963, 2, 17);
            employee.Salary = 143211.12m;
            employees[3] = employee;

            return employees;
        }

        private static Dictionary<int, Dependent> InitializeDependents()
        {
            var dependents = new Dictionary<int, Dependent>();

            var dependent = new Dependent();
            dependent.Id = 1;
            dependent.FirstName = "Spouse";
            dependent.LastName = "Morant";
            dependent.DateOfBirth = new DateTime(1998, 3, 3);
            dependent.EmployeeId = 2;
            dependent.Relationship = Relationship.Spouse;
            dependents[1] = dependent;

            dependent = new Dependent();
            dependent.Id = 2;
            dependent.FirstName = "Child1";
            dependent.LastName = "Morant";
            dependent.DateOfBirth = new DateTime(2020, 6, 23);
            dependent.EmployeeId = 2;
            dependent.Relationship = Relationship.Child;
            dependents[2] = dependent;

            dependent = new Dependent();
            dependent.Id = 3;
            dependent.FirstName = "Child2";
            dependent.LastName = "Morant";
            dependent.DateOfBirth = new DateTime(2021, 5, 18);
            dependent.EmployeeId = 2;
            dependent.Relationship = Relationship.Child;
            dependents[3] = dependent;

            dependent = new Dependent();
            dependent.Id = 4;
            dependent.FirstName = "DP";
            dependent.LastName = "Jordan";
            dependent.DateOfBirth = new DateTime(1974, 1, 2);
            dependent.EmployeeId = 3;
            dependent.Relationship = Relationship.DomesticPartner;
            dependents[4] = dependent;

            return dependents;
        }
    }
}
