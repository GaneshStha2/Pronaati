using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.User.Entity
{
    public class EUser
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public UserType UserType { get; set; }
        [Required, MaxLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public int? BranchId { get; set; }
        public string FullName { get; set; }
        public string PhotoURL { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsDeleted { get; set; }
        public virtual EUserRole Role { get; set; }
        public virtual EBranch Branch { get; set; }
    }

    public enum UserType
    {
        Owner,
        User
    }
}
