using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class UsuarioModel
    {
        public string Cedula { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme Contraseña")]
        public string ConfirmPassword { get; set; }

        public UsuarioModel() { }
        public UsuarioModel(string cedula, string oldpassword, string newpassword, string confirmpassword)
        {
            Cedula = cedula;
            OldPassword = oldpassword;
            NewPassword = newpassword;
            ConfirmPassword = confirmpassword;
        }
    }
}