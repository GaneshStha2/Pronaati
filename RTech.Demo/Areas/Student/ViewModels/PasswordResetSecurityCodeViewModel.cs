using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class PasswordResetSecurityCodeViewModel
    {
        public int Id { get; set; }
        public string SecurityCode { get; set; }
    }
}