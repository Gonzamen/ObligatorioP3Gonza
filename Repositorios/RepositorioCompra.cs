using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using Repositorios.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioCompra : IRepositorioCompra
    {
        public bool Add(Compra unaCompra)
        {

            if (unaCompra == null)
            {
                return false;
            }
            try
            {
                using (DBVacunacionContext db = new DBVacunacionContext())
                {
                    db.Compras.Add(unaCompra);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Compra FindById(int id)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                return db.Compras.Find(id);
            }
        }

        public IEnumerable<Compra> FindByPrestador(string numprestador)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {

                return db.Compras.Where(c => c.SaludPrestador == numprestador).ToList();
            }
        }

        public IEnumerable<Compra> FindByPrestadorMes(string numprestador)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {

                return db.Compras.Include("Vacuna").Where(c => c.SaludPrestador == numprestador && c.Fecha.Month == DateTime.Today.Month).ToList();
            }
        }
    }
}
