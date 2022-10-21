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
    [Authorize(Roles = "API")]
    public class LogApiController : ApiController
    {
        private ILogApiCallsRepository _repLogApiCalls;
        public LogApiController(ILogApiCallsRepository repLogApiCalls)
        {
            _repLogApiCalls = repLogApiCalls;
        }

        [HttpGet]
        [Route("LogApi/GetLogs")]
        public IHttpActionResult GetLogs([FromBody] LogApi log)
        {
            try
            {
                if (log == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_repLogApiCalls.GetLogApiCalls(log.CreatedOnEnd, log.CreatedOnStart, log.Controller, log.Method, log.CreateUser, log.Parameters, log.Response));
                }
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }                  
        }
    }
}
