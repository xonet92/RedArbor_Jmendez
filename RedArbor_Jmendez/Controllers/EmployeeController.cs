using AutoMapper;
using Newtonsoft.Json;
using RedArbor_Jmendez.Models;
using RedArbor_Jmendez_DAL.Entities;
using RedArbor_Jmendez_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;

namespace RedArbor_Jmendez.Controllers
{
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository _repEmployee;
        private ILogApiCallsRepository _repLogApiCalls;
        private ResponseDAO _response;
        public EmployeeController(IEmployeeRepository repEmployee, ILogApiCallsRepository repLogApiCalls)
        {
            _repEmployee = repEmployee;
            _repLogApiCalls = repLogApiCalls;
            _response = new ResponseDAO();
        }

        [HttpGet]
        [Authorize(Roles = "API")]
        [Route("Employee/GetEmployee")]
        public ResponseDAO GetEmployee(int Id)
        {
            return _repEmployee.GetEmployee(Id);
        }

        [HttpGet]
        [Authorize(Roles = "API")]
        [Route("Employee/GetAllEmployees")]
        public IEnumerable<EmployeeDAO> GetAllEmployees()
        {
            return _repEmployee.GetAllEmployees();
        }

        [HttpPost]
        [Authorize(Roles = "API")]
        [Route("Employee/AddEmployee")]
        public ResponseDAO AddEmployee([FromBody] Employee employee)
        {           
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDAO>());
                var mapper = new Mapper(config);
                _response = _repEmployee.AddEmployee(mapper.Map<EmployeeDAO>(employee));
                AddLog("AddEmployee", JsonConvert.SerializeObject(employee), _response);
            }
            catch(Exception e)
            {
                _response.Code = (int)HttpStatusCode.BadRequest;
                _response.Message = e.Message;
                _response.EmployeeDAO = null;
            }          
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "API")]
        [Route("Employee/UpdateEmployee")]
        public ResponseDAO UpdateEmployee([FromBody] Employee employee)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDAO>());
                var mapper = new Mapper(config);
                _response = _repEmployee.UpdateEmployee(mapper.Map<EmployeeDAO>(employee));
                AddLog("UpdateEmployee", JsonConvert.SerializeObject(employee), _response);
            }
            catch(Exception e)
            {
                _response.Code = (int)HttpStatusCode.BadRequest;
                _response.Message = e.Message;
                _response.EmployeeDAO = null;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "API")]
        [Route("Employee/DeleteEmployee")]
        public ResponseDAO DeleteEmployee(int Id)
        {
            try
            {
                _response = _repEmployee.DeleteEmployee(Id);
                AddLog("DeleteEmployee", string.Format("Id = {0}", Id), _response);
            }
            catch(Exception e)
            {
                _response.Code = (int)HttpStatusCode.BadRequest;
                _response.Message = e.Message;
                _response.EmployeeDAO = null;
            }
            return _response;
        }

        private void AddLog(string method, string parameters, ResponseDAO response)
        {          
            LogApiCallsDAO log = new LogApiCallsDAO()
            {
                Controller = "Employee",
                Method = method, 
                Parameters = parameters,
                Response = JsonConvert.SerializeObject(response),
                CreatedOn = DateTime.Now,
                CreateUser = ConfigurationManager.AppSettings["APIUSER"]
            };
            _repLogApiCalls.AddLog(log);
        }
    }
}
