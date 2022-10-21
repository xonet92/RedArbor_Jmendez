using RedArbor_Jmendez_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor_Jmendez_DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeDAO> GetAllEmployees();
        EmployeeDAO GetEmployee(int id);
        EmployeeDAO AddEmployee(EmployeeDAO employee);
        void UpdateEmployee(EmployeeDAO employee);
        void DeleteEmployee(EmployeeDAO employee);
    }
}
