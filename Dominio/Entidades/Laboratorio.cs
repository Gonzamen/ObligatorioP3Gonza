using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Laboratorios")]
    public class Laboratorio
    {
        #region Atributos
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique=true)]
        [MaxLength(30)]
        public string Nombre { get; set; }
        [Required]
        public bool Experiencia { get; set; }    
        public string Pais { get; set; }
        public virtual ICollection<Vacuna> Vacunas { get; set; }
        #endregion
        #region Constructores
        public Laboratorio() { }
        public Laboratorio(int id, string nombre, string pais, bool experiencia)
        {
            Id = id;
            Nombre = nombre;
            Pais = pais;
            Experiencia = experiencia;
        }
        #endregion


    }
}
