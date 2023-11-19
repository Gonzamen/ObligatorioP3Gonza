using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioPrestadorSalud
    {
        bool Add(PrestadorSalud unPrestador);
        IEnumerable<PrestadorSalud> FindAll();
        PrestadorSalud FindByNum(string num);
    }
}
