using System.ComponentModel.DataAnnotations;

namespace Finances.Database.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
} 