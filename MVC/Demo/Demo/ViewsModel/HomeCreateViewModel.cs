using Demo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ViewsModel
{
    public class HomeCreateViewModel
    {
        [Required(ErrorMessage = "Phải nhập họ tên")]
        [MaxLength(30, ErrorMessage = "Không thể vượt quá 30 kí tự")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Phải nhập email")]
        [Display(Name = "Office Email")] //đổi tên Email => Office Email
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Lỗi cú pháp")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn phải chọn phòng ban")]
        public Dept? Department { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
