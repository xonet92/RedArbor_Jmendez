using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor_Jmendez_DAL.Entities
{
    [Table("LogApiCalls")]
    public class LogApiCallsDAO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Parameters { get; set; }
        public string Response { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
