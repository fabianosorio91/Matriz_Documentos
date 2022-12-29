using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IActualizarOrdenCompra<TEntidad>
    {
        Task<List<TEntidad>> ActualizarOrdenCompra(OrdenCompra data);

    }
}
