using Aplicaciones.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;

namespace Aplicacion.Servicios
{
    public class QueryServicio : IServicioQueryOrden<OrdenCompra>
    {
        private readonly IRepositorioQueryOrden<OrdenCompra> repositorioQueryOrden;

        public QueryServicio(IRepositorioQueryOrden<OrdenCompra> _repositorioQueryOrden)
        {
            repositorioQueryOrden = _repositorioQueryOrden;
        }


        public async Task<List<OrdenCompra>> QueryOrden(OrdenCompra data)
        {
            return await repositorioQueryOrden.QueryOrden(data);
        }


    }

}
