using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("PrestadoresSalud")]
    public class PrestadorSalud : IValidable
    {
        #region Atributos
        [Key]
        [Display(Name = "Número de Registro")]
        public string NumRegistro { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(400)]
        public string Nombre { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        [Display(Name = "Nombre de Contacto")]
        public string NombreContacto { get; set; }
        [Required]
        [Display(Name = "Tope de Compras")]
        public int TopeCompras { get; set; }
        [Required]
        [Display(Name = "Cantidad de Afiliados")]
        public int CantAfiliados { get; set; }
        [Required]
        [Display(Name = "Monto Máximo de Vacunas")]
        public int MontoMaxVacunas { get; set; }
        #endregion
        #region Constructores
        public PrestadorSalud()
        {

        }
        public PrestadorSalud(string numregistro, string nombre, int telefono, string nombrecontacto, int topecompras, int cantafiliados, int montomaxvacunas) {
            NumRegistro = numregistro;
            Nombre = nombre;
            Telefono = telefono;
            NombreContacto = nombrecontacto;
            TopeCompras = topecompras;
            CantAfiliados = cantafiliados;
            MontoMaxVacunas = montomaxvacunas;
        }
        #endregion

        public bool Validar()
        {
            return this.NumRegistro.Length <= 6 &&
                   !this.NumRegistro.StartsWith("0")&&
                   this.CantAfiliados>=0 &&
                   this.TopeCompras>=0 &&
                   this.MontoMaxVacunas >=0;
        }
    }
}
