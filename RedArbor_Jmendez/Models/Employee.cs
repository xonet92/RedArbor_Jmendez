using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedArbor_Jmendez.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeleteOn { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public DateTime? Lastlogin { get; set; }
        public string Password { get; set; }
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string Telephone { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string Username { get; set; }
    }
}