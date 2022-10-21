using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedArbor_Jmendez.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "CompanyId is required")]
        public int CompanyId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeleteOn { get; set; }
        [Required(ErrorMessage = "Email is required"), StringLength(255)]
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public DateTime? Lastlogin { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "PortalId is required")]
        public int PortalId { get; set; }
        [Required(ErrorMessage = "RoleId is required")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "StatusId is required")]
        public int StatusId { get; set; }
        public string Telephone { get; set; }
        public DateTime? UpdateOn { get; set; }
        [Required(ErrorMessage = "Username is required"), StringLength(255)]
        public string Username { get; set; }
    }
}