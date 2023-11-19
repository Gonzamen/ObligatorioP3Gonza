using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioCompra
    {
        bool Add(Compra unaCompra);
        IEnumerable<Compra> FindByPrestador(string numprestador);
        IEnumerable<Compra> FindByPrestadorMes(string numprestador);
        Compra FindById(int id);
    }
}
