using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioLaboratorio
    {
        bool Add(Laboratorio unLaboratorio);
        IEnumerable<Laboratorio> FindAll();
        Laboratorio FindById(int id);
        Laboratorio FindByNombre(string nombre);
    }
}
