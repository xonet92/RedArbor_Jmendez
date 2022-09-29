using RedArbor_Jmendez_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor_Jmendez_DAL.Context
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("name=Context") { }
        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(null);
        }
        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        public virtual DbSet<EmployeeDAO> Employees { get; set; }
        public virtual DbSet<LogApiCallsDAO> LogApiCalls { get; set; }
    }
}
