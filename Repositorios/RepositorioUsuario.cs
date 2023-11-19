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
    public class RepositorioUsuario : IRepositorioUsuario
    {
        public bool Add(Usuario unUsuario)
        {
            if (unUsuario == null || !unUsuario.ValidarSoloCedula())
            {
                return false;
            }
            try
            {
                using (DBVacunacionContext db = new DBVacunacionContext())
                {
                    db.Usuarios.Add(unUsuario);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Usuario FindByCedula(string cedula)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {

                return db.Usuarios.Find(cedula);
            }
        }

        public bool Update(Usuario unUsuario)
        {
            using (DBVacunacionContext db = new DBVacunacionContext())
            {
                if(unUsuario != null)
                {
                    db.Entry(unUsuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }  
            }            
        }
    }
}
