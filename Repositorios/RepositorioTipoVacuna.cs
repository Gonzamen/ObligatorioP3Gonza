using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Repositorios.EF;

namespace Repositorios
{
    public class RepositorioTipoVacuna : IRepositorioTipoVacuna
    {
        public bool Add(TipoVacuna unTipo)
        {
            if (unTipo == null)
            {
                return false;
            }
            try
            {
                using (DBVacunacionContext db = new DBVacunacionContext())
                {
                    db.TiposVacuna.Add(unTipo);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<TipoVacuna> FindAll()
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                return db.TiposVacuna.ToList();
            }
        }

        public TipoVacuna FindByCodigo(string codigo)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                return db.TiposVacuna.Find(codigo);
            }
        }
    }
}
