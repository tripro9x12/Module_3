using Microsoft.AspNetCore.Http;
using StudentManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models.ViewModels
{
    public class CreateStudentView : CreateStudentAdd
    {
        public CreateStudentView()
        {
            Provinces = new List<Province>();
            Districts = new List<District>();
            Wards = new List<Ward>();
        }

        public IEnumerable<Province> Provinces { get; set; }
        public IEnumerable<District> Districts { get; set; }
        public IEnumerable<Ward> Wards { get; set; }
    }
}
