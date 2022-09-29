using RedArbor_Jmendez.Models;
using RedArbor_Jmendez_DAL.Entities;
using RedArbor_Jmendez_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedArbor_Jmendez.Controllers
{
    public class LogApiController : ApiController
    {
        private ILogApiCallsRepository _repLogApiCalls;
        public LogApiController(ILogApiCallsRepository repLogApiCalls)
        {
            _repLogApiCalls = repLogApiCalls;
        }

        [HttpGet]
        [Authorize(Roles = "API")]
        [Route("LogApi/GetLogs")]
        public IEnumerable<LogApiCallsDAO> GetLogs([FromBody] LogApi log)
        {
            if(log == null)
            {
                return _repLogApiCalls.GetLogApiCalls();
            }
            else
            {
                return _repLogApiCalls.GetLogApiCalls(log.CreatedOnEnd,log.CreatedOnStart,log.Controller,log.Method,log.CreateUser,log.Parameters,log.Response);
            }           
        }
    }
}
