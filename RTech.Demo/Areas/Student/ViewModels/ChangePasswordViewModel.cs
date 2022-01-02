using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }

        [Required, StringLength(15, MinimumLength =7)]
        public string NewPassword { get; set; }

        [Required, Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}