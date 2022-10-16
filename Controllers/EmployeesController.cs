using System;
using System.Web.Mvc;
using ThalesEmployessAPI.Controllers;
using ThalesEmployessAPI.Model;
using MVC_Thales_Client.Models;
using MVC_Thales_Client.BussinessLogic;
using System.Collections.Generic;

namespace MVC_Thales_Client.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployee rep;

        public EmployeesController()
        {
            rep = new EmployeesApiController();

        }

        
        public ActionResult Index()
        {
            EmployeeModel emp = new EmployeeModel();
            emp.message = "New";
            return View(emp);
        }

        [HttpPost]
        public ActionResult Index(EmployeeModel idEm )
        {
            EmployeeModel emp = new EmployeeModel();
            if (idEm.id == 0 || idEm.id == null)
            {
                emp = GetEmployees();
            }
            else
            {
                emp = getEmployeeById(idEm.id);
            }
            
            return View(emp);
        }

        
        
        [HttpGet]
        private EmployeeModel GetEmployees()
        {
            EmployeeModel emp = new EmployeeModel();
            
            try
            {
                var resp = rep.getAllEmployees();
                var bL = new BusinessLogic();
                List<EmployeeModel> lstEmp = new List<EmployeeModel>();
                if (resp.lstEmployees != null)
                {
                    foreach (var z in resp.lstEmployees)
                    {
                        EmployeeModel b = new EmployeeModel();

                        var Employee_anual_salary = bL.annualSalary(z.employee_salary);

                        b.id = z.id;
                        b.employee_name = z.employee_name;
                        b.employee_age = z.employee_age;
                        b.employee_salary = z.employee_salary;
                        b.employee_annual_salary = Employee_anual_salary;
                    
                        lstEmp.Add(b);
                    }

                    emp.lstEmployees = lstEmp;

                }
                
                else
                {
                    emp.message = "Error while processing the information.";
                    return emp ;
                }
                emp.message = "Ok";

                return emp;
            }
            catch (Exception ex)
            {
                emp.message = "Error while processing the information." + ex.Message.ToString();
                return emp;
                
            }

        }

        private EmployeeModel getEmployeeById(int idEmp)
        {
            EmployeeModel emp = new EmployeeModel();
            try
            {
                var resp = rep.getEmployeeById(idEmp);
                List<EmployeeModel> lstEmp = new List<EmployeeModel>();
                var bL = new BusinessLogic();
                if (resp != null)
                {
                    var Employee_anual_salary = bL.annualSalary(resp.employee_salary);
                    emp.id = resp.id;
                    emp.employee_name = resp.employee_name;
                    emp.employee_age = resp.employee_age;
                    emp.employee_salary = resp.employee_salary;
                    emp.employee_annual_salary = Employee_anual_salary;
                    lstEmp.Add(emp);
                    emp.lstEmployees = lstEmp;
                }
                else
                {
                    emp.message = "This employee not exist!!!";
                    return emp;
                }
                emp.message = "Ok";
                return emp;
            }
            catch (Exception ex)
            {
                emp.message = "Ups! technical issues for: " + ex.Message.ToString();
                return emp;
            }


        }

    }
}