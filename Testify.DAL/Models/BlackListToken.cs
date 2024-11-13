using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class BlackListToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BlacklistAt { get; set; } = DateTime.Now;
    }
}
