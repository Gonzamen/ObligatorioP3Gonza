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
    public class RepositorioLaboratorio : IRepositorioLaboratorio
    {
        public bool Add(Laboratorio unLaboratorio)
        {
            if (unLaboratorio == null)
            {
                return false;
            }
            try
            {
                using (DBVacunacionContext db = new DBVacunacionContext())
                {
                    db.Laboratorios.Add(unLaboratorio);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    public IEnumerable<Laboratorio> FindAll()
    {
        using (DBVacunacionContext db = new DBVacunacionContext())
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db.Laboratorios.ToList();
        }
    }

    public Laboratorio FindById(int id)
    {
        using (DBVacunacionContext db = new DBVacunacionContext())
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db.Laboratorios.Find(id);
        }
    }

    public Laboratorio FindByNombre(string nombre)
    {
        using (DBVacunacionContext db = new DBVacunacionContext())
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db.Laboratorios.Where(l => l.Nombre == nombre).SingleOrDefault();

        }
    }
}
}
