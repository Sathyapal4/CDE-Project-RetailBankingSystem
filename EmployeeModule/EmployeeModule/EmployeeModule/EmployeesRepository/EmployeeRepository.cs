using EmployeeModule.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeModule.EmployeesRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext newContext;

        public EmployeeRepository(EmployeeDbContext context)
        {
            newContext = context;
        }
        public List<Employee> GetAllEmployee()
        {
            try
            {
                List<Employee> employees = newContext.Employees.ToList();
                return employees;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public EmployeeResponse GetEmployee(Employee employee)
        {
            try
            {
                Employee employee1 = newContext.Employees.Where(c => c.Email == employee.Email && c.Password == employee.Password).SingleOrDefault();
                if (employee1 == null)
                    return null;
                return new EmployeeResponse { Id = employee1.Id };
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
