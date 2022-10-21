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
        public EmployeeDAO AddEmployee(EmployeeDAO employee)
        {
            try
            {
                employee.Password = Crypto.HashPassword(employee.Password);
                if (employee.CreatedOn == null)
                {
                    employee.CreatedOn = DateTime.Now;
                }
                _context.Entry(employee).State = EntityState.Added;
                _context.SaveChanges();
                return employee;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateEmployee(EmployeeDAO employee)
        {                     
            employee.Password = Crypto.HashPassword(employee.Password);
            employee.UpdateOn = DateTime.Now;
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEmployee(EmployeeDAO employee)
        {                
            _context.Entry(employee).State = EntityState.Deleted;
            _context.SaveChanges();            
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
     
        public EmployeeDAO GetEmployee(int Id)
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                EmployeeDAO employeeDAO =  db.QueryFirst<EmployeeDAO>("select * From Employees where Id = @Id", new { Id = Id });
                db.Close();
                return employeeDAO;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
