using Microsoft.AspNetCore.Http;
using StudentManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models.ViewModels
{
    public class UpdateStudentView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        [MaxLength(30)]
        [Display(Name = "Họ và tên")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"(09|01||03[2|3|6|8|9])+([0-9]{8})\b", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tỉnh/Thành phố")]
        public int ProvinceId { get; set; }
        [Display(Name = "Quận/Huyện")]
        public int DistrictId { get; set; }
        [Display(Name = "Phường/Xã")]
        public int WardId { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public Gender? Gender { get; set; }
        public string AvatarPath { get; set; }
        public IFormFile Avatar { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        [Display(Name = "Xác Nhận Mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
