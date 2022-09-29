using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor_Jmendez_DAL.Entities
{
    public class ResponseDAO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public EmployeeDAO EmployeeDAO { get; set; }
        public ResponseDAO() { }
        public ResponseDAO(int code, string message, EmployeeDAO employeeDAO)
        {
            Code = code;
            Message = message;
            EmployeeDAO = employeeDAO;
        }
    }
}
