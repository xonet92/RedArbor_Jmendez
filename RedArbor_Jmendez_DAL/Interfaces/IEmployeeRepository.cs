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
        ResponseDAO GetEmployee(int id);
        ResponseDAO AddEmployee(EmployeeDAO employee);
        ResponseDAO UpdateEmployee(EmployeeDAO employee);
        ResponseDAO DeleteEmployee(int id);
    }
}
