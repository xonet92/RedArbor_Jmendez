using RedArbor_Jmendez_DAL.Context;
using RedArbor_Jmendez_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RedArbor_Jmendez_DAL.Interfaces;
using System.Configuration;
using System.Web.Helpers;
using Dapper;
using System.Data.Entity;

namespace RedArbor_Jmendez_DAL.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EFDbContext _context = new EFDbContext();
        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Context"].ConnectionString);
        public ResponseDAO AddEmployee(EmployeeDAO employee)
        {
            try
            {
                if (employee == null)
                {
                    return new ResponseDAO((int)HttpStatusCode.BadRequest, "Parameter is null", null);
                }
                string checkEmp = CheckDataEmployee(employee);
                if (!string.IsNullOrEmpty(checkEmp))
                {
                    return new ResponseDAO((int)HttpStatusCode.BadRequest, checkEmp, null);
                }
                EmployeeDAO employeeDAO = GetRepEmployee(employee.Id, employee.Username);
                if (employeeDAO != null)
                {
                    return new ResponseDAO((int)HttpStatusCode.BadRequest, "Employee is exist", employeeDAO);
                }
                employee.Password = Crypto.HashPassword(employee.Password);
                if (employee.CreatedOn == null)
                {
                    employee.CreatedOn = DateTime.Now;
                }
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return new ResponseDAO((int)HttpStatusCode.OK, "Success", employee);
            }
            catch (Exception e)
            {
                return new ResponseDAO((int)HttpStatusCode.BadRequest, e.Message, null); ;
            }
        }

        public ResponseDAO UpdateEmployee(EmployeeDAO employee)
        {
            try
            {
                if (employee == null)
                {
                    return new ResponseDAO((int)HttpStatusCode.BadRequest, "Parameter is null", null);
                }
                EmployeeDAO employeeDAO = GetRepEmployee(employee.Id);
                if (employeeDAO == null)
                {
                    return new ResponseDAO((int)HttpStatusCode.BadRequest, string.Format("The employee with id {0} not exist in the system", employee.Id), null);
                }
                else
                {
                    string checkEmp = CheckDataEmployee(employee);
                    if (!string.IsNullOrEmpty(checkEmp))
                    {
                        return new ResponseDAO((int)HttpStatusCode.BadRequest, checkEmp, null);
                    }
                    employee.Password = Crypto.HashPassword(employee.Password);
                    employee.UpdateOn = DateTime.Now;
                    _context.Entry(employee).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                return new ResponseDAO((int)HttpStatusCode.OK, "Success", employee);
            }
            catch (Exception e)
            {
                return new ResponseDAO((int)HttpStatusCode.BadRequest, e.Message, null); ;
            }
        }

        public ResponseDAO DeleteEmployee(int id)
        {
            try
            {
                EmployeeDAO employeeDAO = GetRepEmployee(id);
                if (employeeDAO == null)
                {
                    return new ResponseDAO((int)HttpStatusCode.BadRequest, string.Format("The employee with id {0} not exist in the system", id), null);
                }
                else
                {
                    _context.Entry(employeeDAO).State = EntityState.Deleted;
                    _context.SaveChanges();
                }
                return new ResponseDAO((int)HttpStatusCode.OK, "Success", null);
            }
            catch (Exception e)
            {
                return new ResponseDAO((int)HttpStatusCode.BadRequest, e.ToString(), null);
            }
        }

        public IEnumerable<EmployeeDAO> GetAllEmployees()
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                IEnumerable<EmployeeDAO> listEmployee = db.Query<EmployeeDAO>("select * From Employees");
                db.Close();
                return listEmployee;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResponseDAO GetEmployee(int id)
        {
            try
            {
                EmployeeDAO employeeDAO = GetRepEmployee(id);
                if (employeeDAO == null)
                {
                    return new ResponseDAO((int)HttpStatusCode.OK, string.Format("The employee with id {0} not exist in the system", id), null);
                }
                else
                {
                    return new ResponseDAO((int)HttpStatusCode.OK, "Succes", employeeDAO);
                }
            }
            catch (Exception e)
            {
                return new ResponseDAO((int)HttpStatusCode.BadRequest, e.Message, null);
            }
        }

        private EmployeeDAO GetRepEmployee(int Id, string username = "")
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                EmployeeDAO employeeDAO = string.IsNullOrEmpty(username) ? db.QueryFirst<EmployeeDAO>("select * From Employees where Id = @Id", new { Id = Id })
                                          : db.QueryFirst<EmployeeDAO>("select * From Employees where Id = @Id or Username = @Username", new { Id = Id, Username = username });
                db.Close();
                return employeeDAO;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string CheckDataEmployee(EmployeeDAO employee)
        {
            if (employee.CompanyId == 0)
            {
                return "The value Company Id cannot be 0 and must be numeric";
            }
            if (string.IsNullOrEmpty(employee.Email))
            {
                return "The value email cannot be empty";
            }
            if (string.IsNullOrEmpty(employee.Password))
            {
                return "The value Password cannot be empty";
            }
            if(employee.PortalId == 0)
            {
                return "The value Portal Id cannot be 0 and must be numeric";
            }
            if(employee.RoleId == 0)
            {
                return "The value Role Id cannot be 0 and must be numeric";
            }
            if(employee.StatusId == 0)
            {
                return "The value Status Id cannot be 0 and must be numeric";
            }
            if (string.IsNullOrEmpty(employee.Username))
            {
                return "The value Username cannot be empty";
            }
            return string.Empty;
        }
    }
}
