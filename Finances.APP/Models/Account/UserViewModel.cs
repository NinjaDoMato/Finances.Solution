using System;

namespace Finances.APP.Models.Account
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime DateCreated { get; set; }
    }
} 