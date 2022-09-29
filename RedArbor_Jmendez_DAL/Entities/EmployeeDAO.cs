using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor_Jmendez_DAL.Entities
{
    [Table("Employees")]
    public class EmployeeDAO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeleteOn { get; set; }
        [Required, StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Fax { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public DateTime? Lastlogin { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int PortalId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [StringLength(255)]
        public string Telephone { get; set; }
        public DateTime? UpdateOn { get; set; }
        [Required, StringLength(255)]
        public string Username { get; set; }
    }
}
