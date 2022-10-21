using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedArbor_Jmendez.Models;
using RedArbor_Jmendez_DAL.Entities;
using RedArbor_Jmendez_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace RedArbor_Jmendez.Controllers
{
    [Authorize(Roles = "API")]
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository _repEmployee;
        private ILogApiCallsRepository _repLogApiCalls;
        public EmployeeController(IEmployeeRepository repEmployee, ILogApiCallsRepository repLogApiCalls)
        {
            _repEmployee = repEmployee;
            _repLogApiCalls = repLogApiCalls;
        }

        [HttpGet]
        [Route("Employee/GetEmployee")]
        public IHttpActionResult GetEmployee(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest();
                }
                EmployeeDAO emp = _repEmployee.GetEmployee(id);
                if(emp == null)
                {
                    return NotFound();
                }
                return Ok(emp);
            }
            catch (Exception e)
            {
                AddLog("GetEmployee", string.Format("id = {0}", id), e.ToString());
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("Employee/GetAllEmployees")]
        public IHttpActionResult GetAllEmployees()
        {
            try
            {
                return Ok(_repEmployee.GetAllEmployees());
            }
            catch(Exception e)
            {
                AddLog("GetAllEmployees", string.Empty, e.ToString());
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("Employee/AddEmployee")]
        public IHttpActionResult AddEmployee([FromBody] Employee employee)
        {           
            try
            {
                if(employee == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDAO>());
                var mapper = new Mapper(config);
                EmployeeDAO emp = _repEmployee.AddEmployee(mapper.Map<EmployeeDAO>(employee));
                if(emp!= null && emp.Id > 0)
                {
                    AddLog("AddEmployee", JsonConvert.SerializeObject(employee), string.Format("Status: {0}, Response: {1}",HttpStatusCode.OK, JsonConvert.SerializeObject(emp)));
                    return Ok(emp);
                }
                return BadRequest(); 
            }
            catch(Exception e)
            {
                AddLog("AddEmployee", JsonConvert.SerializeObject(employee), e.ToString());
                return InternalServerError(e);
            }          
        }

        [HttpPut]
        [Route("Employee/UpdateEmployee")]
        public IHttpActionResult UpdateEmployee(int id,[FromBody] Employee employee)
        {
            try
            {
                if(employee == null || id != employee.Id)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                EmployeeDAO emp = _repEmployee.GetEmployee(id);
                if(emp == null)
                {
                    return NotFound();
                }
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDAO>());
                var mapper = new Mapper(config);
                _repEmployee.UpdateEmployee(mapper.Map<EmployeeDAO>(employee));
                AddLog("UpdateEmployee", string.Format("id = {0}, Employee = {1}",id,JsonConvert.SerializeObject(employee)), HttpStatusCode.OK.ToString());
                return Ok();
            }
            catch(Exception e)
            {
                AddLog("UpdateEmployee", string.Format("id = {0}, Employee = {1}", id, JsonConvert.SerializeObject(employee)), e.ToString());
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("Employee/DeleteEmployee")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest();
                }
                else
                {
                    EmployeeDAO employee = _repEmployee.GetEmployee(id);
                    if(employee == null)
                    {
                        return NotFound();
                    }
                    _repEmployee.DeleteEmployee(employee);
                    AddLog("DeleteEmployee", string.Format("id = {0}", id), HttpStatusCode.OK.ToString());
                    return Ok();
                }
            }
            catch(Exception e)
            {
                AddLog("DeleteEmployee", string.Format("id = {0}", id), e.ToString());
                return InternalServerError(e);
            }
        }

        private void AddLog(string method, string parameters, string response)
        {          
            LogApiCallsDAO log = new LogApiCallsDAO()
            {
                Controller = "Employee",
                Method = method, 
                Parameters = parameters,
                Response = response,
                CreatedOn = DateTime.Now,
                CreateUser = ConfigurationManager.AppSettings["APIUSER"]
            };
            _repLogApiCalls.AddLog(log);
        }
    }
}
