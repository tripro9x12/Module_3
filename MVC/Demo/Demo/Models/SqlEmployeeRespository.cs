using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class SqlEmployeeRespository : IEmployeeRespository
    {
        private readonly AppDBContext context;

        public SqlEmployeeRespository(AppDBContext context)
        {
            this.context = context;
        }

        public Employee Create(Employee model)
        {
            context.Employees.Add(model);
            context.SaveChanges();
            return model;
        }

        public bool Delete(int id)
        {
            var delEmp = context.Employees.Find(id);
            if(delEmp != null)
            {
                context.Employees.Remove(delEmp);
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public Employee Edit(Employee employee)
        {
            var editEmp = context.Employees.Attach(employee);
            editEmp.State = EntityState.Modified;
            context.SaveChanges();
            return employee;
        }

        public Employee Get(int id)
        {
            return context.Employees.Find(id);
        }

        public IEnumerable<Employee> Gets()
        {
            return context.Employees;
        }
    }
}
