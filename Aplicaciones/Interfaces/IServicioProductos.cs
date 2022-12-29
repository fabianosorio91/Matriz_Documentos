using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    internal interface IServicioProductos<TEntidad>: IBuscarProductosDeOrden<TEntidad>
    {
    }
}
