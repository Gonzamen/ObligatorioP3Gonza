using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Compras")]
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cantidad de Dosis")]
        public int CantDosis { get; set; }
        public virtual PrestadorSalud Prestador { get; set; }
        [ForeignKey("Prestador")]
        [Display(Name = "Institución")]
        public string SaludPrestador { get; set; }
        public virtual Vacuna Vacuna { get; set; }
        [ForeignKey("Vacuna")]
        [Display(Name = "Vacuna")]
        public int LaVacuna { get; set; }
        public DateTime Fecha { get; set; }
        public Compra()
        {
        }

        public Compra(int id, int cantDosis, PrestadorSalud prestador, Vacuna vacuna, DateTime fecha)
        {
            Id = id;
            CantDosis = cantDosis;
            Prestador = prestador;
            SaludPrestador = prestador.NumRegistro;
            Vacuna = vacuna;
            LaVacuna = vacuna.Id;
            Fecha = fecha;
        }
    }
}
