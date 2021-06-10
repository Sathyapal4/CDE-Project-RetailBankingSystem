using EmployeeModule.Models;
using System.Collections.Generic;

namespace EmployeeModule.EmployeesRepository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployee();
        EmployeeResponse GetEmployee(Employee employee);
    }
}