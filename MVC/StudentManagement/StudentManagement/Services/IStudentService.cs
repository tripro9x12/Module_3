using StudentManagement.Models.Entities;
using StudentManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Service
{
    public interface IStudentService
    {
        IEnumerable<Province> GetProvinces();
        IEnumerable<District> GetDistricts(int ProvinceId);
        IEnumerable<Ward> GetWards( int DistrictId, int ProvinceId);

        Student GetStudent(int id);
        int CreateStudent(Student student);
        IEnumerable<StudentView> GetStudents();
        StudentView GetS(int id);
        int EditStudent(Student model);
        int DeleteStudent(int id);

    }
}
