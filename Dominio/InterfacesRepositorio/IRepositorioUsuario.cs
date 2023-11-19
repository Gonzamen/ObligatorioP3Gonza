using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioUsuario
    {
        bool Add(Usuario unUsuario);
        Usuario FindByCedula(string cedula);
        bool Update(Usuario unUsuario);
    }
}
