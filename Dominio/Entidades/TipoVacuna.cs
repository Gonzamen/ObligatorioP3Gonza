using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("TiposVacuna")]
    public class TipoVacuna : IValidable
    {
        [MaxLength(8)]
        [Key]
        public string Codigo { get; set; }
        [MaxLength(150)]
        public string Descripcion { get; set; }

        public TipoVacuna() { }
        public TipoVacuna(string codigo, string descripcion) {
            Codigo = codigo;
            Descripcion = descripcion;
        }
        public bool Validar()
        {
            return Codigo != "" && Descripcion != "";
        }
    }
}
