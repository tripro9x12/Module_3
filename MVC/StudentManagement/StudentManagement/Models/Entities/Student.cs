using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Fullname { get; set; }
        public Gender? Gender { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [ForeignKey("Provinces")]
        public int ProvinceId { get; set; }
        [ForeignKey("Districts")]
        public int DistrictId { get; set; }
        [ForeignKey("Wards")]
        public int WardId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        public string Avatar { get; set; }
    }
}
