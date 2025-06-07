using System;
using System.ComponentModel.DataAnnotations;

namespace Finances.APP.Models.Account
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "A senha deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare("NewPassword", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmNewPassword { get; set; }
    }
} 