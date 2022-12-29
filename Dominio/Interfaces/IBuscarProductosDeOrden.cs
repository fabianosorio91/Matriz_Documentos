using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IBuscarProductosDeOrden<TEntidad>
    {
        Task<List<TEntidad>> BuscarProductosDeOrden(long idOrdenCompra);

    }
}
