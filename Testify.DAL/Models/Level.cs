using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Status { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
