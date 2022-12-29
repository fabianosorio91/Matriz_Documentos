using Aplicaciones.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;

namespace Aplicacion.Servicios
{
    public class OrdenServicio : IServicioOrden<OrdenCompra>
    {
        private readonly IRepositorioOrden<OrdenCompra> repositorioOrden;

        public OrdenServicio(IRepositorioOrden<OrdenCompra> _repositorioOrden)
        {
            repositorioOrden = _repositorioOrden;
        }

        public async Task<List<OrdenCompra>> ListarOrden(OrdenCompra data)
        {
            return await repositorioOrden.ListarOrden(data);
        }


    }

}
