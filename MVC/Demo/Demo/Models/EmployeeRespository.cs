using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class EmployeeRespository : IEmployeeRespository
    {   private List<Employee> employees;

        public EmployeeRespository()
        {
            employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Fullname = "An Nguyễn",
                    Department = Dept.IT,
                    Email = "anpro@gmail.com",
                    AvatarPath = "~/images/2.png"
                },
                new Employee()
                {
                    Id = 2,
                    Fullname = "Long Nguyễn",
                    Department = Dept.HR,
                    Email = "long9x@gmail.com",
                    AvatarPath = "~/images/3.png"
                },
            };
        }

        public Employee Create(Employee model)
        {
            model.Id = employees.Max(e => e.Id) + 1;
            employees.Add(model);
            return model;
        }

        public bool Delete(int id)
        {
            var deleteEmp = Get(id);
            if(deleteEmp != null)
            {
                employees.Remove(deleteEmp);
                return true;
            }
            return false;
        }

        public Employee Edit(Employee employee)
        {
            var editEmp = Get(employee.Id);
            editEmp.Fullname = employee.Fullname;
            editEmp.Email = employee.Email;
            editEmp.Department = employee.Department;
            return editEmp;
        }

        public Employee Get(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> Gets()
        {
            return employees;
        }
    }
}
