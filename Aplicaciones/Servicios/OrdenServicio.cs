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

        public async Task<OrdenCompra> BuscarOrdenCompra(long idOrden)
        {
            return await repositorioOrden.BuscarOrdenCompra(idOrden);
        }

     

        public async Task<List<OrdenCompra>> ActualizarOrdenCompra(OrdenCompra data)
        {
            return await repositorioOrden.ActualizarOrdenCompra(data);
        }

        public async Task<List<OrdenCompra>> Listar()
        {
            return await repositorioOrden.Listar();
        }

        public async Task<bool> Eliminar(int identificadorElemento)
        {
            return await repositorioOrden.Eliminar(identificadorElemento);
        }
    }

}
