using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.EF
{
    public class DBVacunacionContext : DbContext
    {
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<PrestadorSalud> PrestadoresSalud { get; set; }
        public DbSet<TipoVacuna> TiposVacuna { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }
        public DbSet<Compra> Compras { get; set; }

        public DBVacunacionContext() :
            base("miConexion")
        { }
    }
}
