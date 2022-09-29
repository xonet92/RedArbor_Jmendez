using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedArbor_Jmendez.Models
{
    public class LogApi
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Parameters { get; set; }
        public string Response { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreatedOnStart { get; set; }
        public DateTime? CreatedOnEnd { get; set; }
    }
}