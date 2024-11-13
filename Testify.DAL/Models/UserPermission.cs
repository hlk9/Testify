using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testify.DAL.Models
{
    public class UserPermission
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [ForeignKey("PermissionId")]
        public int PermissionId { get; set; }

        public virtual Permission? Permission { get; set; }
        public virtual User? User { get; set; }
    }
}
