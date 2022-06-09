using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nest_BackFront.ViewModels.Authorization
{
    public class RegisterVM
    {
        [Required, MaxLength(50)]
        public string Firstname { get; set; }
        [Required, MaxLength(50)]
        public string Lastname { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}