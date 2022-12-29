using Aplicaciones.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;

namespace Aplicacion.Servicios
{
    public class ProveedorServicio : IServicioProveedors<Proveedor>
    {
        private readonly IRepositorioProveedor<Proveedor> repositorioProveedor;

        public ProveedorServicio(IRepositorioProveedor<Proveedor> _repositorioProveedor)
        {
            repositorioProveedor = _repositorioProveedor;
        }


        public async Task<List<Proveedor>> ListarProveedor()
        {
            return await repositorioProveedor.ListarProveedor();
        }



        public async Task<Proveedor> BuscarProvedorIdProveedor(long idProveedor)
        {
            return await repositorioProveedor.BuscarProvedorIdProveedor(idProveedor);
        }


    }
}
