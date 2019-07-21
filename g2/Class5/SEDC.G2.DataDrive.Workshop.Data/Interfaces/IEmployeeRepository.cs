using SEDC.G2.DataDrive.Workshop.Data.Model;
using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        bool AddEmployee(Employee employee);
        bool EditEmployee(Employee employee);
        bool DeleteEmployee(int id);
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
    }
}
