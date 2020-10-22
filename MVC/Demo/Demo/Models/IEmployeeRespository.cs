using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public interface IEmployeeRespository
    {
        IEnumerable<Employee> Gets();
        Employee Get(int id);
        Employee Create(Employee model);
        Employee Edit(Employee employee);
        bool Delete(int id);
    }
}
