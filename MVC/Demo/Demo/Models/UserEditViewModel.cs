﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        [Display(Name = "Role")]
        public string RoleId { get; set; }
    }
}
