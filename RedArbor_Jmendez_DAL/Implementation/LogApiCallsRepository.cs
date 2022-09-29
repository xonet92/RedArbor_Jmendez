using RedArbor_Jmendez_DAL.Context;
using RedArbor_Jmendez_DAL.Entities;
using RedArbor_Jmendez_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;

namespace RedArbor_Jmendez_DAL.Implementation
{
    public class LogApiCallsRepository : ILogApiCallsRepository
    {
        EFDbContext _context = new EFDbContext();
        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Context"].ConnectionString);
        public void AddLog(LogApiCallsDAO log)
        {
            _context.LogApiCalls.Add(log);
            _context.SaveChanges();
        }

        public IEnumerable<LogApiCallsDAO> GetLogApiCalls(DateTime? CreatedOnStart, DateTime? CreatedOnEnd, string controller, string method, string user, string parameters, string response)
        {

            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
            IEnumerable<LogApiCallsDAO> listLogApi = db.Query<LogApiCallsDAO>("select * From LogApiCalls");
            db.Close();
            if(listLogApi!= null && listLogApi.Count() > 0)
            {
                if (!string.IsNullOrEmpty(controller))
                {
                    listLogApi = listLogApi.Where(x => x.Controller.ToUpper() == controller.ToUpper());
                }
                if (!string.IsNullOrEmpty(method))
                {
                    listLogApi = listLogApi.Where(x => x.Method.ToUpper() == method.ToUpper());
                }
                if (!string.IsNullOrEmpty(user))
                {
                    listLogApi = listLogApi.Where(x => x.CreateUser.ToUpper() == user.ToUpper());
                }
                if (!string.IsNullOrEmpty(parameters))
                {
                    listLogApi = listLogApi.Where(x => x.Parameters.Contains(parameters));
                }
                if (!string.IsNullOrEmpty(response))
                {
                    listLogApi = listLogApi.Where(x => x.Response.Contains(response));
                }
                if (CreatedOnStart != null && CreatedOnEnd == null)
                {
                    string start = CreatedOnStart.Value.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    listLogApi = listLogApi.Where(x => x.CreatedOn.Value.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) == start);
                }else if (CreatedOnStart != null && CreatedOnEnd != null && CreatedOnStart < CreatedOnEnd)
                {
                    listLogApi = listLogApi.Where(x => x.CreatedOn >= CreatedOnStart && x.CreatedOn <= CreatedOnEnd);
                }
            }
            return listLogApi;          
        }

        public IEnumerable<LogApiCallsDAO> GetLogApiCalls()
        {
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
            IEnumerable<LogApiCallsDAO> listLogApi = db.Query<LogApiCallsDAO>("select * From LogApiCalls");
            db.Close();
            return listLogApi;
        }
    }
}
