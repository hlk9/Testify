using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class OrganizationUser
    {
         public int Id { get; set; }
        [ForeignKey("OrganizationId")]
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } 
        public bool? Status { get; set; } = true;

    }
}
