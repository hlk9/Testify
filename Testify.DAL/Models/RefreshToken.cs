﻿using System.ComponentModel.DataAnnotations;

namespace Testify.DAL.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [MaxLength(550)]
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRevoked { get; set; }
    }
}
