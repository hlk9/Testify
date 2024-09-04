using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? LastLogin {  get; set; }
        public byte Status { get; set; } = 1;
        [ForeignKey("LevelId")]
        public string LevelId { get; set; } 
        public virtual Level? Level { get; set; }
        public virtual ICollection<ClassUser>? ClassUsers { get; set; }
        public virtual ICollection<UserPermission>? UserPermissions { get; set; }
        public virtual ICollection<Submission>? Submissions { get; set; }
  
    }
}
