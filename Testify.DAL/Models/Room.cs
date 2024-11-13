using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
        //[ForeignKey("OrganizationId")]
        //public int OrganizationId { get; set; }
        //public virtual Organization? Organization { get; set; }
    }
}
