using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Student.Entity
{
    public class EStudentLogin
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string VerificationCode { get; set; }
        public int LogInFailedCount { get; set; }
        public string TemporaryResetCode { get; set; }
    }
}
