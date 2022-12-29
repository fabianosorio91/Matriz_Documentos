using Aplicaciones.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;

namespace Aplicacion.Servicios
{
    public class ProveedorServicio : IServicioProveedors<Proveedor>
    {
        //variable privada instancia repositorio
        private readonly IRepositorioProveedor<Proveedor> repositorioProveedor;
        //constructor
        public ProveedorServicio(IRepositorioProveedor<Proveedor> _repositorioProveedor)
        {
            repositorioProveedor = _repositorioProveedor;
        }

        //Crear
        public async Task<Proveedor> CreateAsync(Proveedor proveedor)
        {
            return await repositorioProveedor.CreateAsync(proveedor);
        }

        //Listar
        public async Task<List<Proveedor>> ListarProveedor()
        {
            return await repositorioProveedor.ListarProveedor();
        }

        //Eliminar
        public async Task<bool> Eliminar(int id)
        {
            return await repositorioProveedor.Eliminar(id);
        }
        //Buscar
        public async Task<Proveedor> BuscarProveedor(int id)
        {
            return await repositorioProveedor.BuscarProveedor(id);
        }
        //Editar
        public async Task<bool> EditarProveedor(Proveedor proveedor)
        {
            return await repositorioProveedor.EditarProveedor(proveedor);
        }
    }
}
