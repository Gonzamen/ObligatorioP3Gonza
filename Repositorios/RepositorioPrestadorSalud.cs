using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Repositorios.EF;
using System.Data.Entity;

namespace Repositorios
{
    public class RepositorioPrestadorSalud : IRepositorioPrestadorSalud
    {
        public bool Add(PrestadorSalud unPrestador)
        {
            if(unPrestador == null)
            {
                return false;
            }
            try
            {
                using (DBVacunacionContext db = new DBVacunacionContext())
                {
                    db.PrestadoresSalud.Add(unPrestador);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }    
        }

        public IEnumerable<PrestadorSalud> FindAll()
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                return db.PrestadoresSalud.ToList();
            }
        }

        public PrestadorSalud FindByNum(string num)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                return db.PrestadoresSalud.Where(p => p.NumRegistro == num).SingleOrDefault();
            }
        }
    }
}
