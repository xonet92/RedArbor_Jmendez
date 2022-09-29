using RedArbor_Jmendez_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor_Jmendez_DAL.Interfaces
{
    public interface ILogApiCallsRepository
    {
        IEnumerable<LogApiCallsDAO> GetLogApiCalls();
        IEnumerable<LogApiCallsDAO> GetLogApiCalls(DateTime? CreatedOnStart, DateTime? CreatedOnEnd, string controller, string method, string user, string parameters, string response);
        void AddLog(LogApiCallsDAO log);
    }
}
