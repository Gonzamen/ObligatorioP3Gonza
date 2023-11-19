using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Vacunas")]
    public class Vacuna : IValidable
    {
        #region Atributos
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(400)]
        public string Nombre { get; set; }
        public virtual ICollection<Laboratorio> Laboratorios { get; set; }      
        [ForeignKey("Tipo")]
        public string VacunaTipo { get; set; }
        public virtual TipoVacuna Tipo { get; set; }
        [Required]
        public int Dosis { get; set; }
        [Required]
        public int LapsoDosis { get; set; }
        [Required]
        public int RangoEdadMin { get; set; }
        [Required]
        public int RangoEdadMax { get; set; }
        [Required]
        public decimal EfiCovid { get; set; }
        [Required]
        public decimal EfiHosp { get; set; }
        [Required]
        public decimal EfiCti { get; set; }
        [Required]
        public int TempMin { get; set; }
        [Required]
        public int TempMax { get; set; }
        [Required]
        public int Fase { get; set; }
        public string EstatusPaises { get; set; }
        [Required]
        public bool AprobacionEmergencia { get; set; }
        [Required]
        public string EfectosAdv { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int DosisAnual { get; set; }
        [Required]
        public bool Covax { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        #endregion


        #region Constructores
        public Vacuna() { }
        public Vacuna(int id, string nombre, ICollection<Laboratorio> laboratorios, TipoVacuna tipo, int dosis, int lapsoDosis, int rangoEdadMin, int rangoEdadMax, decimal efiCovid, decimal efiHosp, decimal efiCti, int tempMin, int tempMax, int fase, string estatusPaises, bool aprobacionEmergencia, string efectosAdv, decimal precio, int dosisAnual, bool covax,string cedula, DateTime fechaRegistro)
        {
            Id = id;
            Nombre = nombre;
            Laboratorios = laboratorios;
            Tipo = tipo;
            VacunaTipo = Tipo.Codigo;
            Dosis = dosis;
            LapsoDosis = lapsoDosis;
            RangoEdadMin = rangoEdadMin;
            RangoEdadMax = rangoEdadMax;
            EfiCovid = efiCovid;
            EfiHosp = efiHosp;
            EfiCti = efiCti;
            TempMin = tempMin;
            TempMax = tempMax;
            Fase = fase;
            EstatusPaises = estatusPaises;
            AprobacionEmergencia = aprobacionEmergencia;
            EfectosAdv = efectosAdv;
            Precio = precio;
            DosisAnual = dosisAnual;
            Covax = covax;
            Cedula = cedula;
            FechaRegistro = fechaRegistro;
        }
        #endregion

        public bool Validar()
        {
            return
                   Laboratorios.Count() != 0 &&
                   this.VacunaTipo != "" &&
                   this.Dosis >= 1 &&
                   this.LapsoDosis >= 0 &&
                   this.RangoEdadMin >0 &&
                   this.RangoEdadMax >0 &&
                   this.EfiCovid >= 0 && this.EfiCovid <= 100 &&
                   this.EfiHosp >= 0 && this.EfiHosp <= 100 &&
                   this.EfiCti >= 0 && this.EfiCti <= 100 &&
                   this.TempMin >= -100 &&
                   this.TempMax <= 50 &&
                   this.TempMin <= this.TempMax &&
                   this.Fase >= 1 && this.Fase <= 4 &&
                   this.EfectosAdv != "" &&
                   this.Precio >= -1 &&
                   this.DosisAnual >= 0
            ;
        }

        public bool ValidarEdit()
        {
            return
                   this.Fase >= 1 && this.Fase <= 4 &&
                   this.Precio >= 0;
        }

        public override string ToString()
        {
            string lab = "";
            foreach (Laboratorio l in Laboratorios)
            {
                lab += l.Nombre + ", ";
            }
            return lab;
        }
    }
}
