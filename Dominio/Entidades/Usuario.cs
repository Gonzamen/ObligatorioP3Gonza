using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Usuarios")]
    public class Usuario: IValidable
    {
        #region Atributos
        [Key]
        public string Cedula { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required]
        public string Password { get; set; }
        public bool PrimeraVez { get; set; }
        #endregion
        #region Constructores
        public Usuario() { }
        public Usuario(string cedula, string password)
        {
            Cedula = cedula;
            Password = password;
        }
        #endregion

        public bool Validar() {
            return this.Cedula.Length >= 6 &&
                   this.Cedula.Length <= 7 &&
                   this.Cedula.All(c => char.IsDigit(c)) &&
                   this.Password.Length >= 6 &&
                   this.Password.Any(c => char.IsUpper(c)) &&
                   this.Password.Any(c => char.IsLower(c)) &&
                   this.Password.Any(c => char.IsDigit(c));
        }

        public bool ValidarSoloCedula()
        {
            return this.Cedula.Length >= 6 &&
                   this.Cedula.Length <= 7 &&
                   this.Cedula.All(c => char.IsDigit(c));
        }
    }

}
