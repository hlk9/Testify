using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class ClassUser
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public Guid UserId { get; set; }
        public bool? Status { get; set; } = true;

    }
}
