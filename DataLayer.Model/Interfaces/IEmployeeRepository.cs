using System.Collections.Generic;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetByCode(string employeeCode);
        bool SaveEmployee(Employee employee);
    }
}