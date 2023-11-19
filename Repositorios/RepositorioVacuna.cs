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
    public class RepositorioVacuna : IRepositorioVacuna
    {
        public bool Add(Vacuna unaVacuna)
        {
            if (unaVacuna == null)
            {
                return false;
            }
            try
            {
                using (DBVacunacionContext db = new DBVacunacionContext())
                {
                    foreach (Laboratorio lab in unaVacuna.Laboratorios)
                    {
                        db.Entry(lab).State = System.Data.Entity.EntityState.Unchanged;
                    }
                    db.Vacunas.Add(unaVacuna);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }         
        }

        public IEnumerable<Vacuna> FiltrarVacunas(int? fase, int? rangomin, int? rangomax, string tipo, string laboratorio, string pais)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var vacunasFiltradas = db.Vacunas.AsQueryable<Vacuna>();
                if(fase != null)
                {
                    vacunasFiltradas = vacunasFiltradas
                                        .Where(vac => vac.Fase == fase);
                }
                if(rangomin != null && rangomax != null)
                {
                    vacunasFiltradas = vacunasFiltradas
                                        .Where(vac => vac.Precio >= rangomin && vac.Precio <= rangomax);
                }
                if(tipo != null)
                {
                    vacunasFiltradas = vacunasFiltradas
                                        .Where(vac => vac.VacunaTipo.Equals(tipo,StringComparison.InvariantCultureIgnoreCase));
                }
                if(laboratorio != null)
                {
                    vacunasFiltradas = vacunasFiltradas
                                        .Where(vac => vac.Laboratorios
                                        .Any(lab => lab.Nombre.Equals(laboratorio, StringComparison.InvariantCultureIgnoreCase))
                                        );
                }
                if(pais != null)
                {
                    vacunasFiltradas = vacunasFiltradas
                                        .Where(vac => vac.EstatusPaises.Contains(pais));
                }
                return vacunasFiltradas
                        .Include("Laboratorios")
                        .Include("Tipo")
                        .ToList();
            }
        }

        public IEnumerable<Vacuna> FiltrarVacunasUnion(int? fase, int? rangomin, int? rangomax, string tipo, string laboratorio, string pais)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                IEnumerable<Vacuna> vacunasFiltradasFase = Enumerable.Empty<Vacuna>();
                IEnumerable<Vacuna> vacunasFiltradasPrecio = Enumerable.Empty<Vacuna>();
                IEnumerable<Vacuna> vacunasFiltradasTipo = Enumerable.Empty<Vacuna>();
                IEnumerable<Vacuna> vacunasFiltradasLaboratorio = Enumerable.Empty<Vacuna>();
                IEnumerable<Vacuna> vacunasFiltradasPaises = Enumerable.Empty<Vacuna>();
                IEnumerable<Vacuna> todas = db.Vacunas.AsQueryable<Vacuna>();
                if (fase != null)
                {
                    vacunasFiltradasFase = todas
                                        .Where(vac => vac.Fase == fase);
                }
                if (rangomin != null && rangomax != null)
                {
                    vacunasFiltradasPrecio = todas
                                        .Where(vac => vac.Precio >= rangomin && vac.Precio <= rangomax);
                }
                if (tipo != null)
                {
                    vacunasFiltradasTipo = todas
                                        .Where(vac => vac.VacunaTipo.Equals(tipo, StringComparison.InvariantCultureIgnoreCase));
                }
                if (laboratorio != null)
                {
                    vacunasFiltradasLaboratorio = todas
                                        .Where(vac => vac.Laboratorios
                                        .Any(lab => lab.Nombre.Equals(laboratorio, StringComparison.InvariantCultureIgnoreCase))
                                        );
                }
                if (pais != null)
                {
                    vacunasFiltradasPaises = todas
                                        .Where(vac => vac.EstatusPaises.Contains(pais));
                }

                return vacunasFiltradasFase.Union(vacunasFiltradasPrecio)
                          .Union(vacunasFiltradasTipo)
                          .Union(vacunasFiltradasLaboratorio)
                          .Union(vacunasFiltradasPaises).ToList();
            }
        }

        public IEnumerable<Vacuna> FindAll()
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Vacunas.Include("Laboratorios").Include("Tipo").ToList();
            }
        }

        public Vacuna FindById(int id)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Vacunas.Find(id);
            }
        }

        public Vacuna FindByNombre(string nombre)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Vacunas.Include("Laboratorios").Include("Tipo").Where(v=> v.Nombre == nombre).SingleOrDefault();

            }          
        }

        public Vacuna FindByNombre2(string nombre)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Vacunas.Include("Tipo").Where(v => v.Nombre == nombre).SingleOrDefault();

            }
        }
    }
}
