using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Models.Entities;
using StudentManagement.Models.ViewModels;
using StudentManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext context;

        public StudentService(AppDbContext context)
        {
            this.context = context;
        }
        public int CreateStudent(Student student)
        {
            context.Students.Add(student);
            return context.SaveChanges();
        }

        public IEnumerable<District> GetDistricts(int ProvinceId)
        {
            return context.Districts.Where(d => d._province_id == ProvinceId);
        }

        public IEnumerable<Province> GetProvinces()
        {
            return context.Provinces;
        }

        public Student GetStudent(int id)
        {
            return context.Students.FirstOrDefault(s => s.Id == id);
        }
        public StudentView GetS(int id)
        {
            StudentView studentView = new StudentView();
            IEnumerable<StudentView> Students = new List<StudentView>();
            Students = GetStudents();
            studentView = Students.FirstOrDefault(s => s.Id == id);
            return studentView;
        }

        public IEnumerable<StudentView> GetStudents()
        {
            IEnumerable<StudentView> Students = new List<StudentView>();
            Students = (from s in context.Students
                        join p in context.Provinces on s.ProvinceId equals p.id
                        join d in context.Districts on s.DistrictId equals d.id
                        join w in context.Wards on s.WardId equals w.id
                        select (new StudentView()
                        {
                            Id = s.Id,
                            FullName = s.Fullname,
                            Email = s.Email,
                            Gender = (Gender)s.Gender,
                            PhoneNumber = s.PhoneNumber,
                            ProvinceName = p._name,
                            DistrictName = $"{d._prefix} {d._name}",
                            WardName = $"{w._prefix} {w._name}",
                            Address = s.Address,
                            AvatarPath = s.Avatar
                        })).ToList();
            return Students;
        }
 
        public IEnumerable<Ward> GetWards(int DistrictId = 0, int ProvinceId = 0)
        {
            if(DistrictId != 0 && ProvinceId != 0)
            {
                return context.Wards.Where(w => w._district_id == DistrictId && w._province_id == ProvinceId);
            }else if(DistrictId != 0)
            {
                return context.Wards.Where(w => w._district_id == DistrictId);
            }else if(ProvinceId != 0)
            {
                return context.Wards.Where(w => w._province_id == ProvinceId);
            }
            return context.Wards;
        }

        public int EditStudent(Student model)
        {
            var editStudent = context.Students.Attach(model);
            editStudent.State = EntityState.Modified;
            return context.SaveChanges();

        }

        public int DeleteStudent(int id)
        {
            var del = GetStudent(id);
            context.Students.Remove(del);
            return context.SaveChanges();
        }
    }
}
