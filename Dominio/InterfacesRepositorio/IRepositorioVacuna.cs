using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioVacuna
    {
        bool Add(Vacuna unaVacuna);
        IEnumerable<Vacuna> FindAll();
        Vacuna FindByNombre(string nombre);
        Vacuna FindById(int id);
        IEnumerable<Vacuna> FiltrarVacunas(int? fase, int? rangomin, int? rangomax, string tipo, string laboratorio, string pais);
    }
}
