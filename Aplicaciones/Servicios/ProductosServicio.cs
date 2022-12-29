using Aplicacion.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class ProductosServicio: IServicioProductos<Productos>
    {

        private readonly IRepositorioProductos<Productos> _repositorioProductos;

        public ProductosServicio(IRepositorioProductos<Productos> repositorioProductos)
        {
            _repositorioProductos = repositorioProductos;
        }

        public async Task<List<Productos>> BuscarProductosDeOrden(long idOrdenCompra)
        {
            return await _repositorioProductos.BuscarProductosDeOrden(idOrdenCompra);
        }

    }
}
