using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioTipoVacuna
    {
        bool Add(TipoVacuna unTipo);
        IEnumerable<TipoVacuna> FindAll();
        TipoVacuna FindByCodigo(string codigo);
    }
}
