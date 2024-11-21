using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(6)]
        [Required]
        public string OrganizationCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? ContactPhone { get; set; }
        public string? Email { get; set; }
    }
}
