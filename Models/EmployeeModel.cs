using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Thales_Client.Models
{
    public class EmployeeModel
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_annual_salary { get; set; }
        public int employee_age { get; set; }
        public string message { get; set; }
        public List<EmployeeModel> lstEmployees { get; set; }
    }
}